using System;

namespace AutoUpdateTool.Core;

public class UpdateEndedArgs : EventArgs
{
    public UpdateEndedType EndedType { get; }

    public Exception ErrorException { get; }

    public string ErrorMessage { get; }

    public UpdateEndedArgs(string errorMessage, Exception errorException)
    {
        EndedType = UpdateEndedType.ErrorAborted;
        ErrorMessage = errorMessage;
        ErrorException = errorException;
    }

    public UpdateEndedArgs()
    {
        EndedType = UpdateEndedType.Completed;
    }
}
