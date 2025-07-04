using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class PaginateHistoriesReader
{
    [BsonElement("items")]
    public IEnumerable<ReaderHistoriesEntity>? Items { get; set; }
    [BsonElement("totalCount")]
    public int TotalCount { get; set; }
    [BsonElement("pageNumber")]
    public int PageNumber { get; set; }
    [BsonElement("pageSize")]
    public  int PageSize { get; set; }
}
