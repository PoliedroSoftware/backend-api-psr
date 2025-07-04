using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class AddressEntity
{
    [BsonElement("kdx")]
    public required string Kdx { get; set; }
    [BsonElement("neighborhood")]
    public required string Neighborhood { get; set; }
    [BsonElement("zone")]
    public required string Zone { get; set; }
    [BsonElement("city")]
    public required string City { get; set; }
    [BsonElement("country")]
    public required string Country { get; set; }
    [BsonElement("devices")]
    public IEnumerable<DeviceEntity>? Devices { get; set; }
    [BsonElement("gps")]
    public IEnumerable<MarkerEntity>? Gps { get; set; }
}
