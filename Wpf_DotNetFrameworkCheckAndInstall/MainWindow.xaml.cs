using GalaSoft.MvvmLight.Messaging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Wpf_DotNetFrameworkCheckAndInstall
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        private SystemInformationUC uc;
        public MainWindow()
        {
            InitializeComponent();
            uc = new SystemInformationUC();


            Messenger.Default.Register<object>(this, "SystemInformationUCToken", obj =>
            {
                mainArea.Child = uc;
            });
        }
    }
}
