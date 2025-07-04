using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Domain.Ports;

public interface ITranslationService
{
    Task<TransalationsAvailable> GetTranslationsAsync();
}
