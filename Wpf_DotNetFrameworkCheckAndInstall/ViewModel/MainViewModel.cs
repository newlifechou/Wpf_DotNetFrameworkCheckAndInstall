using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Wpf_DotNetFrameworkCheckAndInstall.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            ////if (IsInDesignMode)
            ////{
            ////    // Code runs in Blend --> create design time data.
            ////}
            ////else
            ////{
            ////    // Code runs "for real"
            ////}
            ShowSystemInformationUCCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<object>(null, "SystemInformationUCToken");
            });
            ShowInstallDotNetFrameworkUCCommand = new RelayCommand(() =>
            {
                Messenger.Default.Send<object>(null, "InstallDotNetFrameworkUCToken");
            });
        }

        public RelayCommand ShowSystemInformationUCCommand { get; set; }
        public RelayCommand ShowInstallDotNetFrameworkUCCommand { get; set; }
    }
}