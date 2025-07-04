using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Poliedro.Psr.Domain.Entites;

public class RootObject
{
    [JsonPropertyName("_embedded")]
    public  Embedded translations { get; set; }

    [JsonPropertyName("selectedLanguages")]
    public List<Language> SelectedLanguages { get; set; }

    public class Embedded
    {
        [JsonPropertyName("keys")]
        public List<Translation> Keys { get; set; }
    }
}
