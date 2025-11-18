using System;

namespace AutoUpdateTool.Core;

public class UpdateProgressArgs : EventArgs
{
    public string Text { get; internal set; }

    public float ProgressPercent { get; internal set; }
}
