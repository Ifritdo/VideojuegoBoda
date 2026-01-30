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

    private void Start()
    {
        // TEMPORAL: elegimos una categoría asignada, ELIMINAR cuando se tenga la ruleta de azar
        ShowQuestion(testCategory);
    }

    public void ShowQuestion(CategorySO category)
    {
        quizManager.SetCategory(category);
        quizManager.GenerateQuestion();

        questionText.text = quizManager.GetQuestionText();
        categoryText.text = category.categoryName;

        List<string> answers = quizManager.GetAnswers();

        for (int i = 0; i < answerButtons.Count; i++)
        {
            int index = i;
            answerButtons[i].GetComponentInChildren<TextMeshProUGUI>().text = answers[i];

            answerButtons[i].onClick.RemoveAllListeners();
            answerButtons[i].onClick.AddListener(() => OnAnswerSelected(index));
        }
    }

    private void OnAnswerSelected(int index)
    {
        bool isCorrect = quizManager.SubmitAnswer(index);

        Debug.Log(isCorrect ? "✅ Correcto" : "❌ Incorrecto");

        // Más adelante:
        // - Avisar al GameManager
        // - Mostrar feedback visual
        // - Pasar a la siguiente pregunta
    }
}
