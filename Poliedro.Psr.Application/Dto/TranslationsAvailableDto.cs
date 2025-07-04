using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Application.Dto;

public record TranslationsAvailableDto(
    Dictionary<string, Dictionary<string, string>> Translations, 
    List<Language> Languages);
