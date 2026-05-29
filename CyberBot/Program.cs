using System;
using System.Threading;

//Marcus Johnson
//ST10496028

Console.ForegroundColor = ConsoleColor.DarkRed;
ConsoleUI.DisplayAsciiArt();
Console.ResetColor();

ConsoleUI.Print("hi there! please type your name here: ", 10);

string Name = Console.ReadLine();

//name handling loop
while (string.IsNullOrWhiteSpace(Name))
{
    ConsoleUI.Print("Names can't be blank! Please try again.\n", 10);
    
    Name = Console.ReadLine();

}

ChatBot bot = new(); 

bot.Name = Name;

ConsoleUI.Print("\nwelcome, ", 10);
ConsoleUI.PrintColourWord(bot.Name, ConsoleColor.Magenta);
ConsoleUI.Print("!\n\n");

//plays audio after it displays the welcome message and ASCII art
ConsoleUI.PlayGreetingAudio();

ConsoleUI.Print("Feel free to ask me about ");
ConsoleUI.PrintColourWord("my purpose , how I am,", ConsoleColor.Cyan);
ConsoleUI.Print(" or, more importantly, any questions you may have regarding ");
ConsoleUI.PrintColourWord("cybersecurity and internet safety!", ConsoleColor.Red);
ConsoleUI.Print("\nI am good at talking about: ");
ConsoleUI.PrintColourWord("passwords, phishing attacks, safe browsing\nsocial media safety, internet scams, internet privacy"
                         +", and software updates", ConsoleColor.Cyan);
ConsoleUI.Print(", alternatively, type \"exit\" to exit the program\n");

//Main chatbot loop
while (true)
{
    string? input = Console.ReadLine();

    //null/whitespace check is first so the other conditionals dont have to handle null values, etc
    if (string.IsNullOrWhiteSpace(input))
    {
        ConsoleUI.Print("please type something!\n");
        continue;

    }

    if (input.ToLower() == "exit")
    {
        ConsoleUI.Print("goodbye!! I hope you learnt a lot about cybersecurity!");
        Console.WriteLine(" ");
        break;        
    }

    //loops main dictionary
    string response = bot.GetResponse(input.ToLower());
    
    if (response != null)
    {
        ConsoleUI.Print("So, ", 5);
        ConsoleUI.PrintColourWord(bot.Name, ConsoleColor.Magenta);
        ConsoleUI.Print(", ", 5);
        ConsoleUI.Print(response, 2);
        continue;
    }

    //loops conversation dictionary, if no keywords are found in the main one
    string convo = bot.GetConversation(input.ToLower());
    if (convo != null)
    {
        ConsoleUI.Print(convo, 2);
        continue;
    }

        ConsoleUI.Print("My apologies, ", 5);
        ConsoleUI.PrintColourWord(bot.Name + ", ", ConsoleColor.Magenta);
        ConsoleUI.Print("I'm not sure I know anything about that topic yet!\nCould you ask me about something else?\n", 2);
}



