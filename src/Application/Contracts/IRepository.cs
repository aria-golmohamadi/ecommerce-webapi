﻿using Ardalis.Specification;
using Domain.Common;

namespace Application.Contracts;

public interface IRepository<T> : IRepositoryBase<T> where T : class, IAggregateRoot
{
}
