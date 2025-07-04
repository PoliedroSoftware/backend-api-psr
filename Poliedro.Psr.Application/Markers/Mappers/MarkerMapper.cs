using AutoMapper;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Application.Markers.Commands;
using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Application.Markers.Mappers
{
    public class MarkerMapper: Profile
    {
        public MarkerMapper()
        {
            CreateMap<Position, Dto.PositionDto>();

            CreateMap<MarkerEntity, MarkerDto>()
                .ForMember(marker => marker.Position, opt => opt.MapFrom(src => new Dto.PositionDto(src.Position.Lat, src.Position.Lng)));
        }
    }
}
