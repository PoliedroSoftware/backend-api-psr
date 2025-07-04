using MongoDB.Bson.Serialization.Attributes;
using Poliedro.Psr.Domain.Enum;

namespace Poliedro.Psr.Domain.Entites;

public class DeviceEntity
{
    [BsonElement("guid")]
    public Guid Guid { get; set; }
    [BsonElement("deviceType")]
    public string DeviceType { get; set; }
    [BsonElement("provider")]
    public required string Provider { get; set; }
    [BsonElement("qr")]
    public QrEntity? Qr { get; set; }
    [BsonElement("state")]
    public StateEntity? State { get; set; }
    [BsonElement("reader")]
    public ReaderEntity? Reader { get; set; }
    [BsonElement("readerHistories")]
    public PaginateHistoriesReader? ReaderHistories { get; set; }
}
