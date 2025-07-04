namespace Poliedro.Psr.Domain.Entites.Pinecone;

public record OpenAiPineconeResponseEntity(TokenUsageEntity TokenUsage, CostEntity Cost, string Response);

