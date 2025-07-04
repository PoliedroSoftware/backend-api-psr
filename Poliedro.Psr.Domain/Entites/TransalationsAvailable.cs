namespace Poliedro.Psr.Domain.Entites;

public class TransalationsAvailable
{
    public Dictionary<string, Dictionary<string, string>> translations { get; set; } 
    public List<Language> Languages { get; set; }
}
