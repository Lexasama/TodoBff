using MediatR;
using Todo.Abstractions;
using Todo.Abstractions.Messaging;
using Todo.Core.Primitives.Maybe;

namespace Todo.Features.Todo.GetTodos;

public sealed class GetTodosEndpoint(IMediator mediator, LinkGenerator linkGenerator) : ApiEndpoint(mediator), IEndpoint
{
    public LinkGenerator LinkGenerator { get; } = linkGenerator;

    public void MapEndpoint(IEndpointRouteBuilder app)
    {
        app.MapGet("/todos", async () =>
                await Maybe<GetTodosQuery>
                    .From(new GetTodosQuery())
                    .Bind(ExecuteQueryAsync)
                    .Match(Ok, BadRequest))
            .Produces<GetTodosResponse>()
            .WithTags("Todos")
            .WithName("GetTodos");
    }
}

public sealed record GetTodosQuery : IQuery<Maybe<GetTodosResponse>>;