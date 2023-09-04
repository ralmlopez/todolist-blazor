namespace RHL.Tasks.Application;

public class TaskItem
{
    public int Id { get; private set; }
    public string? Task { get; private set; }

    public TaskItem(int id, string? task)
    {
        Id = id;
        Task = task;
    }

}

