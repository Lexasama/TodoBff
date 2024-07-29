using MediatR;

namespace Todo.Abstractions.Messaging;

public interface IQuery<out TResponse> : IRequest<TResponse>;