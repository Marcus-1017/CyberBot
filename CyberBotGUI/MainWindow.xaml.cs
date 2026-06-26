using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging; 
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CyberBotGUI;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    //creates bot object and bool for name input
    ChatBot bot = new();
    bool nameEntered = false;

    public MainWindow()
    {
        InitializeComponent();
        AddBotMessage("Hi! What is your name?");
        PlayGreetingAudio();

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

        //this is for specialised responses
        if (bot.FavouriteTopic != null && input.ToLower().Contains(bot.FavouriteTopic))
            AddBotMessage($"As someone interested in {bot.FavouriteTopic}, this topic is especially relevant to you!");

        InputBox.Clear();
    }

private void InputBox_KeyDown(object sender, KeyEventArgs e)
{
    if (e.Key == Key.Enter)
        SendButton_Click(sender, e);
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
