using MediatR;

namespace Application.Contracts;

internal interface IQuery<out TResponse> : IRequest<TResponse>
{
}
