namespace GoodGive.API.Helpers.ExceptionHelper;

public class ErrorLog
{
    public Guid LogId { get; set; }
    public Guid UserId { get; set; }
    public string? Message { get; set; }
    public List<string>? MessageCallStack { get; set; }
    public string? Exception { get; set; }
}
