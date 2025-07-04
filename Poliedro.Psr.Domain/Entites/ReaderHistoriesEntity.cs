using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class ReaderHistoriesEntity
{
    [BsonElement("month")]
    public string? Month { get; set; }
    [BsonElement("numberMonth")]
    public int NumberMonth { get; set; }
    [BsonElement("reader")]
    public string? Reader { get; set; }
    [BsonElement("year")]
    public int Year { get; set; }
}
