﻿namespace Poliedro.Psr.Domain.Common.Models;

public class PaginatedResponse<T>(IEnumerable<T> items, int totalCount, int pageNumber, int pageSize)
{
    public IEnumerable<T> Items { get; set; } = items;
    public int TotalCount { get; set; } = totalCount;
    public int PageNumber { get; set; } = pageNumber;
    public int PageSize { get; set; } = pageSize;
}
