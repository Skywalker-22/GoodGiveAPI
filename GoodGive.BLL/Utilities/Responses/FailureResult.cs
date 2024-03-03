namespace GoodGive.BLL.Utilities.Responses;

public class FailureResult<T> : OperationResult<T>
{
    public override bool Success => false;

    public FailureResult(string errorMessage)
    {
        ErrorMessage = errorMessage;
        Data = default;
    }
}
