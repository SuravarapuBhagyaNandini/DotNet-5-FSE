using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiCustomModel.Filters;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
public class CustomAuthFilter : ActionFilterAttribute
{
    public override void OnActionExecuting(ActionExecutingContext context)
    {
        var req = context.HttpContext.Request;
        if (!req.Headers.TryGetValue("Authorization", out var headerValues))
        {
            context.Result = new BadRequestObjectResult("Invalid request - No Auth token");
            return;
        }

        var header = headerValues.ToString();
        if (!header.Contains("Bearer", StringComparison.OrdinalIgnoreCase))
        {
            context.Result = new BadRequestObjectResult("Invalid request - Token present but Bearer unavailable");
            return;
        }

        base.OnActionExecuting(context);
    }
}
