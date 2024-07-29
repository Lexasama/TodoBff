using Todo.Abstractions.Messaging;
using Todo.Api;
using Todo.Core.Primitives.Maybe;

namespace Todo.Features.Todo.GetTodos;

internal sealed class GetTodosQueryHandler(ITodoApiRepository repository)
    : IQueryHandler<GetTodosQuery, Maybe<GetTodosResponse>>
{
    public async Task<Maybe<GetTodosResponse>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var todos = (await repository.GetAll()).Value.Select(t => new TodoResponse(t.Id, t.Title, t.Completed, t.Order))
            .ToList();

        return new GetTodosResponse(todos);
    }
}

public sealed record GetTodosResponse(IReadOnlyCollection<TodoResponse> Todos) : LinkedResponse;