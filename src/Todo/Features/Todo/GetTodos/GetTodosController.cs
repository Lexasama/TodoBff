using MediatR;
using Microsoft.AspNetCore.Mvc;
using Todo.Abstractions;
using Todo.Api;
using Todo.Core.Primitives.Maybe;

namespace Todo.Features.Todo.GetTodos;

public class GetTodosController : ApiController
{
    private readonly IMediator _sender;
    private readonly LinkGenerator _linkGenerator;

    public GetTodosController(IMediator mediator, LinkGenerator linkGenerator) : base(mediator)
    {
        _sender = mediator;
        _linkGenerator = linkGenerator;
    }


    [HttpGet("/test", Name = "GetAll")]
    [ProducesResponseType(typeof(GetTodosResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetAll()
    {
        var result = await Maybe<GetTodosQuery>
            .From(new GetTodosQuery())
            .Bind(query => _sender.Send(query));

        foreach (var todo in result.Value.Todos)
        {
            todo.Links = CreateLinksForTodo(todo.Id, "");
        }

        var wrapper = new LinkCollectionWrapper<TodoResponse>(result.Value.Todos);

        return Ok(CreateLinksForTodos(wrapper));
    }

    [HttpGet("/test/{id:int}")]
    public async Task<IActionResult> GetOne(int id)
    {
        return Ok();
    }

    private IEnumerable<Link> CreateLinksForTodo(int id, string fields = "")
    {
        var links = new List<Link>
        {
            new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetOne), values: new { id, fields }),
                "self",
                "GET")
        };

        return links;
    }

    private LinkCollectionWrapper<TodoResponse> CreateLinksForTodos(LinkCollectionWrapper<TodoResponse> todos)
    {
        todos.Links.Add(
            new Link(_linkGenerator.GetUriByAction(HttpContext, nameof(GetAll), values: new { }),
                "self",
                "GET"
            )
        );

        return todos;
    }
}