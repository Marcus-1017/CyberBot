using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace CyberBotGUI;

public partial class MainWindow : Window
{
    ChatBot bot = new();
    bool nameEntered = false;
    ActivityLogger logger = new();
    TaskManager taskManager;
    private QuizUIHelper quizHelper;
    private int _pendingTaskId = -1;

    public MainWindow()
    {
        InitializeComponent();
        
        taskManager = new TaskManager(logger);
        
        quizHelper = new QuizUIHelper(
            QuizProgressText,
            QuizQuestionText,
            QuizFeedbackText,
            QuizOptionA,
            QuizOptionB,
            QuizOptionC,
            QuizOptionD,
            QuizStartButton,
            QuizSubmitButton,
            QuizNextButton,
            logger
        );
        AddBotMessage(@"
╔═══╗     ╔╗         
║╔═╗║     ║║         
║║ ╚╝╔╗ ╔╗║╚═╗╔══╗╔═╗
║║ ╔╗║║ ║║║╔╗║║╔╗║║╔╝
║╚═╝║║╚═╝║║╚╝║║║═╣║║ 
╚═══╝╚═╗╔╝╚══╝╚══╝╚╝ 
     ╔═╝║                         
     ╚══╝                         
                ");
        AddBotMessage(@"
╔══╗      ╔╗ 
║╔╗║     ╔╝╚╗
║╚╝╚╗╔══╗╚╗╔╝
║╔═╗║║╔╗║ ║║ 
║╚═╝║║╚╝║ ║╚╗
╚═══╝╚══╝ ╚═╝
                " );
    
        AddBotMessage("Hi! What is your name?");
        PlayGreetingAudio();
        LoadTasks();
        RefreshActivityLog();
    }

private void SendButton_Click(object sender, RoutedEventArgs e)
{
    string input = InputBox.Text;
    if (string.IsNullOrWhiteSpace(input)) return;

    if (!nameEntered)
    {
        bot.Name = input;
        nameEntered = true;
        AddUserMessage(input);
        AddBotMessage("Welcome, " + bot.Name + "! Feel free to ask me about passwords, phishing, privacy and more!");
        InputBox.Clear();
        return;
    }

    string lower = input.ToLower();
    AddUserMessage(input);

    if (_pendingTaskId != -1)
    {
        string reminderText = input.Trim();
        taskManager.SetReminder(_pendingTaskId, reminderText);
        AddBotMessage($"Got it! Reminder set: '{reminderText}'.");
        logger.Log($"Reminder set for task ID {_pendingTaskId}: {reminderText}");
        _pendingTaskId = -1;
        LoadTasks();
        RefreshActivityLog();
        InputBox.Clear();
        return;
    }

    string? taskTitle = bot.ExtractTaskTitle(input);
    if (taskTitle != null)
    {
        int newId = taskManager.AddTask(taskTitle, "", "");  // No more "Added via chat"
        _pendingTaskId = newId;
        AddBotMessage($"Task added: '{taskTitle}'. How many days would you like the reminder for?");
        logger.Log($"NLP matched: add task — '{taskTitle}'");
        LoadTasks();
        RefreshActivityLog();
        InputBox.Clear();
        return;
    }

    if (bot.DetectQuizIntent(lower))
    {
        AddBotMessage("Starting the cybersecurity quiz! Switching to the Quiz tab now.");
        quizHelper.StartQuizFromChat();
        MainTabControl.SelectedIndex = 3;
        logger.Log("NLP matched: start quiz");
        InputBox.Clear();
        return;
    }

    if (bot.DetectLogIntent(lower))
    {
        AddBotMessage(logger.GetRecentLog(10));
        if (logger.GetCount() > 10)
            AddBotMessage("There are more entries. Type 'show more' or click 'Show Full Log' in the Activity Log tab.");
        logger.Log("NLP matched: show activity log");
        RefreshActivityLog();
        InputBox.Clear();
        return;
    }

    if (lower.Contains("show more") || lower.Contains("full log"))
    {
        AddBotMessage(logger.GetFullLog());
        logger.Log("NLP matched: show full activity log");
        RefreshActivityLog(showAll: true);
        InputBox.Clear();
        return;
    }

    string? followUp = bot.GetFollowUp(lower);
    if (followUp != null)
    {
        AddBotMessage(followUp);
        InputBox.Clear();
        return;
    }

    string? interest = bot.DetectInterest(lower);
    if (interest != null)
    {
        AddBotMessage(interest);
        InputBox.Clear();
        return;
    }

    string? sentiment = bot.GetSentimentResponse(lower);
    if (sentiment != null)
        AddBotMessage(sentiment);

    string? response = bot.GetResponse(lower) ?? bot.GetConversation(lower);
    if (response != null)
    {
        AddBotMessage(response);
        if (bot.LastTopic != null)
            logger.Log($"Keyword matched: {bot.LastTopic} - response delivered");
    }
    else if (sentiment == null)
    {
        AddBotMessage("I'm not sure I'm familiar with that keyword!");
    }

    if (bot.FavouriteTopic != null && lower.Contains(bot.FavouriteTopic))
        AddBotMessage($"As someone interested in {bot.FavouriteTopic}, this topic is especially relevant to you!");

    InputBox.Clear();
}

    private void InputBox_KeyDown(object sender, KeyEventArgs e)
    {
        if (e.Key == Key.Enter)
            SendButton_Click(sender, e);
    }

    private void AddTaskButton_Click(object sender, RoutedEventArgs e)
    {
        string title = TaskTitleBox.Text.Trim();
        string description = TaskDescBox.Text.Trim();
        string reminder = TaskReminderBox.Text.Trim();

        if (string.IsNullOrWhiteSpace(title)) return;

        taskManager.AddTask(title, description, reminder);
        TaskTitleBox.Clear();
        TaskDescBox.Clear();
        TaskReminderBox.Clear();
        LoadTasks();
        RefreshActivityLog();
    }

    private void HelpButton_Click(object sender, RoutedEventArgs e)
    {
        MessageBox.Show(
            "Chat Commands:\n\n" +
            "  add task to [title]        e.g. 'Add task to Enable 2FA'\n" +
            "  remind me to [title]       e.g. 'Remind me to update my password'\n" +
            "  start quiz                 launches the cybersecurity quiz\n" +
            "  show activity log          shows the last 10 actions\n" +
            "  show more                  shows the full activity history\n\n" +
            "Cybersecurity topics:\n" +
            "  passwords, phishing, privacy, scams,\n" +
            "  browsing, social media, updates",
            "How to Use CyberBot",
            MessageBoxButton.OK,
            MessageBoxImage.Information);
    }

    private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListBox.SelectedItem is ListBoxItem item && item.Tag is CyberTask task)
        {
            taskManager.MarkAsComplete(task.Id);
            LoadTasks();
            RefreshActivityLog();
        }
    }

