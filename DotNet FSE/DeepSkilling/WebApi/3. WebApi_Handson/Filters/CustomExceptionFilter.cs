using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WebApiCustomModel.Filters;

public class CustomExceptionFilter : IExceptionFilter
{
    private readonly string _logPath = Path.Combine(AppContext.BaseDirectory, "logs");

    public void OnException(ExceptionContext context)
    {
        try
        {
            Directory.CreateDirectory(_logPath);
            var file = Path.Combine(_logPath, "exceptions.txt");
            var text = $"[{DateTime.UtcNow:u}] {context.Exception.GetType()}: {context.Exception.Message}\n{context.Exception.StackTrace}\n\n";
            File.AppendAllText(file, text);
        }
        catch { }

        var result = new ObjectResult(new { error = "An internal server error occurred." }) { StatusCode = StatusCodes.Status500InternalServerError };
        context.Result = result;
        context.ExceptionHandled = true;
    }
}
