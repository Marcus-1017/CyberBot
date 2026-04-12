using System.Collections.Generic;

//Marcus Johnson
//ST10496028

public class ChatBot
{
    public string ?Name; 

    public Dictionary<string,string> responses = new() 
    {
        {"passw", "\n══════════════════════════════════════════" 
        +"\nSo, you want to know more about passwords?"
        +"\n══════════════════════════════════════════"
        +"\n1. Never use the same password more than once."
        +"\n2. Consider storing your passwords in a Password Manager."
        +"\n3. Always enable MFA (multi-factor authentication) when you can."
        +"\n4. Try to keep your passwords reasonably long and complex."
        +"\n5. Never share your passwords with anyone you don't trust."
        +"\n"
        +"\nI hope that answers any questions you may have about passwords!"
        +"\n═══════════════════════════════════════════════════════════════"
        },

        {"phish", "\n══════════════════════════════════════════"
        +"\nSo, you want to know more about phishing?"
        +"\n══════════════════════════════════════════"
        +"\n1. Never click links in emails asking for personal information."
        +"\n2. Check the sender's email address carefully for subtle misspellings."
        +"\n3. Legitimate organisations will never ask for your password via email."
        +"\n4. Wherever possible, go directly to the website instead of clicking the link."
        +"\n5. Report suspicious emails as phishing in your email client."
        +"\n"
        +"\nI hope that answers any questions you may have about phishing!"
        +"\n══════════════════════════════════════════════════════════════"
        },

        {
        "priva", "\n══════════════════════════════════════════"
        +"\nSo, you want to know more about privacy?"
        +"\n══════════════════════════════════════════"
        +"\n1. Review your social media privacy settings regularly."
        +"\n2. Avoid sharing personal media or information about yourself on the internet."
        +"\n3. Use a VPN when connecting to public Wi-Fi networks."
        +"\n4. Be heavily selective about which apps you grant permissions to."
        +"\n"
        +"\nI hope that answers any questions you may have about privacy!"
        +"\n═════════════════════════════════════════════════════════════"
        },
        {
        "how are", "I'm doing pretty well, thanks for asking!!!"
        },
        {
        "purpose", "My name is CyberBot, I am a cybersecurity awareness chatbot built in C#"
        }
    };

    //this method will take input from program.cs and 
    //loop through the dictionary to see if the input contains any keywords from the input
    public string GetResponse(string input){

        foreach (var entry in responses)
        {
            if (input.Contains(entry.Key))
            {
                return entry.Value;
            }
        }
        return "\nMy apologies, " + " " + "I'm not sure I know anything about that topic yet! \nCould you ask me about something else?";
    }
}
