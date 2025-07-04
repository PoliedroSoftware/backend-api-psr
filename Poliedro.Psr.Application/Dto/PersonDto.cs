namespace Poliedro.Psr.Application.Dto;

public record PersonDto(
 Guid Guid,
 string Name,
 string LastName,
 string Phone,
 string? Email
);
