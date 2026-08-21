using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Media;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UsefulTools.Helpers;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace UsefulTools.Tabs
{
    public sealed partial class BackupTab : UserControl, INotifyPropertyChanged
    {
        private readonly ObservableCollection<BackupResultItem> _results = [];
        private bool _isBackupRunning;

        public bool IsBackupRunning
        {
            get => _isBackupRunning;
            private set
            {
                if (_isBackupRunning != value)
                {
                    _isBackupRunning = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanExecute));
                    OnPropertyChanged(nameof(CanModify));
                }
            }
        }

        public bool CanExecute => !_isBackupRunning;

        public bool CanModify => !_isBackupRunning;

        public event PropertyChangedEventHandler PropertyChanged;

        public BackupTab()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            BackupResultListView.ItemsSource = _results;
            BackupShowIgnoreCheckBox.IsChecked = BackupConfig.Instance.IsShowIgnore;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            UpdatePathPairPanel();
        }

        private void UpdatePathPairPanel()
        {
            PathPairItemsControl.ItemsSource = null;
            PathPairItemsControl.ItemsSource = BackupConfig.Instance.PathPairs;
        }

        private void AddPathPair_Click(object sender, RoutedEventArgs e)
        {
            _ = ShowPathPairDialogAsync(null);
        }

        private void EditPathPair_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: PathPair pathPair })
            {
                _ = ShowPathPairDialogAsync(pathPair);
            }
        }

        private async Task ShowPathPairDialogAsync(PathPair existing)
        {
            var nameBox = new TextBox { Header = "名称", Text = existing?.Name ?? "" };
            var sourceBox = new TextBox { Header = "源路径", Text = existing?.SourcePath ?? "" };
            var targetBox = new TextBox { Header = "目标路径", Text = existing?.TargetPath ?? "" };
            var policyBox = new ComboBox
            {
                Header = "备份策略",
                SelectedIndex = existing?.Policy ?? (int)BackupPolicy.Overwrite,
            };
            policyBox.Items.Add("覆盖");
            policyBox.Items.Add("追加");
            var sourcePicker = new Button { Content = "选择源目录..." };
            var targetPicker = new Button { Content = "选择目标目录..." };

            sourcePicker.Click += async (_, _) => sourceBox.Text = await PickFolderAsync() ?? sourceBox.Text;
            targetPicker.Click += async (_, _) => targetBox.Text = await PickFolderAsync() ?? targetBox.Text;

            var panel = new StackPanel { Spacing = 12 };
            panel.Children.Add(nameBox);
            panel.Children.Add(sourceBox);
            panel.Children.Add(sourcePicker);
            panel.Children.Add(targetBox);
            panel.Children.Add(targetPicker);
            panel.Children.Add(policyBox);

            var dialog = new ContentDialog
            {
                Title = existing == null ? "新增路径" : "修改路径",
                Content = panel,
                PrimaryButtonText = "确定",
                CloseButtonText = "取消",
                DefaultButton = ContentDialogButton.Primary,
                XamlRoot = XamlRoot,
            };

            if (await dialog.ShowAsync() == ContentDialogResult.Primary)
            {
                string sourcePath = sourceBox.Text.Trim();
                string targetPath = targetBox.Text.Trim();
                if (string.IsNullOrWhiteSpace(sourcePath) || string.IsNullOrWhiteSpace(targetPath))
                {
                    ShowError("源路径和目标路径不能为空。");
                    return;
                }

                if (existing == null)
                {
                    BackupConfig.Instance.PathPairs.Add(new PathPair
                    {
                        Name = nameBox.Text.Trim(),
                        SourcePath = sourcePath,
                        TargetPath = targetPath,
                        Policy = policyBox.SelectedIndex,
                    });
                }
                else
                {
                    existing.Name = nameBox.Text.Trim();
                    existing.SourcePath = sourcePath;
                    existing.TargetPath = targetPath;
                    existing.Policy = policyBox.SelectedIndex;
                }
                BackupConfig.Instance.Save();
                UpdatePathPairPanel();
            }
        }

        private static async Task<string> PickFolderAsync()
        {
            var picker = new FolderPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
            picker.FileTypeFilter.Add("*");
            StorageFolder folder = await picker.PickSingleFolderAsync();
            return folder?.Path;
        }

        private void DeletePathPair_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: PathPair pathPair })
            {
                BackupConfig.Instance.PathPairs.Remove(pathPair);
                BackupConfig.Instance.Save();
                UpdatePathPairPanel();
            }
        }

        private void OpenSourcePath_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: PathPair { SourcePath: { } source } })
            {
                Process.Start("explorer.exe", source);
            }
        }

        private void OpenTargetPath_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: PathPair { TargetPath: { } target } })
            {
                Process.Start("explorer.exe", target);
            }
        }

        private async void ExecOne_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: PathPair pathPair })
            {
                await RunBackupAsync(new List<PathPair> { pathPair });
            }
        }

        private async void ExecBackup_Click(object sender, RoutedEventArgs e)
        {
            await RunBackupAsync(BackupConfig.Instance.PathPairs);
        }

        private async Task RunBackupAsync(IEnumerable<PathPair> pathPairs)
        {
            if (IsBackupRunning)
                return;

            BackupErrorTextBlock.Visibility = Visibility.Collapsed;
            _results.Clear();
            IsBackupRunning = true;
            BackupStatusPanel.Visibility = Visibility.Visible;

            try
            {
                bool showIgnore = BackupShowIgnoreCheckBox.IsChecked == true;
                await Task.Run(() =>
                    BackupService.Execute(
                        pathPairs,
                        showIgnore,
                        item => DispatcherQueue.TryEnqueue(() =>
                        {
                            _results.Add(item);
                            ScrollResultsToBottom();
                        }),
                        (item, type, message) => DispatcherQueue.TryEnqueue(() =>
                        {
                            item.Type = type;
                            item.Message = message;
                        })));
            }
            finally
            {
                IsBackupRunning = false;
                BackupStatusPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void ClearResults_Click(object sender, RoutedEventArgs e)
        {
            _results.Clear();
        }

        private void IsShowIgnore_Changed(object sender, RoutedEventArgs e)
        {
            BackupConfig.Instance.IsShowIgnore = BackupShowIgnoreCheckBox.IsChecked == true;
            BackupConfig.Instance.Save();
        }

        private ScrollViewer _resultScrollViewer;

        private void ScrollResultsToBottom()
        {
            if (_resultScrollViewer == null)
            {
                _resultScrollViewer = FindDescendantScrollViewer(BackupResultListView);
            }
            if (_resultScrollViewer != null)
            {
                DispatcherQueue.TryEnqueue(() =>
                {
                    _resultScrollViewer.ChangeView(
                        null,
                        _resultScrollViewer.ScrollableHeight,
                        null,
                        disableAnimation: true);
                });
            }
        }

        private static ScrollViewer FindDescendantScrollViewer(DependencyObject root)
        {
            int count = VisualTreeHelper.GetChildrenCount(root);
            for (int i = 0; i < count; i++)
            {
                var child = VisualTreeHelper.GetChild(root, i);
                if (child is ScrollViewer scrollViewer)
                {
                    return scrollViewer;
                }
                var found = FindDescendantScrollViewer(child);
                if (found != null)
                {
                    return found;
                }
            }
            return null;
        }

        private void ShowError(string message)
        {
            BackupErrorTextBlock.Text = message;
            BackupErrorTextBlock.Visibility = Visibility.Visible;
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}