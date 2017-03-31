using System.Windows;

using Ninject;

using Parser.Auth.Remote;
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
        private readonly IRemoteUserService remoteUserService;

        private ILogFileReaderEngine engine;

        public MainWindow()
        {
            InitializeComponent();

            this.remoteUserService = NinjectStandardKernelProvider.Kernel.Get<IRemoteUserService>();

            var updateStrategy = NinjectStandardKernelProvider.Kernel.Get<IOnUpdateContainer>();
            updateStrategy.OnUpdate += this.OnUpdate;

            this.BtnStop.Visibility = Visibility.Hidden;
        }

        private void BtnStart_Click(object sender, RoutedEventArgs e)
        {
            this.engine = NinjectStandardKernelProvider.Kernel.Get<ILogFileReaderEngine>();
            this.engine.StartAsync();

            this.BtnStart.Visibility = Visibility.Hidden;
            this.BtnStop.Visibility = Visibility.Visible;
        }

        private void BtnStop_Click(object sender, RoutedEventArgs e)
        {
            this.engine.Stop();
            this.engine = null;

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

        private async void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            var username = this.TbUsername.Text;
            var password = this.TbPassword.Password;

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                await this.remoteUserService.Login(username, password);
            }

            if (this.remoteUserService.GetLoggedInRemoteUser() != null)
            {
                this.LabelTbUsername.Visibility = Visibility.Hidden;
                this.LabelTbPassword.Visibility = Visibility.Hidden;
                this.TbUsername.Visibility = Visibility.Hidden;
                this.TbPassword.Visibility = Visibility.Hidden;
                this.BtnLogin.Visibility = Visibility.Hidden;

                this.LabelUsername.Visibility = Visibility.Visible;
                this.LabelUsername.Content = this.remoteUserService.GetLoggedInRemoteUser().Username;
            }
        }
    }
}
