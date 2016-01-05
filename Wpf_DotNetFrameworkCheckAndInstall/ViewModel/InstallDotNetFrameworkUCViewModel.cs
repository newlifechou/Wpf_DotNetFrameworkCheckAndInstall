using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Collections.Generic;
using System.Windows;
using Wpf_DotNetFrameworkCheckAndInstall.Model;

namespace Wpf_DotNetFrameworkCheckAndInstall.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class InstallDotNetFrameworkUCViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the InstallDotNetFrameworkUCViewModel class.
        /// </summary>
        public InstallDotNetFrameworkUCViewModel()
        {
            SystemInformationCollection sic = new SystemInformationCollection();
            DNFrameworkList = sic.DNFrameworkNameList;

            //OpenTestCommand = new RelayCommand<string>(i =>
            //  {
            //      if (string.IsNullOrEmpty(i))
            //      {
            //          sic.OpenIt(i);
            //      }

            //  });

            OpenFileCommand = new RelayCommand<string>(i =>
              {
                  if (!string.IsNullOrEmpty(i))
                  {
                      //这里注意对sic的引用有闭包问题，切记
                      new SystemInformationCollection().OpenIt(i);
                  }
                  //MessageBox.Show(i);
              });
        }

        #region 公开属性
        private List<string> dNFrameworkList;

        public List<string> DNFrameworkList
        {
            get { return dNFrameworkList; }
            set { dNFrameworkList = value; RaisePropertyChanged(() => DNFrameworkList); }
        }

        #endregion
        #region 公开命令
        public RelayCommand<string> OpenFileCommand { get; set; }
        #endregion
    }
}