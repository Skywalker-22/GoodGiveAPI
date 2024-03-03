namespace GoodGive.BLL.Utilities;

public abstract class OperationResult<T>
{
    public T? Data { get; protected set; }
    public abstract bool Success { get; }
    public string? ErrorMessage { get; protected set; }

    protected OperationResult()
    { }

    public bool HasErrorMessage => !string.IsNullOrEmpty(ErrorMessage);
}
