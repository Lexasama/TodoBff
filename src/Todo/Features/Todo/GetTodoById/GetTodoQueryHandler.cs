using Todo.Abstractions.Messaging;
using Todo.Core.Primitives.Maybe;
using Todo.Model;

namespace Todo.Features.Todo.GetTodoById;

internal sealed class GetTodoQueryHandler(ITodoApiRepository repository)
    : IQueryHandler<GetTodoQuery, Maybe<TodoResponse>>
{
    public async Task<Maybe<TodoResponse>> Handle(GetTodoQuery request, CancellationToken cancellationToken)
    {
        var todo = await repository.GetTodoById(request.Id);

        return From(todo);
    }

    private static TodoResponse From(TodoModel model)
    {
        return new TodoResponse(
            model.Id,
            model.Title,
            model.Completed,
            model.Order
        );
    }
}