using UnityEngine;
using System;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public event Action<int> OnScoreAdded;

    [Header("Cantidad de Rondas")]
    [SerializeField] private int maxQuestions = 10;

    [Header("Config")]
    public int pointsPerQuestion = 1000;
    public float maxTime = 20f;

    [Header("UI")]
    [SerializeField] private QuizUI quizUI;
    [SerializeField] private ScoreUI scoreUI;
    [SerializeField] private GameOverUI gameOverUI;
    [SerializeField] private GameObject cortinas;

    private float currentTime;
    private bool questionActive;

    private int totalScore = 0;
    private int questionsAnswered = 0;

    public int GetQuestionsAnswered() => questionsAnswered;
    public int GetMaxQuestions() => maxQuestions;
    private bool gameEnded = false;

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
            AudioManager.Instance.PlaySFX("Boton");
        }

        if (!questionActive) return;

        currentTime -= Time.deltaTime;

        if (currentTime <= 0f)
        {
            currentTime = 0f;
            AudioManager.Instance.PlaySFX("Alarm");
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

        CheckGameEnd();
    }

    private void CheckGameEnd()
    {
        Debug.Log("Preguntas respondidas: " + questionsAnswered);

        if (questionsAnswered >= maxQuestions)
        {
            Debug.Log("FIN DEL JUEGO");
            EndGame();
            return;
        }
    }

    private void EndGame()
    {
        if (gameEnded) return;

        gameEnded = true;
        questionActive = false;
        enabled = false;

        StartCoroutine(EndGameRoutine());
    }

    private IEnumerator EndGameRoutine()
    {
        Debug.Log("Esperando antes de mostrar panel final");

        yield return new WaitForSeconds(2.5f);
        quizUI.HideQuizUI();
        scoreUI.HideScoreUI();
        cortinas.SetActive(true);
    }
    public void OnCurtainClosed()
    {
        ScoreManager.Instance.SetFinalScore(totalScore);

        if (gameOverUI != null)
        {
            gameOverUI.Show(totalScore);
            gameOverUI.transform.SetAsLastSibling();
        }
    }

    // =========================
    // GETTERS
    // =========================

    public float GetTime() => currentTime;
    public int GetScore() => totalScore;


}
