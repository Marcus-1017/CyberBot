using System;
using System.Media;
using System.Threading;

//Marcus Johnson
//ST10496028

public static class ConsoleUI
{
    //Wav file playback
    public static void PlayGreetingAudio()
    {
       try
       {
           new SoundPlayer("assets/greeting.wav").PlaySync();
       }
       catch (System.IO.FileNotFoundException)
       {
           Console.WriteLine("greeting audio file not found, proceeding without audio.");
       }
    }

    public static void DisplayAsciiArt()
    {
        Console.WriteLine(
"""
                                                       .#@@@@@@#:                           
                                                    *@*          *@#                        
                                                  #%    :#@@@@%:    %%                      
                                                 @:  :@#        #@-  .@                     
                                                @   %*            *@   @                    
                                               @-  @-              :@  :@                   
                                               @  =@                @+  @                   
                                               @  *%                #*  @                   
                                               @  *%                **  @                   
                                               @  *%                **  @                   
                                           :@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@:               
                                          :%                                %:              
                                          :%                                %-              
                                          :%                                %-              
                                          :%              :--:              %-              
                                          :%            =@    @+            %-              
                                          :%           .@      @.           %-              
                                          :%            @      @.           %-              
                                          :%             @%  #@             %-              
                                          :%              @  %.             %-              
                                          :%              @  %.             %-              
                                          :%              @  @              %-              
                                          :%               **               %-              
                                          :%                                %-              
                                          :%                                %-              
                                          :%                                %:              
                                           +@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@+               
   _______     ______  ______ _____   _____ ______ _____ _    _ _____  _____ _________     __  ____   ____ _______ 
  / ____\ \   / /  _ \|  ____|  __ \ / ____|  ____/ ____| |  | |  __ \|_   _|__   __\ \   / / |  _ \ / __ \__   __|
 | |     \ \_/ /| |_) | |__  | |__) | (___ | |__ | |    | |  | | |__) | | |    | |   \ \_/ /  | |_) | |  | | | |   
 | |      \   / |  _ <|  __| |  _  / \___ \|  __|| |    | |  | |  _  /  | |    | |    \   /   |  _ <| |  | | | |   
 | |____   | |  | |_) | |____| | \ \ ____) | |___| |____| |__| | | \ \ _| |_   | |     | |    | |_) | |__| | | |   
  \_____|  |_|  |____/|______|_|  \_\_____/|______\_____|\____/|_|  \_\_____|  |_|     |_|    |____/ \____/  |_|   
     
════════════════════════════════════════════════════════════════════════════════════════════════════════════════════                                                                                                             
""");


    }

public static void PrintColourWord(string input, ConsoleColor colour, bool newline = true)
{
    Console.ForegroundColor = colour;
    Console.Write(input);
    Console.ResetColor();
    
    if (newline)
    {
        Console.WriteLine("");
    }

}

//print methods
public static void Print(string text, int speed = 5, bool newline = true)
{
    foreach (char c in text)
    {
        Console.Write(c);
        Thread.Sleep(speed);
    }
    if (newline)
    {
        Console.WriteLine();
    }
}
}

