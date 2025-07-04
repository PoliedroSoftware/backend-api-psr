using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Application.Mock;
using Poliedro.Psr.Domain.Wrapper;
using StackExchange.Redis;

namespace Poliedro.Psr.Api.Controllers.v1.Psr;

[Route("api/v1/[controller]")]
[ApiController]
public class UsersController(
    IDatabase _cache,
    ILogger<UsersController> _logger
    ) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<PaginationDto<PersonSearch>>> GetAll(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10, 
        [FromQuery] string? neighborhood = null,
        [FromQuery] string? name = null)
    {
        string cacheKey = $"user:all:{pageNumber}:{pageSize}:{neighborhood}:{name}";
        var cachedData = await _cache.StringGetAsync(cacheKey);
        if (!string.IsNullOrEmpty(cachedData))
        {
            var response = JsonConvert.DeserializeObject<ApiResponse<PaginationDto<PersonSearch>>>(cachedData)!;
            _logger.LogInformation("Info redis.");
            return Ok(response);
        }
        var data = MockDataFactory.GenerateUsers(1000, pageNumber, pageSize, neighborhood, name);
        var responseOK = new ApiResponse<PaginationDto<PersonSearch>>
        {
            Data = data,
            CorrelationId = Guid.NewGuid().ToString(),
        };
        await _cache.StringSetAsync(cacheKey, JsonConvert.SerializeObject(responseOK), TimeSpan.FromMinutes(10));
        return Ok(responseOK);
    }
}
