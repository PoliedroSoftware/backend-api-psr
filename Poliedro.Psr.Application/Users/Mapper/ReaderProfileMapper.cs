using AutoMapper;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Application.Users.Mapper;

public class ReaderProfileMapper : Profile
{
    protected ReaderProfileMapper()
    {
       
        CreateMap<UserEntity, UserDto>()
            .ForMember(dest => dest.Person, opt => opt.MapFrom(src => src.Person))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address));

        CreateMap<PersonEntity, PersonDto>();
        CreateMap<AddressEntity, AddressDto>();
        CreateMap<DeviceEntity, DeviceDto>();
        CreateMap<QrEntity, QrDetailsDto>();
        CreateMap<StateEntity, StateDevice>();
        CreateMap<ReaderEntity, ReaderDto>();
        CreateMap<PaginateHistoriesReader, PaginatedReaderHistoryDto>();
        CreateMap<ReaderHistoriesEntity, ReaderHistoryDto>();

    }
}
