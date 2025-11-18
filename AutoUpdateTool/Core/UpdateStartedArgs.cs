using System;

namespace AutoUpdateTool.Core;

public class UpdateStartedArgs : EventArgs
{
    public string Text { get; set; }
}
