using Prism.Mvvm;

namespace MetricFlow.ViewModels
{
    public class MainViewModel : BindableBase
    {
        #region property DisplayName

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
    }
}