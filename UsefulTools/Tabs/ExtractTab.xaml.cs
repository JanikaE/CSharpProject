using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using UsefulTools.Helpers;
using Windows.Storage;
using Windows.Storage.Pickers;

namespace UsefulTools.Tabs
{
    public sealed partial class ExtractTab : UserControl, INotifyPropertyChanged
    {
        private readonly ObservableCollection<ExtractItem> _items = [];
        private bool _isRunning;
        private string _targetFolder = string.Empty;

        public string TargetFolder
        {
            get => _targetFolder;
            private set
            {
                if (_targetFolder != value)
                {
                    _targetFolder = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRunning
        {
            get => _isRunning;
            private set
            {
                if (_isRunning != value)
                {
                    _isRunning = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(CanExecute));
                    OnPropertyChanged(nameof(CanModify));
                }
            }
        }

        public bool CanExecute => !_isRunning;

        public bool CanModify => !_isRunning;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExtractTab()
        {
            InitializeComponent();
            ExtractArchiveListView.ItemsSource = _items;
            ExtractProgressListView.ItemsSource = _items;
            // 恢复并发模式与“解压到同名子文件夹”的勾选状态（默认勾选）
            ExtractModeComboBox.SelectedIndex = ExtractConfig.Instance.ParallelMode;
            ExtractSubfolderCheckBox.IsChecked = ExtractConfig.Instance.CreateNamedSubfolder;
        }

        private void ExtractSubfolder_Changed(object sender, RoutedEventArgs e)
        {
            ExtractConfig.Instance.CreateNamedSubfolder = ExtractSubfolderCheckBox.IsChecked == true;
            ExtractConfig.Instance.Save();
        }

        private void ExtractMode_Changed(object sender, SelectionChangedEventArgs e)
        {
            ExtractConfig.Instance.ParallelMode = ExtractModeComboBox.SelectedIndex;
            ExtractConfig.Instance.Save();
        }

        private void ClearError()
        {
            ExtractErrorTextBlock.Visibility = Visibility.Collapsed;
        }

        private void ShowError(string message)
        {
            ExtractErrorTextBlock.Text = message;
            ExtractErrorTextBlock.Visibility = Visibility.Visible;
        }

        private async void PickTargetFolder_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FolderPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
            picker.FileTypeFilter.Add("*");
            StorageFolder folder = await picker.PickSingleFolderAsync();
            if (folder != null)
            {
                TargetFolder = folder.Path;
            }
        }

        private async void AddArchives_Click(object sender, RoutedEventArgs e)
        {
            var picker = new FileOpenPicker();
            var hwnd = WinRT.Interop.WindowNative.GetWindowHandle(App.MainWindow);
            WinRT.Interop.InitializeWithWindow.Initialize(picker, hwnd);
            picker.SuggestedStartLocation = PickerLocationId.Downloads;
            picker.FileTypeFilter.Add(".zip");

            System.Collections.Generic.IReadOnlyList<StorageFile> files = await picker.PickMultipleFilesAsync();
            if (files == null || files.Count == 0)
            {
                return;
            }

            int added = 0;
            foreach (StorageFile file in files)
            {
                if (!_items.Any(i =>
                        string.Equals(i.FilePath, file.Path, StringComparison.OrdinalIgnoreCase)))
                {
                    _items.Add(new ExtractItem(file.Path));
                    added++;
                }
            }

            if (added == 0)
            {
                ShowError("所选压缩包均已存在于列表中。");
            }
            else
            {
                ClearError();
            }
        }

        private void RemoveSelected_Click(object sender, RoutedEventArgs e)
        {
            var selected = ExtractArchiveListView.SelectedItems.Cast<ExtractItem>().ToList();
            foreach (ExtractItem item in selected)
            {
                _items.Remove(item);
            }
            ClearError();
        }

        private void ClearArchives_Click(object sender, RoutedEventArgs e)
        {
            _items.Clear();
            ClearError();
        }

        private async void RunExtract_Click(object sender, RoutedEventArgs e)
        {
            ClearError();

            if (string.IsNullOrWhiteSpace(TargetFolder))
            {
                ShowError("请先选择解压目标文件夹。");
                return;
            }

            if (_items.Count == 0)
            {
                ShowError("请先添加要解压的压缩包。");
                return;
            }

            bool createSubfolder = ExtractSubfolderCheckBox.IsChecked == true;
            string targetFolder = TargetFolder;
            var parallelMode = (ExtractParallelMode)ExtractModeComboBox.SelectedIndex;

            IsRunning = true;
            ExtractStatusPanel.Visibility = Visibility.Visible;

            try
            {
                await Task.Run(() =>
                    ExtractService.Execute(
                        _items.ToList(),
                        targetFolder,
                        createSubfolder,
                        parallelMode,
                        item => DispatcherQueue.TryEnqueue(() =>
                        {
                            item.State = ExtractState.Running;
                            item.Progress = 0;
                            item.Message = "开始解压...";
                        }),
                        (item, progress) => DispatcherQueue.TryEnqueue(() =>
                        {
                            item.Progress = progress;
                            item.Message = $"已解压 {item.ProgressText}";
                        }),
                        (item, state, message) => DispatcherQueue.TryEnqueue(() =>
                        {
                            item.State = state;
                            if (state == ExtractState.Completed)
                            {
                                item.Progress = 100;
                            }
                            item.Message = message;
                        })));
            }
            finally
            {
                IsRunning = false;
                ExtractStatusPanel.Visibility = Visibility.Collapsed;
            }
        }

        private void ClearProgress_Click(object sender, RoutedEventArgs e)
        {
            foreach (ExtractItem item in _items)
            {
                item.State = ExtractState.Pending;
                item.Progress = 0;
                item.Message = string.Empty;
            }
            ClearError();
        }

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}