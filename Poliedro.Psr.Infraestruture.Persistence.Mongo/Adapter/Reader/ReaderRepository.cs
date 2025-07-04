using MongoDB.Driver;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Reader;

public class ReaderRepository(IMongoDatabase database) : IReaderRepository
{
    public IMongoCollection<UserEntity> _user = database.GetCollection<UserEntity>("users");

    public async Task<List<UserEntity>> ExecuteAsync() =>
        await _user.Find(FilterDefinition<UserEntity>.Empty).ToListAsync();
}
