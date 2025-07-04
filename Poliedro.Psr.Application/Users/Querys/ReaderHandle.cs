using AutoMapper;
using MediatR;
using Poliedro.Psr.Application.Dto;
using Poliedro.Psr.Domain.Entites;
using Poliedro.Psr.Domain.Ports;
using System.Collections.Generic;

namespace Poliedro.Psr.Application.Users.Querys;

public class ReaderHandle(IReaderRepository _readerRepository, IMapper _mapper) : IRequestHandler<ReaderQuerys, IEnumerable<UserEntity>>
{
    public async Task<IEnumerable<UserEntity>> Handle(ReaderQuerys request, CancellationToken cancellationToken)
    {
       List<UserEntity> userEntities = await _readerRepository.ExecuteAsync();
        //IEnumerable<UserDto> responseReaders = [
        //    new UserDto(
        //        Id : Guid.NewGuid(),
        //        Person: new PersonDto(
        //            Guid: userEntities[0].Person.Guid,
        //            Name: userEntities[0].Person.Name,
        //            LastName: userEntities[0].Person.LastName,
        //            Phone: userEntities[0].Person.Phone,
        //            Email: userEntities[0].Person.Email
        //            ),
        //        Address: new AddressDto(
        //            Kdx: userEntities[0].Address.Select(
        //            ))
           
        //    ];
        
       //var mapper = _mapper.Map<IEnumerable<UserDto>>(userEntities);
        return userEntities;
    }
}
