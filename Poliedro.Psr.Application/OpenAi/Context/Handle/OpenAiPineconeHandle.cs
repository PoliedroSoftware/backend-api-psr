using MediatR;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Domain.Entites.Pinecone;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Application.OpenAi.Context.Handle;

public class OpenAiPineconeHandle(IPinceconeService pinceconeService) : IRequestHandler<OpenAiPineconeCommand, OpenAiPineconeResponseDto>
{
    public async Task<OpenAiPineconeResponseDto> Handle(OpenAiPineconeCommand request, CancellationToken cancellationToken)
    {
        OpenAiPineconeResponseEntity response = await pinceconeService.Execute(request.Promt);
        return new OpenAiPineconeResponseDto(
            TokenUsage: new TokenUsageDto(
               TotalTokens: response.TokenUsage.TotalTokens, 
               PromptTokens: response.TokenUsage.PromptTokens, 
               CompletionTokens: response.TokenUsage.CompletionTokens),
            Cost: new CostDto(
               PromptCostUsd: response.Cost.PromptCostUsd,
                CompletionCostUsd: response.Cost.CompletionCostUsd, 
                TotalCostUsd: response.Cost.TotalCostUsd),
            Response: response.Response);
    }
}
