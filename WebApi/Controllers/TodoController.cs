namespace WebApi.Controllers;
using Infrastructurre.Services;
using Microsoft.AspNetCore.Mvc;
using Domain.Dtos;
using System.IO;

[ApiController]
[Route("[controller]")]
public class TodoController : ControllerBase
{
    TodoAppService todoService;
    readonly IWebHostEnvironment _environment;

    public TodoController(IWebHostEnvironment environment, TodoAppService todoService)
    {
        _environment = environment;
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
    [HttpGet("GetByStatus")]
    public List<TodoApp> GetByStatus(int status)
    {
        return todoService.GetByStatus(status);
    }
    [HttpGet("GetWithFiles")]
    public List<GetTodoDto> GetTodoWithFile()
    {
        return todoService.GetTodosWithFile();
    }
    [HttpPost("UploadFile")]
    public GetTodoDto UploadFile([FromForm] AddTodoDto todo, string folderName)
    {
        var path = Path.Combine(_environment.WebRootPath, folderName);
        if (!Directory.Exists(path))
        {
            Directory.CreateDirectory(path);
        }
        return todoService.UploadFile(todo, folderName);
    }
    [HttpPut("UpdateTodo")]
    public GetTodoDto UpdateTodo(AddTodoDto todo)
    {
        return todoService.UpdateFile(todo);
    }
}
