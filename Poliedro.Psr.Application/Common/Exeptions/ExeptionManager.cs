using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Http;
using Poliedro.Psr.Application.Common.Features;

namespace Poliedro.Psr.Application.Common.Exeptions;

public class ExceptionManager : IExceptionFilter
{
    public void OnException(ExceptionContext context)
    {
        context.Result = new ObjectResult(ResponseApiService.Response(
            StatusCodes.Status500InternalServerError, null, context.Exception.Message));

        context.HttpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
    }
}