    private void IncompleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListBox.SelectedItem is ListBoxItem item && item.Tag is CyberTask task)
        {
            taskManager.MarkAsIncomplete(task.Id);
            LoadTasks();
            RefreshActivityLog();
        }
    }

    private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListBox.SelectedItem is ListBoxItem item && item.Tag is CyberTask task)
        {
            taskManager.DeleteTask(task.Id);
            LoadTasks();
            RefreshActivityLog();
        }
    }

    private void LoadTasks()
    {
        var tasks = taskManager.GetAllTasks();
        TaskListBox.ItemsSource = null;
        TaskListBox.Items.Clear();

        foreach (var task in tasks)
        {
            string status = task.IsComplete ? "✓" : "○";
            string reminder = string.IsNullOrWhiteSpace(task.Reminder) ? "" : $" | Reminder: {task.Reminder}";
            string display = $"{status} {task.Title} - {task.Description}{reminder}";

            ListBoxItem item = new ListBoxItem();
            item.Content = display;
            item.Tag = task;
            item.Foreground = task.IsComplete
                ? new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6E3A1"))
                : new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBA6F7"));

            TaskListBox.Items.Add(item);
        }
    }

    // Quiz delegates
    private void QuizStartButton_Click(object sender, RoutedEventArgs e)
    {
        quizHelper.StartQuiz();
    }

    private void QuizSubmitButton_Click(object sender, RoutedEventArgs e)
    {
        quizHelper.SubmitAnswer();
    }

    private void QuizNextButton_Click(object sender, RoutedEventArgs e)
    {
        quizHelper.NextQuestion();
    }

    // Activity Log
    private void RefreshLogButton_Click(object sender, RoutedEventArgs e)
    {
        RefreshActivityLog();
    }

    private void ShowMoreLogButton_Click(object sender, RoutedEventArgs e)
    {
        RefreshActivityLog(showAll: true);
    }

    private void RefreshActivityLog(bool showAll = false)
    {
        ActivityLogPanel.Children.Clear();

        if (logger.GetCount() == 0)
        {
            AddLogEntry("No activity recorded yet.", "#6C7086");
            ShowMoreLogButton.IsEnabled = false;
            return;
        }

        string raw = showAll ? logger.GetFullLog() : logger.GetRecentLog(10);
        string[] lines = raw.Split('\n', StringSplitOptions.RemoveEmptyEntries);

        foreach (string line in lines)
        {
            if (!string.IsNullOrWhiteSpace(line))
                AddLogEntry(line, "#FFCBA6F7");
        }

        ShowMoreLogButton.IsEnabled = !showAll && logger.GetCount() > 10;
    }

    private void AddLogEntry(string text, string colorHex)
    {
        TextBlock tb = new TextBlock
        {
            Text = text,
            Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString(colorHex)),
            Margin = new Thickness(8, 4, 8, 4),
            TextWrapping = TextWrapping.Wrap,
            FontSize = 13
        };
        ActivityLogPanel.Children.Add(tb);
    }

    private void AddBotMessage(string text)
    {
        TextBlock msg = new TextBlock();
        msg.Text = "CyberBot: " + text;
        msg.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6E3A1"));
        msg.Margin = new Thickness(10);
        msg.TextWrapping = TextWrapping.Wrap;
        ChatPanel.Children.Add(msg);
        ChatScrollViewer.ScrollToBottom();
    }

    private void AddUserMessage(string text)
    {
        TextBlock msg = new TextBlock();
        msg.Text = "You: " + text;
        msg.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBA6F7"));
        msg.Margin = new Thickness(10);
        msg.TextWrapping = TextWrapping.Wrap;
        ChatPanel.Children.Add(msg);
        ChatScrollViewer.ScrollToBottom();
    }

    private void PlayGreetingAudio()
    {
        try
        {
            var player = new System.Media.SoundPlayer("assets/greeting.wav");
            player.Play();
        }
        catch { }
    }
}
