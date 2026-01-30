using System.Collections.Generic;
using UnityEngine;

public class QuizManager : MonoBehaviour
{
    [Header("Database")]
    [SerializeField] private QuizDatabaseSO quizDatabase;

    private CategorySO currentCategory;
    private QuestionSO currentQuestion;

    private List<string> currentAnswers = new List<string>();

    // Preguntas usadas por categoría (runtime)
    private Dictionary<CategorySO, HashSet<QuestionSO>> usedQuestions =
        new Dictionary<CategorySO, HashSet<QuestionSO>>();

    // =========================
    // SETUP
    // =========================

    public void SetCategory(CategorySO category)
    {
        currentCategory = category;

        if (!usedQuestions.ContainsKey(category))
            usedQuestions.Add(category, new HashSet<QuestionSO>());
    }

    // =========================
    // QUESTION FLOW
    // =========================

    public void GenerateQuestion()
    {
        currentQuestion = GetRandomUnusedQuestion(currentCategory);

        if (currentQuestion == null)
        {
            Debug.LogWarning("No hay preguntas disponibles en esta categoría");
            return;
        }

        usedQuestions[currentCategory].Add(currentQuestion);
        PrepareAnswers();
    }

    private QuestionSO GetRandomUnusedQuestion(CategorySO category)
    {
        List<QuestionSO> available = new List<QuestionSO>();

        foreach (var question in quizDatabase.questions)
        {
            if (question.Category == category &&
                !usedQuestions[category].Contains(question))
            {
                available.Add(question);
            }
        }

        // Si se acabaron, reiniciamos
        if (available.Count == 0)
        {
            usedQuestions[category].Clear();

            foreach (var question in quizDatabase.questions)
            {
                if (question.Category == category)
                    available.Add(question);
            }
        }

        if (available.Count == 0)
            return null;

        return available[Random.Range(0, available.Count)];
    }

    // =========================
    // ANSWERS
    // =========================

    private void PrepareAnswers()
    {
        currentAnswers.Clear();

        currentAnswers.Add(currentQuestion.CorrectAnswer);
        currentAnswers.AddRange(currentQuestion.WrongAnswers);

        // Mezclar
        for (int i = 0; i < currentAnswers.Count; i++)
        {
            int randomIndex = Random.Range(i, currentAnswers.Count);
            (currentAnswers[i], currentAnswers[randomIndex]) =
                (currentAnswers[randomIndex], currentAnswers[i]);
        }
    }

    // =========================
    // PUBLIC GETTERS
    // =========================

    public string GetQuestionText()
    {
        return currentQuestion.QuestionText;
    }

    public List<string> GetAnswers()
    {
        return currentAnswers;
    }

    public bool SubmitAnswer(int index)
    {
        if (index < 0 || index >= currentAnswers.Count)
            return false;

        return currentAnswers[index] == currentQuestion.CorrectAnswer;
    }
}
