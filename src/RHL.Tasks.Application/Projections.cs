namespace RHL.Tasks.Application;

public class Projections
{
    public static IEnumerable<TaskItem> GetTasksPending(DataStore dataStore)
    {
        var allTasks = dataStore.GetAllEvents().ToList();
        var tasksCompletedOrRemoved = new HashSet<int>(
                allTasks
                .Where(x => x.EventType == EventType.TaskCompleted || x.EventType == EventType.TaskRemoved)
                .Select(x => x.Id));

        return allTasks
            .Where(x => (x.EventType == EventType.TaskCreated || x.EventType == EventType.TaskUpdated)
                    && !tasksCompletedOrRemoved.Contains(x.Id))
            .GroupBy(x => x.Id)
            .Select(g => g.OrderBy(c => c.Id).ThenByDescending(c => c.Created).First())
            .Select(x => new TaskItem(x.Id, x.Task));
    }

    public static IEnumerable<TaskItem> GetTasksCompleted(DataStore dataStore)
    {
        var allTasks = dataStore.GetAllEvents().ToList();
        var tasksCompleted = new HashSet<int>(allTasks.Where(x => x.EventType == EventType.TaskCompleted).Select(x => x.Id));
        var tasksRemoved = new HashSet<int>(allTasks.Where(x => x.EventType == EventType.TaskRemoved).Select(x => x.Id));

        return allTasks
            .Where(x => (x.EventType == EventType.TaskCreated || x.EventType == EventType.TaskUpdated)
                    && tasksCompleted.Contains(x.Id)
                    && !tasksRemoved.Contains(x.Id))
            .GroupBy(x => x.Id)
            .Select(g => g.OrderBy(c => c.Id).ThenByDescending(c => c.Created).First())
            .Select(x => new TaskItem(x.Id, x.Task));
    }
}
