namespace Domain.Dtos;

public class TodoApp
{
    public int Id { get; set; }
    public string? TodoName { get; set; }
    public Status Status { get; set; }
    public string StatusName { get { return Status.ToString(); } }

}
