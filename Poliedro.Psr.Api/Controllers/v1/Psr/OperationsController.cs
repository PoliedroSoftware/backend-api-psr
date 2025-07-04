using Poliedro.Psr.Application.Mock;
using Poliedro.Psr.Domain.Dto;
using Poliedro.Psr.Domain.Wrapper;

namespace Poliedro.Psr.Api.Controllers.v1.Psr;

using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Poliedro.Psr.Application.Dto;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class OperationsController(IDatabase cache) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        string cacheKey = $"erroOperation:all";

        try
        {
            var cachedData = await cache.StringGetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                var response = JsonConvert.DeserializeObject<ApiResponse<List<ErrorReportDto>>>(cachedData)!;
                return Ok(response);
            }
        }
        catch (RedisConnectionException ex)
        {
            Console.WriteLine($"Error conectando a Redis: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }

        var data = MockDataFactory.GenerateMockErrorData();
        var responseOK = new ApiResponse<List<ErrorReportDto>>
        {
            Data = data,
            CorrelationId = Guid.NewGuid().ToString(),
        };

        try
        {
          
            await cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(responseOK), TimeSpan.FromMinutes(10));
        }
        catch (RedisConnectionException ex)
        {
            Console.WriteLine($"Error conectando a Redis: {ex.Message}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error inesperado: {ex.Message}");
        }
        return Ok(responseOK);
    }
}

