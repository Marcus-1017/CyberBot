using System.Collections.Generic;

//Marcus Johnson
//ST10496028

public class ChatBot
{
    public string ?Name;
    public string? FavouriteTopic;
    private Random random = new();
    public string? LastTopic;

    //random responses dictionary
    private Dictionary<string, List<string>> randomResponses = new()
{
    {"phish", new List<string>
        {
            "Quick phishing tip: Be cautious of emails asking for personal information. Scammers often disguise themselves as trusted organisations so try to verify who they are.\n",
            "Quick phishing tip: Never click suspicious links in emails, go directly to the website instead.\n",
            "Quick phishing tip: Check the sender's email address carefully for subtle misspellings.\n"
        }
    },
    {"passw", new List<string>
        {
            "Quick password tip: Use a password manager to keep track of strong unique passwords.\n",
            "Quick password tip: Never reuse the same password across multiple accounts.\n",
            "Quick password tip: Enable two-factor authentication wherever possible.\n"
        }
    },
    {"scam", new List<string>
        {
            "Quick scam tip: If it sounds too good to be true, it probably is.\n",
            "Quick scam tip: Never send money to someone you haven't met in person.\n",
            "Quick scam tip: Always verify the identity of anyone asking for personal details.\n"
        }
    }
};

    public string? GetRandomResponse(string input)
    {
        foreach (var entry in randomResponses)
        {
            if (input.Contains(entry.Key))
            {
                int index = random.Next(entry.Value.Count);
                return entry.Value[index];
            }
        }
        return null;
    }


    //"sentiment" dictionary 
    private Dictionary<string, string> sentiments = new()
{
    {"worri",  "Yeah I get it, its worrying stuff. Heres some tips to help.\n"},
    {"worry",  "Yeah I get it, its worrying stuff. Heres some tips to help.\n"},
    {"scared",   "Dont be scared. Heres what works for me.\n"},
    {"confused", "No worries, let me explain it better.\n"},
    {"frustrated", "Yeah I feel you. Let me help.\n"},
    {"anxious",  "Take a breath. One step at a time.\n"}
};

    //Main dictionary, the program loops through this one first
    private Dictionary<string,string> responses = new() 
    {
        {"passw", 
         "you want to know more about passwords?"
        +"\n══════════════════════════════════════════"
        +"\n1. Never use the same password more than once."
        +"\n2. Consider storing your passwords in a Password Manager."
        +"\n3. Always enable MFA (multi-factor authentication) when you can."
        +"\n4. Try to keep your passwords reasonably long and complex."
        +"\n5. Never share your passwords with anyone you don't trust."
        +"\n6. Try to change your passwords every few months."
        +"\n"
        +"\nI hope that answers any questions you may have about passwords!"
        +"\n═══════════════════════════════════════════════════════════════\n"
        },
        
        {"scam", 
         "you want to know more about internet scams?"
        +"\n══════════════════════════════════════════"
        +"\n1. Never send money or OTPs to someone you have only met online."
        +"\n2. Be suspicious of urgent messages claiming your account will be locked."
        +"\n3. Never share your ID number or bank details with unknown callers."
        +"\n4. Always verify that your caller is from the organisation they claim to be from."
        +"\n5. If it sounds too good to be true, it probably is a scam."
        +"\n"
        +"\nI hope that answers any questions you may have about internet scams!"
        +"\n══════════════════════════════════════════════════════════════\n"
        },

        {"brows", 
         "you want to know more about safe browsing?"
        +"\n══════════════════════════════════════════"
        +"\n1. Always look for HTTPS in the website address."
        +"\n2. Avoid saving passwords in your browser."
        +"\n3. Use an ad-blocker to avoid malicious advertisements."
        +"\n4. Clear your browsing cache and cookies regularly."
        +"\n"
        +"\nI hope that answers any questions you may have about browsing!"
        +"\n══════════════════════════════════════════════════════════════\n"
        },

        {"social", 
         "you want to know more about social media?"
        +"\n══════════════════════════════════════════"
        +"\n1. Never post your location or travel plans in real-time."
        +"\n2. Adjust privacy settings to 'Friends Only' for personal posts."
        +"\n3. Be careful of friend requests from people you don't know."
        +"\n4. Think before you post, it stays online forever."
        +"\n"
        +"\nI hope that answers any questions you may have about social media!"
        +"\n══════════════════════════════════════════\n"
        },

        {"phish", 
         "you want to know more about phishing?"
        +"\n══════════════════════════════════════════"
        +"\n1. Never click links in emails asking for personal information."
        +"\n2. Check the sender's email address carefully for subtle misspellings."
        +"\n3. Legitimate organisations will never ask for your password via email."
        +"\n4. Wherever possible, go directly to the website instead of clicking the link."
        +"\n5. Report suspicious emails as phishing in your email client."
        +"\n6. Never scan QR codes from sources you don't trust."
        +"\n"
        +"\nI hope that answers any questions you may have about phishing!"
        +"\n══════════════════════════════════════════════════════════════\n"
        },

        {"priva",
         "you want to know more about privacy?"
        +"\n══════════════════════════════════════════"
        +"\n1. Review your social media privacy settings regularly."
        +"\n2. Avoid sharing personal media or information about yourself on the internet."
        +"\n3. Use a VPN when connecting to public Wi-Fi networks."
        +"\n4. Be heavily selective about which apps you grant permissions to."
        +"\n"
        +"\nI hope that answers any questions you may have about privacy!"
        +"\n═════════════════════════════════════════════════════════════\n"
        },

        {"update",
         "you want to know more about software updates?"
        +"\n══════════════════════════════════════════"
        +"\n1. Updates fix known security vulnerabilities hackers exploit."
        +"\n2. Always install updates for your phone, laptop, and apps."
        +"\n3. Turn on automatic updates so you never miss critical patches."
        +"\n4. Outdated software is one of the most common ways hackers get in."
        +"\n5. This includes your browser, antivirus, and operating system."
        +"\n══════════════════════════════════════════\n"
        }
    };


