namespace Poliedro.Psr.Application.Dto;

public record OpenAiPineconeResponseDto(TokenUsageDto TokenUsage, CostDto Cost, string Response);


