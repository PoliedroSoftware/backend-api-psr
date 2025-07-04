using MediatR;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Application.Translations.Query;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;

namespace Poliedro.Psr.Application.Translations.Handle;

public class GetTranslationsHandler(ITranslationService _translationService) : IRequestHandler<GetTranslationsQuery, TranslationsAvailableDto>
{
    private static readonly Dictionary<string, string> FlagToCountryCodeMap = new()
    {
        { "🇺🇸", "US" },
        { "🇨🇴", "CO" }
    };

    public async Task<TranslationsAvailableDto> Handle(GetTranslationsQuery request, CancellationToken cancellationToken)
    {
        var responseEntity = await _translationService.GetTranslationsAsync();
        MapCountryCodes(responseEntity.Languages);

        return new TranslationsAvailableDto(
            Translations: responseEntity.translations,
            Languages: responseEntity.Languages);
    }

    private void MapCountryCodes(IEnumerable<Language> languages)
    {
        foreach (var language in languages)
        {
            foreach (var flag in FlagToCountryCodeMap)
            {
                if (language.FlagEmoji.Contains(flag.Key))
                {
                    language.Code = flag.Value;
                    break;
                }
            }
        }
    }
}

