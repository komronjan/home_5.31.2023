namespace Domain.Dtos;
using Microsoft.AspNetCore.Http;


public class AddTodoDto : TodoApp
{
    public IFormFile File { get; set; }
}