using Microsoft.AspNetCore.Components;
using RHL.Tasks.Application;

namespace RHL.Tasks.Web.Components;

public partial class Tasks : ComponentBase
{
    TaskItem? SelectedItem { get; set; }

    [Parameter]
    public bool AllowSelect { get; set; } = true;

    [Parameter]
    public IEnumerable<TaskItem>? TaskItems { get; set; }

    [Parameter]
    public EventCallback<TaskItem> OnTaskSelected { get; set; }

    [Parameter]
    public EventCallback<TaskItem> OnTaskRemoved { get; set; }

    async Task SelectTask(TaskItem taskItem)
    {
        SelectedItem = taskItem;
        StateHasChanged();
        await OnTaskSelected.InvokeAsync(taskItem);
    }

    async Task RemoveTask(TaskItem taskItem)
    {
        await OnTaskRemoved.InvokeAsync(taskItem);
    }
}
