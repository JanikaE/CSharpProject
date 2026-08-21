using System.Windows.Input;
using Microsoft.UI.Windowing;
using Microsoft.UI.Xaml;
using UsefulTools.Helpers;

namespace UsefulTools
{
    public sealed partial class MainWindow : Window
    {
        public ICommand ReopenWindowCommand { get; }
        public ICommand OpenBase64TabCommand { get; }
        public ICommand OpenNameCaseTabCommand { get; }
        public ICommand OpenBackupTabCommand { get; }
        public ICommand ExitCommand { get; }

        public MainWindow()
        {
            ReopenWindowCommand = new RelayCommand(ReopenWindow);
            OpenBase64TabCommand = new RelayCommand(() => OpenTab(0));
            OpenNameCaseTabCommand = new RelayCommand(() => OpenTab(1));
            OpenBackupTabCommand = new RelayCommand(() => OpenTab(2));
            ExitCommand = new RelayCommand(ExitApplication);

            InitializeComponent();

            this.AppWindow.Closing += AppWindow_Closing;
        }

        private void AppWindow_Closing(AppWindow sender, AppWindowClosingEventArgs args)
        {
            args.Cancel = true;
            sender.Hide();
        }

        private void ReopenWindow()
        {
            AppWindow.Show();
            Activate();
        }

        private void OpenTab(int index)
        {
            AppWindow.Show();
            Activate();
            MainTabView.SelectedIndex = index;
        }

        private void ExitApplication()
        {
            TrayIcon.Dispose();
            App.Current.Exit();
        }
    }
}