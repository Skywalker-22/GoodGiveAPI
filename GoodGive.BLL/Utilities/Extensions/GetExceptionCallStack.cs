namespace GoodGive.BLL.Utilities.Extensions;

public static class ExceptionExtensions
{
    public static string GetExceptionCallStack(this Exception exception)
    {
        string callstack = "Call stack unavailable.";

        if (exception.TargetSite != null && exception.TargetSite.ReflectedType != null && exception.TargetSite.ReflectedType.FullName != null)
        {
            callstack = exception.TargetSite.ReflectedType.FullName;
        }

        return callstack;
    }
}