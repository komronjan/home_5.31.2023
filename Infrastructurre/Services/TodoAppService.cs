namespace Infrastructurre.Services;
using Infrastructurre.Context;
using Dapper;
using Domain.Dtos;
public class TodoAppService
{
    DapperContext dapperContext;
    private readonly IFileService _fileService;

    public TodoAppService(DapperContext dapperContext, IFileService fileService)
    {
        this.dapperContext = dapperContext;
        _fileService = fileService;
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
    public List<TodoApp> GetByStatus(int status)
    {
        using (var conn = dapperContext.CreateConnection())
        {
            var sql = $"select id as Id,todo_name as TodoName,status as Status from TODOApp where status={status};";
            var result = conn.Query<TodoApp>(sql);
            return result.ToList();
        }
    }
    public List<GetTodoDto> GetTodosWithFile()
    {
        using (var conn = dapperContext.CreateConnection())
        {
            var sql = $"select id as Id, todo_name as TodoName,status as Status, file_name as FileName from TODOApp;";
            var result = conn.Query<GetTodoDto>(sql);
            return result.ToList();
        }
    }
    public GetTodoDto UploadFile(AddTodoDto todo, string folderName)
    {
        using (var conn = dapperContext.CreateConnection())
        {
            var filename = _fileService.CreateFileName(folderName, todo.File);
            var sql = $"Insert into TODOApp(todo_name,status,file_name) values(@todoName,@status,@filename) returning id;";
            var result = conn.ExecuteScalar<int>(sql, new
            {
                todo.TodoName,
                todo.Status,
                filename
            });
            return new GetTodoDto()
            {
                TodoName = todo.TodoName,
                Status = todo.Status,
                FileName = filename,
                Id = result
            };
        }
    }
    public GetTodoDto UpdateFile(AddTodoDto todo)
    {
        using (var conn = dapperContext.CreateConnection())
        {
            var sqlp = "select id as Id, todo_name as TodoName,status as Status, file_name as FileName from TODOApp where id = @id;";
            var existing = conn.QuerySingle<GetTodoDto>(sqlp,new{ todo.Id });
            if (existing != null)
            { 
                return new GetTodoDto();
            }
            string filename= null ;
            if (todo.File!= null && existing.FileName!=null){
                _fileService.DeleteFile("images",existing.FileName);
                filename =_fileService.CreateFileName("images",todo.File);

            }
            else if (todo.File!= null && existing.FileName==null){
                filename= _fileService.CreateFileName("images",todo.File);
            }
            var sql= $"Update TODOApp set todo_name=@TodoName, status=@Status where id = @Id";
            if(todo.File!=null){
                sql= $"Update TODOApp set todo_name=@TodoName, status=@Status, file_name=@filename where id = @Id";
            }
            var result = conn.Execute(sql,new {
                todo.TodoName,
                todo.Status,
                filename,
                todo.Id          
            });
            return new GetTodoDto (){
                TodoName = todo.TodoName,
                Status = todo.Status,
                FileName = filename,
                Id = todo.Id
            };

        }
    }
}
