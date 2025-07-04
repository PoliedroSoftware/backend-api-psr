namespace Poliedro.Psr.Application.Dto
{
    public record MarkerDto(
      PositionDto Position,
      string Code,
      string Trt,
      string? Phone,
      string? Photo
    );

    public record PositionDto(double Lat, double Lng);
}
