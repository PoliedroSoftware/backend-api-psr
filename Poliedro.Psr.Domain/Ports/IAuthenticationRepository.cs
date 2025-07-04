using Poliedro.Psr.Domain.Dto;
using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Domain.Ports;

public interface IAuthenticationRepository
{
    Task SaveAsync(AuthenticationEnTity authenticationEnTity);
    Task<AuthenticationEnTity> GetByIdAsync(PhoneDto phone);
    Task<bool> PhoneExists(PhoneDto phone);
}

