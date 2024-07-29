using Todo.Core.Primitives.Maybe;
using Todo.Model;

namespace Todo;

internal sealed class TodoApiRepository : ITodoApiRepository
{
    private readonly HttpClient _httpClient;

    public TodoApiRepository(IHttpClientFactory factory)
    {
        _httpClient = factory.CreateClient("TodoApiClient");
    }

    public async Task<Maybe<IReadOnlyCollection<TodoModel>>> GetAll()
    {
        var result = await _httpClient.GetAsync("/todos");
        if (result.IsSuccessStatusCode)
        {
            return (await result.Content.ReadFromJsonAsync<List<TodoModel>>())!;
        }

        return Maybe<IReadOnlyCollection<TodoModel>>.None;
    }

    public async Task<Maybe<TodoModel>> GetTodoById(int id)
    {
        var result = await _httpClient.GetAsync($"/todos/{id}");

        if (result.IsSuccessStatusCode)
        {
            return (await result.Content.ReadFromJsonAsync<TodoModel>())!;
        }

        return Maybe<TodoModel>.None;
    }
}

internal interface ITodoApiRepository
{
    // public Task<IReadOnlyCollection<TodoModel>> GetAll();
    public Task<Maybe<IReadOnlyCollection<TodoModel>>> GetAll();

    public Task<Maybe<TodoModel>> GetTodoById(int id);
}