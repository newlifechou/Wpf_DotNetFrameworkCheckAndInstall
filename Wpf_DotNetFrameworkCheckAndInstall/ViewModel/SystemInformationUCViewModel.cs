using System;
using GalaSoft.MvvmLight;
using Wpf_DotNetFrameworkCheckAndInstall.Model;

namespace Wpf_DotNetFrameworkCheckAndInstall.ViewModel
{
    /// <summary>
    /// This class contains properties that a View can data bind to.
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class SystemInformationUCViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the SystemInformationUCViewModel class.
        /// </summary>
        public SystemInformationUCViewModel()
        {
            InitialProperty();
        }
        /// <summary>
        /// 初始化属性
        /// </summary>
        private void InitialProperty()
        {
            SystemInformationCollection sic = new SystemInformationCollection();
            systemVersion = sic.OSVersion;
            NetFrameworkVersion = sic.NetFrameworkVersion;
            ProcessCount = sic.ProcessorCount;
            AllDotNetFrameworkVersion = sic.AllDotNetFrameworkVersion;


        }
        #region 公开属性
        private string systemVersion;

        public string SystemVersion
        {
            get { return systemVersion; }
            set { systemVersion = value; RaisePropertyChanged(() => SystemVersion); }
        }
        private string netFrameworkVerson;

        public string NetFrameworkVersion
        {
            get { return netFrameworkVerson; }
            set { netFrameworkVerson = value; RaisePropertyChanged(() => NetFrameworkVersion); }
        }
        private string processCount;

        public string ProcessCount
        {
            get { return processCount; }
            set { processCount = value; RaisePropertyChanged(() => ProcessCount); }
        }

        private string allDotNetFrameworkVersion;

        public string AllDotNetFrameworkVersion
        {
            get { return allDotNetFrameworkVersion; }
            set { allDotNetFrameworkVersion = value; RaisePropertyChanged(() => AllDotNetFrameworkVersion); }
        }




        #endregion
    }
}