// Marcus Johnson
// ST10496028

using System.Linq;

namespace CyberBotGUI;

public class ActivityLogger
{
    private List<string> _log = new();

    public void Log(string action)
    {
        string entry = DateTime.Now.ToString("[HH:mm] ") + action;
        _log.Add(entry);
    }

    public string GetRecentLog(int count = 10)
    {
        if (_log.Count == 0)
            return "No activity recorded yet.";

        var recent = _log.TakeLast(count).ToList();
        string result = "Here's a summary of recent actions:\n";
        for (int i = 0; i < recent.Count; i++)
            result += $"{i + 1}. {recent[i]}\n";

        return result;
    }

    public string GetFullLog()
    {
        if (_log.Count == 0)
            return "No activity recorded yet.";

        string result = "Full activity log:\n";
        for (int i = 0; i < _log.Count; i++)
            result += $"{i + 1}. {_log[i]}\n";

        return result;
    }

    public int GetCount()
    {
        return _log.Count;
    }
}
