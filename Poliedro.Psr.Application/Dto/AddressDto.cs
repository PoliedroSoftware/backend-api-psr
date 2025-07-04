namespace Poliedro.Psr.Application.Dto;

public record AddressDto(
string Kdx,
string Neighborhood,
string Zone,
string City,
string Country,
List<DeviceDto> Devices,
List<MarkerDto> Gps);
