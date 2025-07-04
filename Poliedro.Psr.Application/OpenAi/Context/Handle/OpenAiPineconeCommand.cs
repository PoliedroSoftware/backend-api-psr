using MediatR;
using Poliedro.Psr.Application.Dto;

namespace Poliedro.Psr.Application.OpenAi.Context.Handle;

public record OpenAiPineconeCommand(string Promt):  IRequest<OpenAiPineconeResponseDto>;

