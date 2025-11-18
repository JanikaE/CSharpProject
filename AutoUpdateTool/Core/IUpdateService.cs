using System;

namespace AutoUpdateTool.Core;

public interface IUpdateService
{
    event EventHandler<UpdateStartedArgs> UpdateStarted;

    event EventHandler<UpdateProgressArgs> UpdateProgressChanged;

    event EventHandler<UpdateEndedArgs> UpdateEnded;

    void UpdateNow();
}
