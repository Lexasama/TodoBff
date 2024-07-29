using MediatR;
using Microsoft.AspNetCore.Components;
using Todo.Api;

namespace Todo.Abstractions;

[Route("api")]
public class ApiEndpoint : MessagingModule
{
    protected ApiEndpoint(IMediator mediator, IHttpContextAccessor httpContextAccessor, LinkGenerator linkGenerator) :
        base(mediator)
    {
        _httpContext = httpContextAccessor;
        _linkGenerator = linkGenerator;
    }


    private readonly IHttpContextAccessor _httpContext;

    private readonly LinkGenerator _linkGenerator;

    protected Link CreateLink(string endpointName, string rel, string method)
    {
        var href = _linkGenerator.GetUriByName(_httpContext.HttpContext, endpointName);

        var link =
            new Link(href, rel,
                method
            );
        return link;
    }


    protected IResult Ok(object value) => Results.Ok(value);

    protected IResult NotFound() => Results.NotFound();

    protected IResult BadRequest() => Results.BadRequest();
}