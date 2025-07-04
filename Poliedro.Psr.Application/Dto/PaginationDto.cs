using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Application.Dto;

public record PaginationDto<T>
(
    IEnumerable<T> Items,
    int TotalCount,
    int PageNumber,
    int PageSize
)
{
    public static implicit operator PaginationDto<T>(TransalationsAvailable v)
    {
        throw new NotImplementedException();
    }
}
