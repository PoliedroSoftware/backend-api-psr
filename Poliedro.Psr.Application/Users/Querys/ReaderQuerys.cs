using MediatR;
using Poliedro.Psr.Domain.Entites;

namespace Poliedro.Psr.Application.Users.Querys;

public record ReaderQuerys(): IRequest<IEnumerable<UserEntity>>;

