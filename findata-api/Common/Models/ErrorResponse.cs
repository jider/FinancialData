using Microsoft.AspNetCore.Mvc;

namespace findata_api.Common.Models;

public class ErrorResponse
{
    public int StatusCode { get; set; }
    public string? StatusPhrase { get; set; }
    public List<string> Errors { get; set; } = [];
    public DateTime Timestamp { get; set; }

    public static IActionResult GenerateErrorResponse(ActionContext actionContext)
    {
        var apiError = new ErrorResponse
        {
            StatusCode = 400,
            StatusPhrase = "Bad Request",
            Timestamp = DateTime.Now
        };

        var errors = actionContext.ModelState.AsEnumerable();
        foreach (var error in errors)
        {
            if (error.Value is null) continue;

            foreach (var inner in error.Value.Errors)
            {
                apiError.Errors.Add(inner.ErrorMessage);
            }
        }

        return new BadRequestObjectResult(apiError);
    }
}
