using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class GpsEntity
{
    [BsonElement("latitude")]
    public double Latitude { get; set; }
    [BsonElement("longitude")]
    public double Longitude { get; set; }
    [BsonElement("accuracy")]
    public double Accuracy { get; set; }
}
