using Amazon.Runtime.Internal;
using MongoDB.Driver;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Markers
{
    public class MarkerRepository(IMongoDatabase database) : IMarkerRepository
    {

        public IMongoCollection<UserEntity> _readerUsersCollection = database.GetCollection<UserEntity>("users");


        public async  Task<bool> AddMarkerToReaderAsync(Guid readerGuid, MarkerEntity marker, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
