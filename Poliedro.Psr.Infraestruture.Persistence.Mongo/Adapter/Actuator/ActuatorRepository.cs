using MongoDB.Driver;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Actuator;

public class ActuatorRepository(IMongoDatabase database) : IActuatorRepository
{
    private readonly IMongoCollection<ActuatorEntity> _actuators = database.GetCollection<ActuatorEntity>("actuator");

    public async Task<ActuatorEntity> GetAllActuatorsAsync()
    {
        return await _actuators.Find(act => true).FirstOrDefaultAsync();
    }

    public async Task<ActuatorEntity> GetActuatorByIdAsync(string id)
    {
        return await _actuators.Find(act => act.Id == id).FirstOrDefaultAsync();
    }

    public async Task CreateActuatorAsync(ActuatorEntity actuator)
    {
        await _actuators.InsertOneAsync(actuator);
    }

    public async Task UpdateActuatorAsync(string id, ActuatorEntity  actuator)
    {
        await _actuators.ReplaceOneAsync(act => act.Id == id, actuator);
    }

    public async Task DeleteActuatorAsync(string id)
    {
        await _actuators.DeleteOneAsync(act => act.Id == id);
    }
}
