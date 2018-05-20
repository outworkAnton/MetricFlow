using System;
using System.Diagnostics;
using System.Windows;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Exceptions;

namespace MetricFlow
{
    /// <inheritdoc />
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly IRevisionService _revisionService;

        async void App_Startup(object sender, StartupEventArgs e)
        {
            try
            {
                await _revisionService.DownloadLatestDatabaseRevision().ConfigureAwait(false);
            }
            catch (NetworkException networkException)
            {
                ConfirmOnException(networkException, "\nUnable to get database file from server\nWould you like to work with a local copy?",
                    "Connection problem was occurred");
            }
            catch (FileException fileException)
            {
                throw new Exception("Database file update or locate is failed\n" + fileException.Message);
            }
            catch (ServiceException serviceException)
            {
                ConfirmOnException(serviceException, "\nThere was a problem with the Google Drive service\nWould you like to work with a local copy?",
                    "Service problem was occurred");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Cannot start application",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Current.Shutdown();
            }

        }

        private static void ConfirmOnException(Exception exception, string textBody, string caption)
        {
            switch (MessageBox.Show(exception.Message + textBody, caption,
                MessageBoxButton.YesNo,
                MessageBoxImage.Error))
            {
                case MessageBoxResult.Yes:
                    Debug.WriteLine("Service failed. Working with a local copy");
                    break;
                case MessageBoxResult.No:
                    throw new Exception("Loading application was canceled by user");
            }
        }
    }
}