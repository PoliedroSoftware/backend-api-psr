using MediatR;
using Poliedro.Psr.Application.Dto;

namespace Poliedro.Psr.Application.Translations.Query;

public record GetTranslationsQuery() : IRequest<TranslationsAvailableDto>;

