namespace Todo.Model;

public record TodoModel(int Id, string Title, bool Completed, int Order, string Url);