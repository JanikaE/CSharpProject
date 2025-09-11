using FileManager.Configs;
using FileManager.Forms.Deletes;
using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace FileManager.Controls
{
    public partial class UserControlDeletePattern : UserControl
    {
        private readonly DeletePattern _deletePattern;

        [Browsable(false)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public DeletePattern DeletePattern => _deletePattern;

        public UserControlDeletePattern(DeletePattern deletePattern)
        {
            InitializeComponent();
            _deletePattern = deletePattern ?? throw new ArgumentNullException(nameof(deletePattern));

            // 初始化控件值
            textBoxSourcePath.Text = _deletePattern.SourcePath;
            textBoxMatchFolder.Text = _deletePattern.MatchFolder;
            textBoxMatchFile.Text = _deletePattern.MatchFile;
            checkBoxFullMatch.Checked = _deletePattern.IsFullMatch;
            checkBoxIgnoreCase.Checked = _deletePattern.IsIgnoreCase;

            // 绑定控件值变化事件
            HookEvents();
        }

        private void HookEvents()
        {
            // 监听控件值变化事件
            textBoxSourcePath.TextChanged += UpdateDeletePattern;
            textBoxMatchFolder.TextChanged += UpdateDeletePattern;
            textBoxMatchFile.TextChanged += UpdateDeletePattern;
            checkBoxFullMatch.CheckedChanged += UpdateDeletePattern;
            checkBoxIgnoreCase.CheckedChanged += UpdateDeletePattern;
        }

        private void UpdateDeletePattern(object sender, EventArgs e)
        {
            // 更新对象属性
            _deletePattern.SourcePath = textBoxSourcePath.Text;
            _deletePattern.MatchFolder = textBoxMatchFolder.Text;
            _deletePattern.MatchFile = textBoxMatchFile.Text;
            _deletePattern.IsFullMatch = checkBoxFullMatch.Checked;
            _deletePattern.IsIgnoreCase = checkBoxIgnoreCase.Checked;
            Config.Instance.Save();
        }

        private void ButtonExec_Click(object sender, EventArgs e)
        {
            FormExecDelete formExecDelete = new()
            {
                DeletePattern = DeletePattern
            };
            formExecDelete.Show();
            formExecDelete.Execute();
        }

        private void ButtonDelete_Click(object sender, EventArgs e)
        {
            Config.Instance.DeletePatterns.Remove(DeletePattern);
            Config.Instance.Save();
            Dispose();
            Global.UIDelete.SetPanelPosition();
        }
    }
}