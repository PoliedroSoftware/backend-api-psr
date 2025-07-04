using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;
using System.Text.Json;

namespace Poliedro.Psr.Infraestruture.Persistence.Mongo.Adapter.Translations;

public class TranslationsService(HttpClient _httpClient) : ITranslationService
{
    private const string BaseUrl = "http://159.89.239.32:8085/v2/projects/2/translations?size=1000";
    private const string ApiKey = "tgpak_gjpwqyjugf3domtemjsdi4trou3wqy3mgfvtqzldn4ygo";

    public async Task<TransalationsAvailable> GetTranslationsAsync()
    {
        var request = new HttpRequestMessage(HttpMethod.Get, BaseUrl);
        request.Headers.Add("Accept", "application/json");
        request.Headers.Add("X-API-Key", ApiKey);

        var response = await _httpClient.SendAsync(request);

        if (!response.IsSuccessStatusCode)
        {
            throw new Exception($"Error fetching translations: {response.StatusCode}");
        }

        var responseBody = await response.Content.ReadAsStringAsync();
        RootObject data = JsonSerializer.Deserialize<RootObject>(responseBody);

        var translationsByLanguage = new Dictionary<string, Dictionary<string, string>>();

        foreach (var key in data.translations.Keys)
        {
            foreach (var translation in key.Translations)
            {
                var language = translation.Key;
                var translationDetail = translation.Value;

                if (!translationsByLanguage.ContainsKey(language))
                {
                    translationsByLanguage[language] = new Dictionary<string, string>();
                }

                translationsByLanguage[language][key.KeyName] = translationDetail.Text;
            }
        }

        return new TransalationsAvailable() { translations = translationsByLanguage, Languages = data.SelectedLanguages };
    }
}
