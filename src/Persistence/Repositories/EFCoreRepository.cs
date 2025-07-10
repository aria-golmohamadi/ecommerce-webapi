using Application.Contracts;
using Ardalis.Specification.EntityFrameworkCore;
using Domain.Common;
using Persistence.Contexts;

namespace Persistence.Repositories;

internal class EFCoreRepository<T>(ApplicationDbContext context) : RepositoryBase<T>(context), IRepository<T> where T : class, IAggregateRoot
{
}
