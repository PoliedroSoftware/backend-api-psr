using Poliedro.Psr.Domain.Entites.Pinecone;

namespace Poliedro.Psr.Domain.Ports;

public interface IPinceconeService
{
    Task<OpenAiPineconeResponseEntity> Execute(string prompt);
}
