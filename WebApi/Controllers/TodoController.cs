namespace WebApi.Controllers;
using Infrastructurre.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    TodoAppService todoService;

    public TodoController(TodoAppService todoService)
    {
        this.todoService = todoService;
    }
    [HttpGet("GetTodo")]
    public List<TodoApp> GetTodos()
    {
        return todoService.GetTodos();
    }
    [HttpPost("AddTodo")]
    public void AddTodo(TodoApp todo)
    {
        todoService.AddTodo(todo);
    }
    [HttpPut("UpdateTodo")]
    public void UpdateTodo(TodoApp todo)
    {
        todoService.UpdateTodo(todo);
    }
    [HttpDelete("DeleteTodo")]

    public void DeleteTodo(int id)
    {
         todoService.DeleteTodo(id);
    }
}
