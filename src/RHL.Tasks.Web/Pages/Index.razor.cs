namespace RHL.Tasks.Web.Pages;

using Microsoft.AspNetCore.Components;
using RHL.Tasks.Application;
using RHL.Tasks.Web.Components;

public partial class Index : ComponentBase
{
    [Inject]
    DataStore DataStore { get; set; } = default!;

    string? NewTask;
    string? UpdatedTask;
    TaskItem? SelectedTask;
    IEnumerable<TaskItem> TaskItems { get; set; } = default!;
    IEnumerable<TaskItem> CompletedItems { get; set; } = default!;

    public Tasks Tasks { get; set; } = default!;

    protected override void OnInitialized()
    {
        GetTasks();
        GetCompleted();
    }

    void GetCompleted()
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
        if (SelectedTask != null && !string.IsNullOrEmpty(UpdatedTask))
        {
            Commands.UpdateTask(DataStore, SelectedTask.Id, UpdatedTask);
            Tasks.Clear();
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
