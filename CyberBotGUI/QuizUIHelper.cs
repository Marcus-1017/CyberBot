// Marcus Johnson
// ST10496028

using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace CyberBotGUI;

public class QuizUIHelper
{
    private QuizManager _quizManager;
    private ActivityLogger _logger;
    private bool _quizAnswered = false;

    private TextBlock _progressText;
    private TextBlock _questionText;
    private TextBlock _feedbackText;
    private RadioButton _optionA;
    private RadioButton _optionB;
    private RadioButton _optionC;
    private RadioButton _optionD;
    private Button _startButton;
    private Button _submitButton;
    private Button _nextButton;

    public QuizUIHelper(
        TextBlock progressText,
        TextBlock questionText,
        TextBlock feedbackText,
        RadioButton optionA,
        RadioButton optionB,
        RadioButton optionC,
        RadioButton optionD,
        Button startButton,
        Button submitButton,
        Button nextButton,
        ActivityLogger logger)
    {
        _quizManager = new QuizManager();
        _logger = logger;
        
        _progressText = progressText;
        _questionText = questionText;
        _feedbackText = feedbackText;
        _optionA = optionA;
        _optionB = optionB;
        _optionC = optionC;
        _optionD = optionD;
        _startButton = startButton;
        _submitButton = submitButton;
        _nextButton = nextButton;
    }

    public void StartQuiz()
    {
        _quizManager.StartQuiz();
        _quizAnswered = false;
        _startButton.IsEnabled = false;
        _submitButton.IsEnabled = true;
        _nextButton.IsEnabled = false;
        
        _logger.Log("Quiz started");
        ShowQuestion();
    }

    public void StartQuizFromChat()
    {
        StartQuiz();
    }

    public void ShowQuestion()
    {
        if (_quizManager.IsFinished())
        {
            ShowResults();
            return;
        }

        var question = _quizManager.GetCurrentQuestion();
        if (question == null)
        {
            ShowResults();
            return;
        }

        _progressText.Text = $"Question {_quizManager.GetCurrentIndex() + 1} of {_quizManager.GetTotalQuestions()} | Score: {_quizManager.GetScore()}";
        _questionText.Text = question.Question;

        _optionA.Content = "A) " + question.Options[0];
        _optionB.Content = "B) " + question.Options[1];
        _optionC.Content = "C) " + question.Options[2];
        _optionD.Content = "D) " + question.Options[3];

        _optionA.IsChecked = false;
        _optionB.IsChecked = false;
        _optionC.IsChecked = false;
        _optionD.IsChecked = false;
        _feedbackText.Text = "";
        _feedbackText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6E3A1"));

        _submitButton.IsEnabled = true;
        _nextButton.IsEnabled = false;
        _quizAnswered = false;
    }

    public void SubmitAnswer()
    {
        if (_quizAnswered) return;

        string selected = "";
        if (_optionA.IsChecked == true) selected = "A";
        else if (_optionB.IsChecked == true) selected = "B";
        else if (_optionC.IsChecked == true) selected = "C";
        else if (_optionD.IsChecked == true) selected = "D";

        if (string.IsNullOrEmpty(selected))
        {
            _feedbackText.Text = "Please select an answer first!";
            _feedbackText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F38BA8"));
            return;
        }

        var currentQuestion = _quizManager.GetCurrentQuestion();
        string correctAnswer = currentQuestion?.CorrectAnswer ?? "unknown";

        bool correct = _quizManager.SubmitAnswer(selected);
        _quizAnswered = true;

        string explanation = _quizManager.GetFeedback(correct);

        if (correct)
        {
            _feedbackText.Text = "Correct! " + explanation;
            _feedbackText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A6E3A1"));
        }
        else
        {
            _feedbackText.Text = $"Incorrect. The correct answer was {correctAnswer}. " + explanation;
            _feedbackText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#F38BA8"));
        }

        _progressText.Text = $"Question {_quizManager.GetCurrentIndex() + 1} of {_quizManager.GetTotalQuestions()} | Score: {_quizManager.GetScore()}";
        _submitButton.IsEnabled = false;

        if (_quizManager.IsFinished())
        {
            _nextButton.Content = "Show Results";
            _nextButton.IsEnabled = true;
        }
        else
        {
            _nextButton.Content = "Next Question";
            _nextButton.IsEnabled = true;
        }
    }

    public void NextQuestion()
    {
        if (_quizManager.IsFinished())
        {
            ShowResults();
        }
        else
        {
            ShowQuestion();
        }
    }

    public void ShowResults()
    {
        string finalScore = _quizManager.GetFinalScore();
        string message = _quizManager.GetFinalMessage();

        _progressText.Text = "Quiz Complete!";
        _questionText.Text = $"You scored {finalScore}!";
        _feedbackText.Text = message;
        _feedbackText.Foreground = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#FFCBA6F7"));

        _optionA.Content = "";
        _optionB.Content = "";
        _optionC.Content = "";
        _optionD.Content = "";

        _submitButton.IsEnabled = false;
        _nextButton.IsEnabled = false;
        _startButton.IsEnabled = true;
        _nextButton.Content = "Next Question";

        _logger.Log($"Quiz completed - score: {finalScore}");
    }

    public bool IsQuizActive()
    {
        return _quizManager.IsQuizActive();
    }
}
