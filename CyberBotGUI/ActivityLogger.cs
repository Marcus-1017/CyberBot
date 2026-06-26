// Marcus Johnson
// ST10496028

namespace CyberBotGUI;

// records all significant chatbot actions with timestamps
public class ActivityLogger
{
    private List<string> _log = new();

    // adds a timestamped entry to the log
    public void Log(string action)
    {
        string entry = DateTime.Now.ToString("[HH:mm] ") + action;
        _log.Add(entry);
    }

    // returns the last 10 entries as a numbered list
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

    // this method returns the full log as a numbered list
    public string GetFullLog()
    {
        if (_log.Count == 0)
            return "No activity recorded yet.";

        string result = "Full activity log:\n";
        for (int i = 0; i < _log.Count; i++)
            result += $"{i + 1}. {_log[i]}\n";

        return result;
    }

    // returns total number of log entries
    public int GetCount()
    {
        return _log.Count;
    }
}
