using System;
using System.Threading;

//Marcus Johnson
//ST10496028

ConsoleUI.PlayGreetingAudio();
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

ConsoleUI.Print("Feel free to ask me about ");
ConsoleUI.PrintColourWord("my purpose , how I am,", ConsoleColor.Cyan);
ConsoleUI.Print(" or, more importantly, any questions you may have regarding ");
ConsoleUI.PrintColourWord("cybersecurity and internet safety!", ConsoleColor.Red);
ConsoleUI.Print("\nI am good at talking about: ");
ConsoleUI.PrintColourWord("Passwords, Phishing attacks, and Privacy", ConsoleColor.Cyan);
ConsoleUI.Print(", alternatively, type \"exit\" to exit the program\n");

while (true)
{
    string input = Console.ReadLine();

    if (input.ToLower() == "exit")
    {
        break;        
    }
    if (string.IsNullOrWhiteSpace(input))
    {
        ConsoleUI.Print("please type something!\n");
        continue;

    }

    string response = bot.GetResponse(input.ToLower(), bot.Name, out bool found);
    
    if (found)
    {
        ConsoleUI.Print("So, ", 5);
    }
    else
    {
        ConsoleUI.Print("My apologies, ", 5);
    }

    ConsoleUI.PrintColourWord(bot.Name, ConsoleColor.Magenta);        
    ConsoleUI.Print(response, 2);
}



