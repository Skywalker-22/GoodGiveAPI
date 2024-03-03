namespace GoodGive.BLL.Utilities.Responses;

public class SuccessResult<T> : OperationResult<T>
{
    public override bool Success => true;

    public SuccessResult(T data)
    {
        Data = data;
    }

    public SuccessResult()
    {
        Data = default;
    }
}
