using MediatR;
using Microsoft.AspNetCore.Components;

namespace Todo.Abstractions;

[Route("api")]
public class ApiEndpoint : MessagingModule
{
    protected IResult Ok(object value) => Results.Ok(value);

    protected IResult NotFound() => Results.NotFound();

    protected IResult BadRequest() => Results.BadRequest();

    protected ApiEndpoint(IMediator mediator) : base(mediator)
    {
    }
}