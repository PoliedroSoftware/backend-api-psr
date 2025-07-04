using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Poliedro.Psr.Domain.Entites;

public class LastReaderEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [BsonElement("filename")]
    public string Filename { get; set; }

    [BsonElement("reader")]
    public string Reader{ get; set; }

    [BsonElement("date")]
    public DateTime Date { get; set; }
}
