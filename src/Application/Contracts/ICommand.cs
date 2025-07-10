using MediatR;

namespace Application.Contracts;

internal interface ICommand<out TResponse> : IRequest<TResponse>
{
}
