using Microsoft.AspNetCore.Mvc;
using MongoDB.Driver;
using Newtonsoft.Json;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Application.Mock;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Wrapper;
using StackExchange.Redis;

namespace Poliedro.Psr.Api.Controllers.v1.Psr;

[Route("api/v1/[controller]")]
[ApiController]
public class ReadersController(IDatabase _cache) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<UserEntity>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10)
    {
        string cacheKey = $"readers:all:{pageNumber}:{pageSize}";
        object? response = null;

        try
        {
            var cachedData = await _cache.StringGetAsync(cacheKey);
            if (!string.IsNullOrEmpty(cachedData))
            {
                response = JsonConvert.DeserializeObject<ApiResponse<PaginationDto<ResponseReaderDto>>>(cachedData)!;
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


        var data = MockDataFactory.GetALL(10000, pageNumber, pageSize);
        var responseOK = new ApiResponse<PaginationDto<ResponseReaderDto>>
        {
            Data = data,
            CorrelationId = Guid.NewGuid().ToString(),
        };

        try
        {
            await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(responseOK), TimeSpan.FromMinutes(10));
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



    [HttpGet("{guid}")]
    public async Task<ActionResult<ResponseReaderDto>> GetById(
            [FromQuery] Guid guid,
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 12)
    {
        var response = new ApiResponse<ResponseReaderDto>
        {
            Data = MockDataFactory.GetByID(guid, pageNumber, pageSize),
            CorrelationId = Guid.NewGuid().ToString(),
        };
        return Ok(await Task.FromResult(response));
    }
}
