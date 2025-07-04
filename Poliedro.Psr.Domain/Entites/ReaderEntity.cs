using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class ReaderEntity
{
    [BsonElement("reader")]
    public string? Reader { get; set; }
    [BsonElement("dateTime")]
    public DateTime DateTime { get; set; }
    [BsonElement("lastReader")]
    public string? LastReader { get; set; }
}
