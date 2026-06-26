// Marcus Johnson
// ST10496028

using System.Linq;

namespace CyberBotGUI;

public class TaskManager
{
    private TaskStorageHelper _storage = new();
    private ActivityLogger _logger;

    public TaskManager(ActivityLogger logger)
    {
        _logger = logger;
    }

    public int AddTask(string title, string description, string reminder)
    {
        _storage.AddTask(title, description, reminder);
        var tasks = _storage.LoadTasks();
        int id = tasks.Count > 0 ? tasks[^1].Id : 1;
        
        string log = string.IsNullOrWhiteSpace(reminder)
            ? $"Task added: '{title}'"
            : $"Task added: '{title}' (Reminder: {reminder})";
        _logger.Log(log);
        return id;
    }

    public List<CyberTask> GetAllTasks()
    {
        return _storage.LoadTasks();
    }

    public void MarkAsComplete(int id)
    {
        var tasks = _storage.LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        _storage.MarkAsComplete(id);
        if (task != null)
            _logger.Log($"Task marked complete: '{task.Title}'");
    }

    public void MarkAsIncomplete(int id)
    {
        var tasks = _storage.LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        _storage.MarkAsIncomplete(id);
        if (task != null)
            _logger.Log($"Task marked incomplete: '{task.Title}'");
    }

    public void DeleteTask(int id)
    {
        var tasks = _storage.LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        _storage.DeleteTask(id);
        if (task != null)
            _logger.Log($"Task deleted: '{task.Title}'");
    }

    public void SetReminder(int id, string reminder)
    {
        var tasks = _storage.LoadTasks();
        var task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.Reminder = reminder;
            _storage.SaveTasks(tasks);
            _logger.Log($"Reminder set for task '{task.Title}': {reminder}");
        }
    }
}
