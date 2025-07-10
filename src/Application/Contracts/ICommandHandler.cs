using MediatR;

namespace Application.Contracts;

internal interface ICommandHandler<in TCommand, TResponse> : IRequestHandler<TCommand, TResponse> where TCommand : ICommand<TResponse>
{
}
