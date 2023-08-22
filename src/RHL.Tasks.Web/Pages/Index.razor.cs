namespace RHL.Tasks.Web.Pages;

using Microsoft.AspNetCore.Components;
using RHL.Tasks.Application;
using RHL.Tasks.Web.Components;

public partial class Index : ComponentBase
{
    [Inject]
    DataStore? DataStore { get; set; }

    string? NewTask;
    string? UpdatedTask;
    TaskItem? SelectedTask;
    IEnumerable<TaskItem>? TaskItems { get; set; }
    IEnumerable<TaskItem>? CompletedItems { get; set; }

    public Tasks? Tasks { get; set; }
    public Tasks? Completed { get; set; }

    protected override void OnInitialized()
    {
        GetTasks();
        GetCompleted();
    }

    private void GetCompleted()
    {
        CompletedItems = Projections.GetTasksCompleted(DataStore);
    }

    void GetTasks()
    {
        TaskItems = Projections.GetTasksPending(DataStore);
    }

    void TaskSelected(TaskItem task)
    {
        SelectedTask = task;
        UpdatedTask = task.Task;
    }

    void TaskRemoved(TaskItem task)
    {
        Commands.RemoveTask(DataStore, task.Id);
        Reset();
        GetTasks();
        GetCompleted();
    }

    void AddTask()
    {
        if (!string.IsNullOrEmpty(NewTask))
        {
            Commands.CreateTask(DataStore, NewTask);
            NewTask = string.Empty;
            GetTasks();
        }
    }

    void UpdateTask()
    {
        if (SelectedTask != null)
        {
            Commands.UpdateTask(DataStore, SelectedTask.Id, UpdatedTask);
            Reset();
            GetTasks();
        }
    }

    void CompleteTask()
    {
        if (SelectedTask != null)
        {
            Commands.CompleteTask(DataStore, SelectedTask.Id);
            Reset();
            GetTasks();
            GetCompleted();
        }
    }

    void Reset()
    {
        UpdatedTask = string.Empty;
        SelectedTask = null;
    }
}
