using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Download;
using Google.Apis.Drive.v3;
using Google.Apis.Services;
using Google.Apis.Util.Store;

namespace MetricFlow.Helpers
{
    public static class GoogleDriveHelper
    {
        static readonly string[] Scopes = {DriveService.Scope.DriveReadonly};
        private const string APPLICATION_NAME = "MetricFlow";

        public static IDownloadProgress SyncDatabase()
        {
            UserCredential credential;

            using (var stream =
                new FileStream("client_id.json", FileMode.Open, FileAccess.Read))
            {
                string credPath = Environment.GetFolderPath(
                    Environment.SpecialFolder.Personal);
                credPath = Path.Combine(credPath, ".credentials/metric-flow.json");

                credential = GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    Scopes,
                    Environment.UserName,
                    CancellationToken.None,
                    new FileDataStore(credPath, true)).Result;
                Debug.WriteLine("Credential file saved to: " + credPath);
            }

            var service = new DriveService(new BaseClientService.Initializer
            {
                HttpClientInitializer = credential,
                ApplicationName = APPLICATION_NAME
            });
            Debug.WriteLine("Drive API service was created");

            var databaseDirectory = Path.GetDirectoryName(Process.GetCurrentProcess()
                                        .MainModule
                                        .FileName) + "\\Database";
            if (!Directory.Exists(databaseDirectory))
            {
                Debug.WriteLine("Directory of database not exist");
                Directory.CreateDirectory(databaseDirectory);
                Debug.WriteLine("Directory of database was created");
            }
            var databaseFileName = databaseDirectory + "\\Metric.Flow.Database";
            var request = service.Files.Get("1OPaCtIMT89zY6gg5PZMQ3ygbIRn-gUJ6");
            Debug.WriteLine("Gets database file from server");

            using (var stream =
                new FileStream(databaseFileName
                    , FileMode.OpenOrCreate, FileAccess.ReadWrite,
                    FileShare.ReadWrite))
            {
                return request.DownloadWithStatus(stream);
            }
        }
    }
}