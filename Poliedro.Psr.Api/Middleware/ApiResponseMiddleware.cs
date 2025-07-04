using Poliedro.Psr.Domain.Wrapper;
using System.Text.Json;

namespace Poliedro.Psr.Api.Middleware;

public class ApiResponseMiddleware(RequestDelegate _next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var originalBodyStream = context.Response.Body;
        using var responseBody = new MemoryStream();
        context.Response.Body = responseBody;

        await _next(context);

        if (context.Response.StatusCode == StatusCodes.Status200OK)
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var bodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            var apiResponse = new ApiResponse<object>
            {
                Data = string.IsNullOrEmpty(bodyText) ? null : JsonSerializer.Deserialize<object>(bodyText),
                CorrelationId = Guid.NewGuid().ToString(),
            };

            var jsonResponse = JsonSerializer.Serialize(apiResponse);
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsync(jsonResponse);
        }
        else
        {
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);
        }
    }
}
