using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class UserEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public ObjectId Id { get; set; }
    [BsonElement("person")]
    public required PersonEntity Person { get; set; }
    [BsonElement("address")]
    public required IEnumerable<AddressEntity> Address { get; set; }
}
