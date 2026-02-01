using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class QuizUI : MonoBehaviour
{
    [SerializeField] private CategorySO testCategory; // TEMPORAL: Quitar cuando se implemente la rueda que eliga la categoria al azar

    [Header("References")]
    [SerializeField] private QuizManager quizManager;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI categoryText;
    [SerializeField] private List<Button> answerButtons;
    [SerializeField] private TextMeshProUGUI timerText;

    private bool answered = false;

    private void Update()
    {
        if (GameManager.Instance == null) return;

        float time = GameManager.Instance.GetTime();

        // Si la pregunta no está activa, ocultamos
        if (time <= 0f)
        {
            timerText.text = "";
            return;
        }

        timerText.text = $"⏱ {time:F1}";
    }

    public void ShowQuestion(CategorySO category)
    {
        answered = false;

        quizManager.SetCategory(category);
        quizManager.GenerateQuestion();

        questionText.text = quizManager.GetQuestionText();
        categoryText.text = category.categoryName;

        List<string> answers = quizManager.GetAnswers();

        for (int i = 0; i < answerButtons.Count; i++)
        {
            int index = i;

            answerButtons[i].interactable = true;
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }

        // Avisamos que empieza la pregunta
        GameManager.Instance.StartQuestion();
    }


    private void OnAnswerSelected(int index)
    {
        if (answered) return;
        answered = true;

        bool isCorrect = quizManager.SubmitAnswer(index);

        // Bloquear botones
        foreach (var btn in answerButtons)
            btn.interactable = false;

        // Avisar al GameManager
        GameManager.Instance.AnswerQuestion(isCorrect);

        Debug.Log(isCorrect ? "✅ Correcto" : "❌ Incorrecto");

        // Más adelante:
        // - Feedback visual
        // - Fade out
        // - Volver a ruleta
    }

}
