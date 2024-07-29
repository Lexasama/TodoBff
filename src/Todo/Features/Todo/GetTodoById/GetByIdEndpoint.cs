using MediatR;
using Todo.Abstractions;
using Todo.Abstractions.Messaging;
using Todo.Core.Primitives.Maybe;

namespace Todo.Features.Todo.GetTodoById;

public sealed class GetByIdEndpoint(IMediator mediator, IHttpContextAccessor contextAccessor, LinkGenerator linkGenerator)
    : ApiEndpoint(mediator, contextAccessor, linkGenerator), IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/todos/{id:int}", async (int id) =>
            {
                return await Maybe<GetTodoQuery>
                    .From(new GetTodoQuery(id))
                    .Bind(ExecuteQueryAsync)
                    .Match(
                        todo => Results.Ok(AddLinks(todo)),
                        NotFound);
            })
            .WithTags("Todos")
            .WithName("GetTodo");

        TodoResponse AddLinks(TodoResponse todo)
        {
            todo.Links =
            [
                CreateLink("GetTodo", "self", "GET")
            ];

            return todo;
        }
    }
}

public record GetTodoQuery(int Id) : IQuery<Maybe<TodoResponse>>;