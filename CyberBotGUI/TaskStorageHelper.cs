// Marcus Johnson
// ST10496028

using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace CyberBotGUI;

public class TaskStorageHelper
{
    private const string FilePath = "tasks.json";

    public List<CyberTask> LoadTasks()
    {
        try
        {
            if (!File.Exists(FilePath))
                return new List<CyberTask>();

            string json = File.ReadAllText(FilePath);
            return JsonConvert.DeserializeObject<List<CyberTask>>(json) ?? new List<CyberTask>();
        }
        catch
        {
            return new List<CyberTask>();
        }
    }

    public void SaveTasks(List<CyberTask> tasks)
    {
        try
        {
            string json = JsonConvert.SerializeObject(tasks, Formatting.Indented);
            File.WriteAllText(FilePath, json);
        }
        catch { }
    }

    public int AddTask(string title, string description, string reminder)
    {
        List<CyberTask> tasks = LoadTasks();
        int newId = tasks.Count > 0 ? tasks[^1].Id + 1 : 1;

        tasks.Add(new CyberTask
        {
            Id = newId,
            Title = title,
            Description = description,
            Reminder = reminder,
            IsComplete = false,
            CreatedAt = DateTime.Now.ToString("yyyy-MM-dd HH:mm")
        });

        SaveTasks(tasks);
        return newId;
    }

    public void MarkAsComplete(int id)
    {
        List<CyberTask> tasks = LoadTasks();
        CyberTask? task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsComplete = true;
            SaveTasks(tasks);
        }
    }

    public void MarkAsIncomplete(int id)
    {
        List<CyberTask> tasks = LoadTasks();
        CyberTask? task = tasks.FirstOrDefault(t => t.Id == id);
        if (task != null)
        {
            task.IsComplete = false;
            SaveTasks(tasks);
        }
    }

    public void DeleteTask(int id)
    {
        List<CyberTask> tasks = LoadTasks();
        tasks.RemoveAll(t => t.Id == id);
        SaveTasks(tasks);
    }
}