    //conversation dictionary, loops only if not null and it doesnt find any keywords from 'response' dictionary
    private Dictionary<string,string> conversation = new()
    {
        {
        "hello", "hey!!\n"
        },

        {
        "hey", "helloooo\n"
        },

        {
        "how are", "I'm doing pretty well, thanks for asking!!!\n"
        },

        {
        "purpose", "My name is CyberBot, I am a cybersecurity awareness chatbot built in C#\n"
        },

        {
        "topic", "I can talk about - passwords, phishing, internet privacy, social media safety, safe browsing, internet scams, software updates\n"
        }
    };

    public string? DetectInterest(string input)
    {
        string[] topics = { "password", "phishing", "privacy", "scams", "browsing", "social media", "updates" };

        foreach (string topic in topics)
        {
            if (input.Contains("interest") || input.Contains("like") || input.Contains("care about"))
            {
                if (input.Contains(topic))
                {
                    FavouriteTopic = topic;
                    return $"Great! I'll remember that you're interested in {topic}. It's a crucial part of staying safe online.\n";
                }
            }
        }
        return null;
    }

    //loops in the sentiment dictionary
    public string? GetSentimentResponse(string input)
    {
        foreach (var entry in sentiments)
        {
            if (input.Contains(entry.Key))
                return entry.Value;
        }
        return null;
    }

    //this method will take input from program.cs and 
    //loop through the dictionary to see if the input contains any keywords from the input
    public string? GetResponse(string input){

        foreach (var entry in responses)
        {
            if (input.Contains(entry.Key))
            {
                //this is to match one of the follow up phrases
                LastTopic = entry.Key;

                return entry.Value;
            }
        }
        return null;
    }

    public string? GetConversation(string input)
    {
        foreach (var entry in conversation)
        {
            if (input.Contains(entry.Key))
                return entry.Value;
        }
        return null;
    }

    public string? GetFollowUp(string input)
    {
        string[] followUpPhrases = { "more", "explain", "elaborate", "another tip", "tell me more" };

        foreach (string phrase in followUpPhrases)
        {
            if (input.Contains(phrase) && LastTopic != null)
            {
                return GetRandomResponse(LastTopic);
            }
        }
        return null;
    }
}
