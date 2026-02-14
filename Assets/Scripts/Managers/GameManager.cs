using UnityEngine;
using System;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action<int> OnScoreAdded;

    [Header("Cantidad de Rondas")]
    [SerializeField] private int maxQuestions = 10;

    [Header("Config")]
    public int pointsPerQuestion = 1000;
    public float maxTime = 20f;

    private float currentTime;
    private bool questionActive;

    private int totalScore = 0;
    private int questionsAnswered = 0;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UnityEngine.SceneManagement.SceneManager.LoadScene("Menu");
        }

        if (!questionActive) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            questionActive = false;

            OnTimeExpired();
            CheckGameEnd();
        }
    }

    // =========================
    // QUESTION FLOW
    // =========================

    public void StartQuestion()
    {
        currentTime = maxTime;
        questionActive = true;
    }

    public void AnswerQuestion(bool isCorrect)
    {
        if (!questionActive) return;

        questionActive = false;
        questionsAnswered++;

        if (isCorrect)
        {
            float timeRatio = currentTime / maxTime;
            int points = Mathf.RoundToInt(pointsPerQuestion * timeRatio);
            totalScore += points;
            OnScoreAdded?.Invoke(points); //AVISA A LA UI

            Debug.Log($"✅ Correcto +{points} puntos");
        }
        else
        {
            Debug.Log("❌ Incorrecto +0 puntos");
        }

        CheckGameEnd();
    }

    private void OnTimeExpired()
    {
        questionsAnswered++;
        Debug.Log("⏱ Tiempo agotado");
    }

    private void CheckGameEnd()
    {
        if (questionsAnswered >= maxQuestions)
        {
            EndGame();
        }
    }

    private void EndGame()
    {
        ScoreManager.Instance.SetFinalScore(totalScore);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Puntajes");
    }

    // =========================
    // GETTERS
    // =========================

    public float GetTime() => currentTime;
    public int GetScore() => totalScore;
}
