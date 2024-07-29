using Todo.Api;

namespace Todo.Features.Todo;

public record TodoResponse(
    int Id,
    string Title,
    bool Completed,
    int Order) : LinkedResponse;