using MediatR;
using Todo.Abstractions;
using Todo.Abstractions.Messaging;
using Todo.Core.Primitives.Maybe;

namespace Todo.Features.Todo.GetTodos;

public sealed class GetTodosEndpoint(
    IMediator mediator,
    IHttpContextAccessor contextAccessor,
    LinkGenerator linkGenerator)
    : ApiEndpoint(mediator, contextAccessor, linkGenerator), IEndpoint
{
    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/todos",
                async () =>
                {
                    return await Maybe<GetTodosQuery>
                        .From(new GetTodosQuery())
                        .Bind(ExecuteQueryAsync)
                        .Match(
                            todos => Results.Ok(AddLinks(todos)),
                            BadRequest);
                })
            .Produces<GetTodosResponse>()
            .WithTags("Todos")
            .WithName("GetTodos");


        GetTodosResponse AddLinks(GetTodosResponse response)
        {
            foreach (var todo in response.Todos)
            {
                todo.Links =
                [
                    CreateLink("GetTodo", "self", "GET")
                ];
            }

            response.Links =
            [
                CreateLink("GetTodos", "self", "GET")
            ];

            return response;
        }
    }
}

public sealed record GetTodosQuery : IQuery<Maybe<GetTodosResponse>>;