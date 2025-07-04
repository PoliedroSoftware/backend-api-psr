namespace Poliedro.Psr.Application.Dto;

public record UserDto(
Guid Id,
PersonDto Person,
IEnumerable<AddressDto> Address,
IEnumerable<string> Roles
);
