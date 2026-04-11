using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class QuizUI : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private QuizManager quizManager;

    [Header("UI")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI categoryText;
    [SerializeField] private List<Button> answerButtons;
    [SerializeField] private TextMeshProUGUI timerText;
    [SerializeField] private RouletteFlow rouletteFlow;

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

        timerText.text = $"Tiempo: {time:F1}";
    }

    public void ShowQuestion(CategorySO category)
    {
        answered = false;

        quizManager.SetCategory(category);
        quizManager.GenerateQuestion();

        questionText.text = quizManager.GetQuestionText();
        categoryText.text = category.categoryName;

        List<string> answers = quizManager.GetAnswers();

        foreach (var btn in answerButtons)
        {
            btn.GetComponent<Image>().color = Color.white;
        }

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

        if (isCorrect)
            AudioManager.Instance.PlaySFX("RespuestaCorrecta");
        else
            AudioManager.Instance.PlaySFX("RespuestaIncorrecta");

        // Bloquear botones
        foreach (var btn in answerButtons)
            btn.interactable = false;

        // Avisar al GameManager
        GameManager.Instance.AnswerQuestion(isCorrect);

        // Feedback visual
        ShowAnswerFeedback(index, isCorrect);

        Debug.Log(isCorrect ? "✅ Correcto" : "❌ Incorrecto");

        StartCoroutine(ReturnToRouletteRoutine());

        // Más adelante:
        // - Fade out

    }

    private IEnumerator ReturnToRouletteRoutine()
    {
        // Espera para que el jugador vea el resultado
        yield return new WaitForSeconds(2.5f);

        // =========================================
        // AQUÍ IRÁ LA ANIMACIÓN DE CORTINAS
        // =========================================
        //
        // Cuando tengas la animación:
        // 1. Activás panel cortinas
        // 2. Esperás a que se cierren
        // 3. Cambiás de estado (volver a ruleta)
        // 4. Esperás que se abran
        //
        // Ejemplo futuro:
        // yield return StartCoroutine(CurtainController.CloseAndOpen());
        //
        // =========================================

        rouletteFlow.ReturnToRoulette();
    }

    private void ShowAnswerFeedback(int selectedIndex, bool isCorrect)
    {
        Color correctColor = Color.green;
        Color wrongColor = Color.red;

        if (isCorrect)
        {
            answerButtons[selectedIndex]
                .GetComponent<Image>().color = correctColor;
        }
        else
        {
            answerButtons[selectedIndex]
                .GetComponent<Image>().color = wrongColor;

            int correctIndex = quizManager.GetCorrectAnswerIndex();
            answerButtons[correctIndex]
                .GetComponent<Image>().color = correctColor;
        }
    }
}
