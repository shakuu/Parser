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
using Ninject;

using Parser.LogFile.Reader.Contracts;
using Parser.WPFClient.Implementations;
using Parser.WPFClient.NinjectModules;

namespace Parser.WPFClient
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly ILogFileReaderEngine engine;

        public MainWindow()
        {
            InitializeComponent();

            this.engine = NinjectStandardKernelProvider.Kernel.Get<ILogFileReaderEngine>();

            var updateStrategy = NinjectStandardKernelProvider.Kernel.Get<IOnUpdateContainer>();
            updateStrategy.OnUpdate += this.OnUpdate;

            this.BtnStop.Visibility = Visibility.Hidden;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            this.engine.StartAsync();

            this.BtnStart.Visibility = Visibility.Hidden;
            this.BtnStop.Visibility = Visibility.Visible;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            this.engine.Stop();

            this.BtnStop.Visibility = Visibility.Hidden;
            this.BtnStart.Visibility = Visibility.Visible;
        }

        private void OnUpdate(object sender, UpdateEventArgs args)
        {
            this.Dispatcher.Invoke(() =>
            {
                this.LabelTimestamp.Content = args.UpdateMessage;
            });
        }
    }
}
