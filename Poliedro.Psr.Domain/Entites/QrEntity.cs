using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class QrEntity
{
    [BsonElement("code")]
    public string? Code { get; set; }
    [BsonElement("uri")]
    public Uri? Uri { get; set; }
}
