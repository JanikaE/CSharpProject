using System.ComponentModel;
using System.IO;
using System.Runtime.CompilerServices;

namespace UsefulTools.Tabs
{
    public enum ExtractState
    {
        Pending,
        Running,
        Completed,
        Failed,
    }

    /// <summary>
    /// 单个待解压压缩包的模型，包含解压进度与状态。
    /// </summary>
    public sealed partial class ExtractItem : INotifyPropertyChanged
    {
        public string FilePath { get; }

        public string Name { get; }

        private ExtractState _state = ExtractState.Pending;

        public ExtractState State
        {
            get => _state;
            set
            {
                if (_state != value)
                {
                    _state = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(IsRunning));
                }
            }
        }

        private double _progress;

        public double Progress
        {
            get => _progress;
            set
            {
                if (_progress != value)
                {
                    _progress = value;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(ProgressText));
                }
            }
        }

        public string ProgressText => $"{_progress:0}%";

        private string _message = string.Empty;

        public string Message
        {
            get => _message;
            set
            {
                if (_message != value)
                {
                    _message = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsRunning => _state == ExtractState.Running;

        public ExtractItem(string filePath)
        {
            FilePath = filePath;
            Name = Path.GetFileName(filePath);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}