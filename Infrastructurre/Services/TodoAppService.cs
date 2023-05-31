namespace Infrastructurre.Services;
using Infrastructurre.Context;
using Dapper;
using Domain.Dtos;
public class TodoAppService
{
    DapperContext dapperContext;
    public TodoAppService(DapperContext dapperContext)
    {
        this.dapperContext = dapperContext;
    }
    public List<TodoApp> GetTodos()
    {
        using (var conn = dapperContext.CreateConnection())
        {
            var sql = $"select id as Id , todo_name as TodoName, status as Status from TODOApp;";
            var result = conn.Query<TodoApp>(sql);
            return result.ToList();
        }
    }
    public void AddTodo(TodoApp todo)
    {
        using (var conn = dapperContext.CreateConnection())
        {
            var sql = "INSERT INTO TODOApp(todo_name, status) values(@TodoName, @Status);";
            var result = conn.Execute(sql, todo);
        }
    }
    public void UpdateTodo(TodoApp todo)
    {

        using (var conn = dapperContext.CreateConnection())
        {
            var sql = $"UPDATE TODOApp SET todo_name = '{todo.TodoName}', status = {(int)todo.Status} where id={todo.Id};";
            var result = conn.Execute(sql);

        }
    }
    public void DeleteTodo(int id)
    {
        using (var conn = dapperContext.CreateConnection())
        {
            var sql = $"Delete from TODOApp where id={id};";
            var result = conn.Execute(sql);
        }
    }
}
