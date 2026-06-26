// Marcus Johnson
// ST10496028

namespace CyberBotGUI;

public class QuizManager
{
    private List<QuizQuestion> _questions = new();
    private int _currentIndex = 0;
    private int _score = 0;
    private bool _quizActive = false;

    public QuizManager()
    {
        LoadQuestions();
    }

    private void LoadQuestions()
    {
        _questions = new List<QuizQuestion>
        {
    
        new QuizQuestion
{
    Question = "Someone emails you asking for your password. What do you do?",
    Options = new List<string> { "Send them your password", "Just delete it", "Report it as phishing", "Ignore it" },
    CorrectAnswer = "C",
    Explanation = "Reporting phishing emails helps stop scams and protects other people too."
},
new QuizQuestion
{
    Question = "Which of these passwords is actually strong?",
    Options = new List<string> { "password123", "MyBirthday1990", "P@ssw0rd!", "G7#kL9$mQ2@vX" },
    CorrectAnswer = "D",
    Explanation = "Strong passwords mix uppercase, lowercase, numbers, and symbols so they’re harder to crack."
},
new QuizQuestion
{
    Question = "What does HTTPS actually mean?",
    Options = new List<string> { "Hyper Text Transfer Protocol Secure", "High Tech Transfer Protocol System", "Hyper Transfer Text Protocol Secure", "None of these" },
    CorrectAnswer = "A",
    Explanation = "HTTPS encrypts the connection between your browser and the website."
},
new QuizQuestion
{
    Question = "Is it a good idea to do online banking on public Wi-Fi?",
    Options = new List<string> { "Yes, it’s always fine", "No, it’s risky unless you use a VPN", "Only if the Wi-Fi is free", "Only for checking balances" },
    CorrectAnswer = "B",
    Explanation = "Public Wi-Fi is often unencrypted, so attackers can intercept data unless you’re using protection like a VPN."
},
new QuizQuestion
{
    Question = "What is social engineering in simple terms?",
    Options = new List<string> { "A computer virus", "Tricking people into giving away info", "Building social networks", "Designing social media apps" },
    CorrectAnswer = "B",
    Explanation = "It’s basically psychological manipulation to get people to reveal sensitive information."
},
new QuizQuestion
{
    Question = "2FA adds extra security. What’s usually the second step?",
    Options = new List<string> { "Your password again", "A code sent to your phone", "Your birth date", "Your address" },
    CorrectAnswer = "B",
    Explanation = "Two-factor authentication uses your password plus something you physically have, like your phone."
},
new QuizQuestion
{
    Question = "If you think you got scammed, what’s the best first step?",
    Options = new List<string> { "Ignore it", "Contact your bank and report it", "Only tell friends", "Wait and see what happens" },
    CorrectAnswer = "B",
    Explanation = "You should report it quickly so damage can be limited and others can be warned."
},
new QuizQuestion
{
    Question = "is it good to save passwords in your browser?",
    Options = new List<string> { "Yes, always fine", "Better to use a password manager", "Only for low-value accounts", "As long as the password is strong" },
    CorrectAnswer = "B",
    Explanation = "Dedicated password managers are generally more secure than browser storage."
},
new QuizQuestion
{
    Question = "What is malware?",
    Options = new List<string> { "Hardware component", "Software made to cause harm", "A system update", "A browser feature" },
    CorrectAnswer = "B",
    Explanation = "Malware is just malicious software designed to damage or access systems without permission."
},
new QuizQuestion
{
    Question = "Why update software as soon as possible?",
    Options = new List<string> { "New features", "Fixing security holes", "Speed boost", "Because it’s required" },
    CorrectAnswer = "B",
    Explanation = "Updates often patch security vulnerabilities that attackers actively exploit."
},
new QuizQuestion
{
    Question = "What’s ransomware?",
    Options = new List<string> { "Free software", "Locks your files and demands money", "Antivirus software", "A security certificate" },
    CorrectAnswer = "B",
    Explanation = "It encrypts your files and demands payment to get them back."
},
new QuizQuestion
{
    Question = "You find a random QR code online, is it safe to scan?",
    Options = new List<string> { "Yes, it’s just a code", "No, it can lead to phishing", "Only if you know the website", "Always safe" },
    CorrectAnswer = "B",
    Explanation = "QR codes can hide malicious links, so you should only scan trusted ones."
}
        };
    }

    public bool IsQuizActive()
    {
        return _quizActive;
    }

    public void StartQuiz()
    {
        _currentIndex = 0;
        _score = 0;
        _quizActive = true;
    }

    public QuizQuestion? GetCurrentQuestion()
    {
        if (_currentIndex < _questions.Count)
            return _questions[_currentIndex];
        return null;
    }

    public bool SubmitAnswer(string answer)
    {
        if (!_quizActive || _currentIndex >= _questions.Count)
            return false;

        var question = _questions[_currentIndex];
        bool correct = answer.ToUpper() == question.CorrectAnswer;
        
        if (correct)
            _score++;
        _currentIndex++;

        if (_currentIndex >= _questions.Count)
            _quizActive = false;
        return correct;
    }

    public bool IsFinished()
    {
        return _currentIndex >= _questions.Count;
    }

    public int GetTotalQuestions()
    {
        return _questions.Count;
    }

    public string GetFinalMessage()
    {
        int percentage = (_score * 100) / _questions.Count;
        
        if (percentage >= 90)
            return "Excellent! You're a cybersecurity pro!";
        else if (percentage >= 70)
            return "Good job! You have solid cybersecurity knowledge!";
        else if (percentage >= 50)
            return "Keep learning! You've got the basics but could improve.";
        else
            return "Keep studying cybersecurity, it's important for your safety!";
    }

    public string GetProgressText()
    {
        return $"Question {_currentIndex + 1} of {_questions.Count} | Score: {_score}";
    }

    public int GetCurrentIndex()
    {
        return _currentIndex;
    }

    public void ResetQuiz()
    {
        _currentIndex = 0;
        _score = 0;
    }

    public string GetFeedback(bool correct)
    {
        var question = _questions[_currentIndex - 1];
        return question.Explanation;
    }

    public string GetFinalScore()
    {
        return $"{_score} out of {_questions.Count}";
    }

    public int GetScore()
    {
        return _score;
    }
}
