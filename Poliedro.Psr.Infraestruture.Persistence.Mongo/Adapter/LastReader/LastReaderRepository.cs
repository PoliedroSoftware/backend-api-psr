using MongoDB.Driver;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.LastReader;

public class LastReaderRepository(IMongoDatabase database) : ILastReaderRepository
{
    private readonly IMongoCollection<LastReaderEntity> _lastReader = database.GetCollection<LastReaderEntity>("reader");

    public async Task CreateActuatorAsync(LastReaderEntity lastReaderEntity)
    {
        await _lastReader.InsertOneAsync(lastReaderEntity);
    }
}
