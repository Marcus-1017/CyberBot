// Marcus Johnson
// ST10496028

namespace CyberBotGUI;

public class TaskManager
{
    private TaskStorageHelper _storage = new();
    private ActivityLogger _logger;

    public TaskManager(ActivityLogger logger)
    {
        _logger = logger;
    }

    // this method adds a task and logs the action
    public string AddTask(string title, string description, string reminder)
    {
        _storage.AddTask(title, description, reminder);
        string log = string.IsNullOrWhiteSpace(reminder)
            ? $"Task added: '{title}'"
            : $"Task added: '{title}' (Reminder: {reminder})";
        _logger.Log(log);
        return log;
    }

    // returns all tasks
    public List<CyberTask> GetAllTasks()
    {
        return _storage.LoadTasks();
    }

    // marks a task complete and logs the action
    public void MarkAsComplete(int id)
    {
        var tasks = _storage.LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        _storage.MarkAsComplete(id);
        if (task != null)
            _logger.Log($"Task marked complete: '{task.Title}'");
    }

    // marks a task incomplete and logs the action
    public void MarkAsIncomplete(int id)
    {
        var tasks = _storage.LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        _storage.MarkAsIncomplete(id);
        if (task != null)
            _logger.Log($"Task marked incomplete: '{task.Title}'");
    }
    // deletes a task and logs the action
    public void DeleteTask(int id)
    {
        var tasks = _storage.LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        _storage.DeleteTask(id);
        if (task != null)
            _logger.Log($"Task deleted: '{task.Title}'");
    }
}
