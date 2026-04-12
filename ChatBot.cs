using System.Collections.Generic;

//Marcus Johnson
//ST10496028

public class ChatBot
{
    public string Name; 

    public Dictionary<string,string> responses = new() 
    {
        {"password", "══════════════════════════════════════════" 
        +"So, you want to know more about passwords?"
        +"══════════════════════════════════════════"
        +"1. Never use the same password more than once."
        +"2. Consider storing your passwords in a Password Manager."
        +"3. Always enable MFA (multi-factor authentication) when you can."
        +"4. Try to keep your passwords reasonably long and complex."
        +"5. Never share your passwords with anyone you don't trust."
        +""
        +"I hope that answers any questions you may have about passwords!"
        +"═══════════════════════════════════════════════════════════════"
        },

        {"phishing", "══════════════════════════════════════════"
        +"So, you want to know more about phishing?"
        +"══════════════════════════════════════════"
        +"1. Never click links in emails asking for personal information."
        +"2. Check the sender's email address carefully for subtle misspellings."
        +"3. Legitimate organisations will never ask for your password via email."
        +"4. When in doubt, go directly to the website instead of clicking the link."
        +"5. Report suspicious emails as phishing in your email client."
        +""
        +"I hope that answers any questions you may have about phishing!"
        +"══════════════════════════════════════════════════════════════"
        },

        {
        "privacy", "══════════════════════════════════════════"
        +"So, you want to know more about privacy?"
        +"══════════════════════════════════════════"
        +"1. Review your social media privacy settings regularly."
        +"2. Avoid sharing personal media or information about yourself on the internet."
        +"3. Use a VPN when connecting to public Wi-Fi networks."
        +"4. Be heavily selective about which apps you grant permissions to."
        +""
        +"I hope that answers any questions you may have about privacy!"
        +"═════════════════════════════════════════════════════════════"
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
        return "My apologies, I'm not sure I know anything about that topic yet! \nCould you ask me about something else?";
    }
}
