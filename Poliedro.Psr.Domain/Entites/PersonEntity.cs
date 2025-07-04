using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class PersonEntity
{
    [BsonElement("guid")]
    public Guid Guid { get; set; }
    [BsonElement("name")]
    public required  string Name { get; set; }
    [BsonElement("lastName")]
    public required string LastName {  get; set; }
    [BsonElement("phone")]
    public required string Phone { get; set; }
    [BsonElement("email")]
    public string? Email { get; set; }
}
