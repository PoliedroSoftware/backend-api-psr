using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Poliedro.Psr.Domain.Entites;

public class ActuatorEntity
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    [BsonElement("_id")]
    public string Id { get; set; }

    [BsonElement("valve")]
    public bool Valve { get; set; }

    [BsonElement("solenoid_valve")]
    public bool SolenoidValve { get; set; }

    [BsonElement("bomb")]
    public bool Bomb { get; set; }

    [BsonElement("circuit")]
    public bool Circuit { get; set; }

    [BsonElement("raspberry")]
    public bool Raspberry { get; set; }
}
