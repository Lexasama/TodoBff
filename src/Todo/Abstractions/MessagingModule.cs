using MediatR;
using Todo.Abstractions.Messaging;

namespace Todo.Abstractions;

public abstract class MessagingModule
{
    protected MessagingModule(IMediator mediator)
    {
        _mediator = mediator;
    }


    private IMediator _mediator;

    protected Task ExecuteCommand<TResult>(ICommand<TResult> command)
    {
        return _mediator.Send(command);
    }

    protected Task<TResult> ExecuteQueryAsync<TResult>(IQuery<TResult> query)
    {
        return _mediator.Send(query);
    }
}