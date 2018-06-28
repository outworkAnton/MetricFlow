using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using BusinessLogic.Contract.Exceptions;
using BusinessLogic.Contract.Interfaces;
using BusinessLogic.Models;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Drive.v3.Data;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Microsoft.Win32;
using File = Google.Apis.Drive.v3.Data.File;

namespace BusinessLogic
{
    public static class GoogleDriveHelper
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        static readonly string[] Scopes = {DriveService.Scope.Drive};
        private const string ApplicationName = "MetricFlow";
        private static readonly DriveService Service;
        private const string FileId = "1tElrrts1g2GR8Tny-xxd3p0N4gAaoGJm";
        private static readonly string DatabaseFileName;
        private static Revision _remoteRevision;

        static GoogleDriveHelper()
        {
            Service = GetService().GetAwaiter().GetResult();
            DatabaseFileName = GetDatabaseFileName();
        }

        #region Private methods

        static bool IsConnectedToInternet()
        {
            return InternetGetConnectedState(out _, 0);
        }

        static async Task<DriveService> GetService()
        {
            try
            {
                if (!IsConnectedToInternet())
                {
                    throw new NetworkException("No Internet connection");
                }

                UserCredential userCredential;
                using (var stream = new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
                {
                    var credPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
                    credPath = Path.Combine(credPath, ".credentials/metric-flow.json");
                    Debug.WriteLine("Credential file saved to: " + credPath);

                    userCredential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                                                                           GoogleClientSecrets.Load(stream).Secrets,
                                                                           Scopes,
                                                                           Environment.UserName,
                                                                           CancellationToken.None,
                                                                           new FileDataStore(credPath, true))
                                                                       .ConfigureAwait(false);
                }

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = userCredential,
                    ApplicationName = ApplicationName
                });
                Debug.WriteLine("Drive API service was created");
                return service;
            }
            catch (GoogleApiException apiException)
            {
                Debug.WriteLine("Google Drive service API was threw an exception\n" + apiException);

                throw new ServiceException("Service error was occured with " +
                                           Enum.Parse(typeof(HttpStatusCode), apiException.HttpStatusCode.ToString()) +
                                           " error\n" + apiException);
            }
            catch (IOException ioException)
            {
                throw new FileException(
                    "There is a problem locating or opening database file\nFile or directory is lost or unavailable\n" +
                    ioException);
            }
        }

        static string GetDatabaseFileName()
        {
            try
            {
                var databaseDirectory = Path.GetDirectoryName(Process.GetCurrentProcess()
                                                                     .MainModule
                                                                     .FileName) + "\\Database";
                if (!Directory.Exists(databaseDirectory))
                {
                    Debug.WriteLine("Directory of database not exist");
                    Directory.CreateDirectory(databaseDirectory);
                    Debug.WriteLine("Directory of database was created");
                }

                return databaseDirectory + "\\Metric.Flow.Database";
            }
            catch (Exception exception)
            {
                throw new FileException(exception.Message);
            }
        }

        private static string GetMimeType(string fileName)
        {
            var mimeType = "application/unknown";
            var ext = Path.GetExtension(fileName)?.ToLower();
            var regKey =
                Registry.ClassesRoot.OpenSubKey(
                    ext ?? throw new FileException("Couldn't get file type"));
            if (regKey?.GetValue("Content Type") != null)
                mimeType = regKey.GetValue("Content Type").ToString();
            return mimeType;
        }

        #endregion

        public static bool NeedDownload(IDatabaseRevision localRevision)
        {
            var revisions = Service?.Revisions?.List(FileId);
            if (revisions != null)
            {
                revisions.Fields = "*";
                _remoteRevision = revisions.Execute()?.Revisions
                                           ?.OrderByDescending(revision => revision.ModifiedTime).FirstOrDefault();
            }
            else
            {
                return false;
            }

            if (localRevision == null)
            {
                return true;
            }

            return localRevision.Modified < _remoteRevision?.ModifiedTime
                   || _remoteRevision?.Id != localRevision.Id
                   || _remoteRevision?.Size != localRevision.Size;
        }

        public static async Task<IDatabaseRevision> GetLatestRemoteRevision()
        {
            try
            {
                var request = Service.Files.Get(FileId);
                Debug.WriteLine("Get database file from server");
                IDownloadProgress status;

                using (var stream = new FileStream(
                    DatabaseFileName,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.ReadWrite)
                )
                {
                    status = await request.DownloadAsync(stream).ConfigureAwait(false);
                }

                if (status != null)
                {
                    while (status.Status == DownloadStatus.Downloading)
                    {
                        Debug.WriteLine("Downloading database file: " + status.BytesDownloaded);
                    }

                    if (status.Status == DownloadStatus.Failed)
                    {
                        throw new ServiceException(status.Exception?.Message);
                    }

                    var remoteRevisionId = _remoteRevision.Id ??
                                           throw new ArgumentNullException("Remote revision has no Id value");
                    var remoteRevisionModified = _remoteRevision.ModifiedTime ??
                                           throw new ArgumentNullException("Remote revision has no ModifiedTime value");
                    var remoteRevisionSize = _remoteRevision.Size ??
                                           throw new ArgumentNullException("Remote revision has no Size value");
                    Debug.WriteLine("Database file updated from server");
                    return new DatabaseRevision(remoteRevisionId, remoteRevisionModified, remoteRevisionSize);
                }

                return null;
            }
            catch (Exception exception)
            {
                throw new ServiceException(exception.Message);
            }
        }

        public static IUploadProgress UpdateRemoteRevision()
        {
            try
            {
                //TODO: implement method for detect local DB changes

                var body = new File
                {
                    Name = Path.GetFileName(DatabaseFileName),
                    Description = "File update automatically by MetricFlow application",
                    MimeType = GetMimeType(DatabaseFileName),
                    Parents = new List<string> {"1ylBG0aHKWIKhi2w6hSCmEz9vW9DVBpgU"}
                };

                using (var stream =
                    new FileStream(DatabaseFileName
                        , FileMode.Open, FileAccess.Read,
                        FileShare.Read))
                {
                    var request = Service.Files.Update(body, FileId, stream, body.MimeType);
                    Debug.WriteLine("Send database file to server");
                    request.Upload();
                    return request.GetProgress();
                }
            }
            catch (Exception exception)
            {
                throw new ServiceException(exception.Message);
            }
        }
    }
}