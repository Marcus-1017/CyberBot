using System;

//Marcus Johnson
//ST10496028

ConsoleUI.PlayGreetingAudio();

ConsoleUI.DisplayAsciiArt();
Console.WriteLine("hi there! what is your name??");

string Name = Console.ReadLine();

while (string.IsNullOrWhiteSpace(Name))
{
    Console.WriteLine("Names can't be blank! Please try again.");
    Name = Console.ReadLine();

}

ChatBot bot = new();

bot.Name = Name;

