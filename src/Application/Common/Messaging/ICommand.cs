using MediatR;

namespace Application.Common.Messaging;

public interface ICommand : IRequest
{
}

public interface ICommand<TResponse> : IRequest<TResponse>
{
}
