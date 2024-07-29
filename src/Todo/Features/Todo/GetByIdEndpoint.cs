using MediatR;
using Todo.Abstractions;
using Todo.Abstractions.Messaging;
using Todo.Core.Primitives.Maybe;

namespace Todo.Features.Todo;

public sealed class GetByIdEndpoint(IMediator mediator) : ApiEndpoint(mediator), IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/todos/{id:int}", async (int id) =>
                await Maybe<GetTodoQuery>
                    .From(new GetTodoQuery(id))
                    .Bind(query => ExecuteQueryAsync(query))
                    .Match(
                        todo=> Results.Ok(CreateLink(todo)),
                        
                        NotFound))
            .WithTags("Todos")
            .WithName("GetTodo");
    }

    private object CreateLink(TodoResponse todo)
    {
        
    }
    
    
}

public record GetTodoQuery(int Id) : IQuery<Maybe<TodoResponse>>;