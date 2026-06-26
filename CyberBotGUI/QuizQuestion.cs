// Marcus Johnson
// ST10496028

namespace CyberBotGUI;

public class QuizQuestion
{
    public string Question { get; set; } = "";
    public List<string> Options { get; set; } = new List<string>(); 
    public string CorrectAnswer { get; set; } = ""; 
    public string Explanation { get; set; } = ""; 
    public bool IsTrueFalse { get; set; } }
