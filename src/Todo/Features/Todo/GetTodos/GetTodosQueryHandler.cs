using Todo.Abstractions.Messaging;
using Todo.Api;
using Todo.Core.Primitives.Maybe;

namespace Todo.Features.Todo.GetTodos;

internal sealed class GetTodosQueryHandler : IQueryHandler<GetTodosQuery, Maybe<GetTodosResponse>>
{
    public async Task<Maybe<GetTodosResponse>> Handle(GetTodosQuery request, CancellationToken cancellationToken)
    {
        var todos = new[]
        {
            new(0, "todo1", false, 0),
            new TodoResponse(1, "todo2", false, 1),
            new TodoResponse(2, "todo3", false, 2)
        };
        //var todos = Array.Empty<TodoModel>();
        var response = new GetTodosResponse(todos);
        return response;
    }
}

public sealed record GetTodosResponse(IReadOnlyCollection<TodoResponse> Todos) : LinkedResponse;