using System;
using System.Threading;
using System.Windows.Forms;
using AutoUpdateTool.Core;
using AutoUpdateTool.Model;

namespace AutoUpdateTool;

public partial class UpdateForm : Form
{
    private Thread _updateThread;
    private IUpdateService _updateService;

    public UpdateForm()
    {
        InitializeComponent();
        this.Load += UpdateForm_Load;
    }

    private void UpdateForm_Load(object sender, EventArgs e)
    {
        if (!UpdateCore.TryResolveUpdateService(out _updateService))
        {
            this.Close();
            return;
        }

        _updateThread = new Thread(DoUpdate);
        _updateThread.Start();
    }

    private void DoUpdate()
    {
        _updateService.UpdateStarted += UpdateService_UpdateStarted;
        _updateService.UpdateProgressChanged += UpdateService_UpdateProgressChanged;
        _updateService.UpdateEnded += UpdateService_UpdateEnded;
        _updateService.UpdateNow();
        _updateService.UpdateStarted -= UpdateService_UpdateStarted;
        _updateService.UpdateProgressChanged -= UpdateService_UpdateProgressChanged;
        _updateService.UpdateEnded -= UpdateService_UpdateEnded;
    }

    private void UpdateService_UpdateStarted(object sender, UpdateStartedArgs e)
    {
        if (this.InvokeRequired)
        {
            this.BeginInvoke(new Action<object, UpdateStartedArgs>(UpdateService_UpdateStarted), sender, e);
            return;
        }

        txtlab.Text = e.Text;
    }

    private void UpdateService_UpdateProgressChanged(object sender, UpdateProgressArgs e)
    {
        if (this.InvokeRequired)
        {
            this.BeginInvoke(new Action<object, UpdateProgressArgs>(UpdateService_UpdateProgressChanged), sender, e);
            return;
        }

        probar.Value = (int)(e.ProgressPercent * 100);
        txtlab.Text = $"{e.Text}({e.ProgressPercent:P1})...";
    }

    private void UpdateService_UpdateEnded(object sender, UpdateEndedArgs e)
    {
        if (this.InvokeRequired)
        {
            this.BeginInvoke(new Action<object, UpdateEndedArgs>(UpdateService_UpdateEnded), sender, e);
            return;
        }

        if (e.EndedType == UpdateEndedType.Completed)
        {
            UpdateCore.UpdateSign = UpdateStatus.Succeed;
            txtlab.Text = "操作成功";
            this.DialogResult = DialogResult.OK;
        }
        else if (e.ErrorException is ThreadAbortException)
        {
            UpdateCore.UpdateSign = UpdateStatus.Cancel;
            txtlab.Text = "已取消操作";
        }
        else
        {
            UpdateCore.UpdateSign = UpdateStatus.Fail;
            txtlab.Text = "操作中遇到错误";
            MessageBox.Show(this, e.ErrorMessage, "错误", MessageBoxButtons.OK, MessageBoxIcon.Error);
            this.DialogResult = DialogResult.OK;
        }
    }
}