public class HttpMethodsLogic
{
    public static async Task<IResult> GetAllTodos(TodoRepository repository)
    {
        return TypedResults.Ok(await repository.GetAllTodos());
    }
    
    public static async Task<IResult> GetCompleteTodos(bool isComplete, TodoRepository repository)
    {
        var todos = await repository.GetAllTodos();
        return TypedResults.Ok(todos.Where(todo => todo.IsComplete == isComplete).ToList());
    }

    public static async Task<IResult> CreateTodo(Todo todo, TodoRepository repository)
    {
        await repository.CreateTodo(todo);
        return TypedResults.Created($"/todoitems/{todo.Id}", todo);
    }

    public static async Task<IResult> UpdateTodo(int id, Todo inputTodo, TodoRepository repository)
    {
        var todo = await repository.GetTodoById(id);
        if (todo is null)
            return TypedResults.NotFound();

        inputTodo.Id = id;
        await repository.UpdateTodo(inputTodo);
        return TypedResults.NoContent();
    }
    
    public static async Task<IResult> DeleteTodo(int id, TodoRepository repository)
    {
        var rowsAffected = await repository.DeleteTodo(id);
        return rowsAffected > 0 ? TypedResults.NoContent() : TypedResults.NotFound();
    }
}