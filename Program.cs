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
    ConsoleUI.Print("Names can't be blank! Please try again.", 10);
    
    Name = Console.ReadLine();

}

ChatBot bot = new(); 

bot.Name = Name;

ConsoleUI.Print("welcome, ", 10, false);
ConsoleUI.PrintColourWord(bot.Name, ConsoleColor.Magenta, false);
Console.Write("!\n\n");

ConsoleUI.Print("Feel free to ask me about ",5,false);
ConsoleUI.PrintColourWord("my purpose , how I am,", ConsoleColor.Cyan, false);
ConsoleUI.Print(" or, more importantly, any questions you may have regarding ",5,false);
ConsoleUI.PrintColourWord("cybersecurity and internet safety!", ConsoleColor.Red, false);
ConsoleUI.Print("\nI am good at talking about: ",5,false);
ConsoleUI.PrintColourWord("Passwords, Phishing attacks, and Privacy", ConsoleColor.Cyan, false);
ConsoleUI.Print("\nalternatively, type \"exit\" to exit the program");
string? input = Console.ReadLine();

bool run = true;
while (run)
{
    if (input.Equals("exit"))
    {
        run = false;
        break;
    }

    ConsoleUI.Print(bot.GetResponse(input.ToLower()), 1);
    ConsoleUI.Print("\nor type \"exit\" to exit the program", 4);

    input = Console.ReadLine();
}



