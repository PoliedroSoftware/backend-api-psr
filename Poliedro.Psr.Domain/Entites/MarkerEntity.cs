using MongoDB.Bson.Serialization.Attributes;

namespace Poliedro.Psr.Domain.Entites;

public class MarkerEntity
{
    [BsonElement("position")]
    public Position? Position { get; set; }

    [BsonElement("code")]
    public string? Code { get; set; }

    [BsonElement("trt")]
    public string? Trt { get; set; }

    [BsonElement("phone")]
    public string? Phone { get; set; }

    [BsonElement("photo")]
    public string? Photo { get; set; }
}

public class Position
{
    [BsonElement("lat")]
    public double Lat { get; set; }

    [BsonElement("lng")]
    public double Lng { get; set; }
}