using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using UsefulTools.Helpers;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace UsefulTools.Tabs
{
    public sealed partial class BackupTab : UserControl
    {
        public BackupTab()
        {
            InitializeComponent();
            Loaded += OnLoaded;
            BackupShowIgnoreCheckBox.IsChecked = BackupConfig.Instance.IsShowIgnore;
            BackupPolicyComboBox.SelectedIndex = BackupConfig.Instance.Policy;
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
                    });
                }
                else
                {
                    existing.Name = nameBox.Text.Trim();
                    existing.SourcePath = sourcePath;
                    existing.TargetPath = targetPath;
                }
                BackupConfig.Instance.Save();
                UpdatePathPairPanel();
            }
        }

        private async Task<string> PickFolderAsync()
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

        private void ExecOne_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button { Tag: PathPair pathPair })
            {
                RunBackup(new List<PathPair> { pathPair });
            }
        }

        private void ExecBackup_Click(object sender, RoutedEventArgs e)
        {
            RunBackup(BackupConfig.Instance.PathPairs);
        }

        private void RunBackup(IEnumerable<PathPair> pathPairs)
        {
            BackupErrorTextBlock.Visibility = Visibility.Collapsed;
            List<BackupResultItem> results = BackupService.Execute(
                pathPairs,
                BackupPolicyComboBox.SelectedIndex,
                BackupShowIgnoreCheckBox.IsChecked == true);
            BackupResultListView.ItemsSource = null;
            BackupResultListView.ItemsSource = results;
        }

        private void ClearResults_Click(object sender, RoutedEventArgs e)
        {
            BackupResultListView.ItemsSource = null;
        }

        private void IsShowIgnore_Changed(object sender, RoutedEventArgs e)
        {
            BackupConfig.Instance.IsShowIgnore = BackupShowIgnoreCheckBox.IsChecked == true;
            BackupConfig.Instance.Save();
        }

        private void Policy_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            BackupConfig.Instance.Policy = BackupPolicyComboBox.SelectedIndex;
            BackupConfig.Instance.Save();
        }

        private void ShowError(string message)
        {
            BackupErrorTextBlock.Text = message;
            BackupErrorTextBlock.Visibility = Visibility.Visible;
        }
    }
}
