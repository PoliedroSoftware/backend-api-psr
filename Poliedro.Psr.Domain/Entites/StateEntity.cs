using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class StateEntity
{
    [BsonElement("state")]
    public bool State { get; set; }
    [BsonElement("description")]
    public string? Description { get; set; }
    [BsonElement("color")]
    public string? Color { get; set; }
}
