using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace GoodGive.API.Helpers;

public class ApiController : ControllerBase
{
    internal Guid GetUserId()
    {
        var userId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);
        if (userId == null)
        {
            return Guid.Empty;
        }
        else
        {
            return Guid.Parse(userId.Value);
        }
    }
}
