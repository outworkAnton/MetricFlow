using System;
using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows;
using BusinessLogic.Contract;
using BusinessLogic.Contract.Exceptions;
using Prism.Mvvm;

namespace MetricFlow.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region Backing fields

        /// <summary>
        /// Represent DisplayName property
        /// </summary>
        public string DisplayName
        {
            get => _displayName;
            set => SetProperty(ref _displayName, value);
        }

        /// <summary>
        /// Backing field for property DisplayName
        /// </summary>
        private string _displayName = "MetricFlow - Utility Services Manager";


        #endregion

        private static IRevisionService _revisionService;

        public MainViewModel(IRevisionService revisionService)
        {
            _revisionService = revisionService;
        }

        static async Task CheckDatabase()
        {
            try
            {
                await _revisionService.DownloadLatestDatabaseRevision();
            }
            catch (NetworkException networkException)
            {
                ConfirmOnException(networkException,
                    "\nUnable to get database file from server\nWould you like to work with a local copy?",
                    "Connection problem was occurred");
            }
            catch (FileException fileException)
            {
                throw new Exception("Database file update or locate is failed\n" + fileException.Message);
            }
            catch (ServiceException serviceException)
            {
                ConfirmOnException(serviceException,
                    "\nThere was a problem with the Google Drive service\nWould you like to work with a local copy?",
                    "Service problem was occurred");
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message, "Cannot start application",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                Application.Current.Shutdown();
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

        public static async void OnLoaded()
        {
            await CheckDatabase().ConfigureAwait(false);
        }
    }
}
