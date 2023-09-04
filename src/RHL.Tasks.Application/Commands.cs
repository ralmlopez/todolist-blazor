namespace RHL.Tasks.Application;

public class Commands
{
    public static void CreateTask(DataStore dataStore, string task)
    {
        if (task == null) return;
        dataStore.AppendEvent(task, EventType.TaskCreated);
    }

    public static void UpdateTask(DataStore dataStore, int id, string task)
    {
        dataStore.AppendEvent(id, task, EventType.TaskUpdated);
    }

    public static void CompleteTask(DataStore dataStore, int id)
    {
        dataStore.AppendEvent(id, EventType.TaskCompleted);
    }

    public static void RemoveTask(DataStore dataStore, int id)
    {
        dataStore.AppendEvent(id, EventType.TaskRemoved);
    }
}
