using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SQLite;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Threading.Tasks;
using System.Windows;
using Google.Apis.Download;
using MetricFlow.Helpers;
using static System.Configuration.ConfigurationManager;

namespace MetricFlow
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static string DbConnectionString { get; set; }

        void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                var status = GoogleDriveHelper.SyncDatabase();

                if (status.Status == DownloadStatus.NotStarted)
                {
                    throw new NetworkInformationException();
                }

                while (status.Status == DownloadStatus.Downloading)
                {
                    Debug.WriteLine("Downloading database file: " + status.BytesDownloaded);
                }

                if (status.Status == DownloadStatus.Failed)
                {
                    throw new FileLoadException(status.Exception?.Message);
                }

                Debug.WriteLine("Database file updated from server");
                DbConnectionString = ConnectionStrings["MetricFlowDatabase"].ConnectionString;
            }
            catch (NetworkInformationException networkInformationException)
            {
                MessageBox.Show("Cannot connect to server\n" + networkInformationException.Message, "Cannot start application",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                throw;
            }
            catch (FileLoadException fileLoadException)
            {
                MessageBox.Show("Database file update or locate is failed\n" + fileLoadException.Message, "Cannot start application",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                throw;
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Cannot start application",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Current.Shutdown();
            }
        }
    }
}
