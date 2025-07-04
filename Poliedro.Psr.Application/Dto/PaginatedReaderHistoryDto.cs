namespace Poliedro.Psr.Application.Dto;

public record PaginatedReaderHistoryDto(
    IEnumerable<ReaderHistoryDto> Items,
    int TotalCount,
    int PageNumber,
    int PageSize
);


