using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.InteropServices;
using System.Security.Cryptography;
using System.Threading;
using Google;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Upload;
using Google.Apis.Util.Store;
using Microsoft.Win32;
using File = Google.Apis.Drive.v3.Data.File;

namespace MetricFlow.Helpers
{
    public static class GoogleDriveHelper
    {
        [DllImport("wininet.dll")]
        private static extern bool InternetGetConnectedState(out int description, int reservedValue);

        static readonly string[] Scopes = {DriveService.Scope.DriveReadonly};
        private const string APPLICATION_NAME = "MetricFlow";
        private static readonly DriveService _service;
        private const string _fileId = "1OPaCtIMT89zY6gg5PZMQ3ygbIRn-gUJ6";
        private static readonly string _databaseFileName;

        static GoogleDriveHelper()
        {
            _service = GetService();
            _databaseFileName = GetDatabaseFileName();
        }

        #region Private methods

        static bool IsConnectedToInternet()
        {
            return InternetGetConnectedState(out _, 0);
        }

        static DriveService GetService()
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

                    userCredential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                        GoogleClientSecrets.Load(stream).Secrets,
                        Scopes,
                        Environment.UserName,
                        CancellationToken.None,
                        new FileDataStore(credPath, true)).Result;
                }

                var service = new DriveService(new BaseClientService.Initializer
                {
                    HttpClientInitializer = userCredential,
                    ApplicationName = APPLICATION_NAME
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

        static bool NeedSync(bool downloadDirection = true)
        {
            var revisions = _service?.Revisions?.List(_fileId);
            revisions.Fields = "*";
            var remoteDbFile = revisions?.Execute()?.Revisions
                                       ?.OrderByDescending(revision => revision.ModifiedTime)?.FirstOrDefault();
            DateTime? remoteMod = remoteDbFile?.ModifiedTime;
            long? remoteSize = remoteDbFile?.Size;

            DateTime? localMod = System.IO.File.GetLastWriteTime(_databaseFileName);
            long? localSize = new FileInfo(_databaseFileName).Length;

            if (downloadDirection)
            {
                return (remoteMod == null) || (!(localMod > remoteMod)) || (remoteSize != localSize);
            }

            return (remoteMod == null) || (!(localMod < remoteMod)) || (remoteSize != localSize);
        }

        #endregion

        public static IDownloadProgress DownloadDatabase()
        {
            try
            {
                if (!NeedSync()) return null;

                var request = _service.Files.Get(_fileId);
                Debug.WriteLine("Gets database file from server");

                using (var stream = new FileStream(
                    _databaseFileName,
                    FileMode.OpenOrCreate,
                    FileAccess.ReadWrite,
                    FileShare.ReadWrite)
                )
                {
                    return request.DownloadWithStatus(stream);
                }
            }
            catch (Exception exception)
            {
                throw new ServiceException(exception.Message);
            }
        }

        public static IUploadProgress UploadDatabase()
        {
            try
            {
                if (!NeedSync(false)) return null;

                var body = new File
                {
                    Name = Path.GetFileName(_databaseFileName),
                    Description = "File update automatically by MetricFlow application",
                    MimeType = GetMimeType(_databaseFileName),
                    Parents = new List<string> {"1ylBG0aHKWIKhi2w6hSCmEz9vW9DVBpgU"}
                };

                using (var stream =
                    new FileStream(_databaseFileName
                        , FileMode.Open, FileAccess.Read,
                        FileShare.Read))
                {
                    var request = _service.Files.Update(body, _fileId, stream, body.MimeType);
                    Debug.WriteLine("Gets database file from server");
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