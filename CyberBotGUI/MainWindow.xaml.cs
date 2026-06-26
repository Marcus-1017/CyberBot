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
    TaskStorageHelper taskManager = new();


    public MainWindow()
    {
        InitializeComponent();
        AddBotMessage("Hi! What is your name?");
        PlayGreetingAudio();
        LoadTasks();

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

        //checks for follow ups before interests or sentiment to keep conversational flow
        string? followUp = bot.GetFollowUp(input.ToLower());
        if (followUp != null)
        {
            AddUserMessage(input);
            AddBotMessage(followUp);
            InputBox.Clear();
            return;
        }

        string? interest = bot.DetectInterest(input.ToLower());
        if (interest != null)
        {
            AddUserMessage(input);
            AddBotMessage(interest);
            InputBox.Clear();
            return;
        }

        // checks for sentiments
        AddUserMessage(input);
        string? sentiment = bot.GetSentimentResponse(input.ToLower());
        if (sentiment != null)
            AddBotMessage(sentiment);

        string? response = bot.GetResponse(input.ToLower()) ?? bot.GetConversation(input.ToLower());
        if (response != null)
        {
            AddBotMessage(response);
        }
        else if (sentiment == null)
        {
            AddBotMessage("I'm not sure I'm familiar with that keyword!");
        }

        // specialised response for favourite topic
        if (bot.FavouriteTopic != null && input.ToLower().Contains(bot.FavouriteTopic))
            AddBotMessage($"As someone interested in {bot.FavouriteTopic}, this topic is especially relevant to you!");


        InputBox.Clear();
    }private void InputBox_KeyDown(object sender, KeyEventArgs e)
{
    if (e.Key == Key.Enter)
        SendButton_Click(sender, e);
}

    // adds a task from the task panel
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
    }

    // marks selected task as complete
    private void CompleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListBox.SelectedItem is ListBoxItem item && item.Tag is CyberTask task)
        {
            taskManager.MarkAsComplete(task.Id);
            LoadTasks();
        }
    }

    // marks selected task as incomplete
    private void IncompleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListBox.SelectedItem is ListBoxItem item && item.Tag is CyberTask task)
        {
            taskManager.MarkAsIncomplete(task.Id);
            LoadTasks();
        }
    }

    // deletes selected task
    private void DeleteTaskButton_Click(object sender, RoutedEventArgs e)
    {
        if (TaskListBox.SelectedItem is ListBoxItem item && item.Tag is CyberTask task)
        {
            taskManager.DeleteTask(task.Id);
            LoadTasks();
        }
    }

    // loads tasks from json and displays them in the list
    private void LoadTasks()
    {
        var tasks = taskManager.LoadTasks();
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
private void AddBotMessage(string text)
{
    TextBlock msg = new TextBlock();
    msg.Text = "CyberBot: " + text;
    msg.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6E3A1"));
    msg.Margin = new Thickness(10);
    msg.TextWrapping = TextWrapping.Wrap;
    ChatPanel.Children.Add(msg);
}

private void AddUserMessage(string text)
{
    TextBlock msg = new TextBlock();
    msg.Text = "You: " + text;
    msg.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#CBA6F7"));
    msg.Margin = new Thickness(10);
    msg.TextWrapping = TextWrapping.Wrap;
    ChatPanel.Children.Add(msg);
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
