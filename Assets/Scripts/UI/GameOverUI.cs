using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TMP_Text finalScoreText;
    [SerializeField] private TMP_InputField nameInput;
    [SerializeField] private TMP_Text positionText;
    [SerializeField] private TMP_Text rankingMessageText;

    private int finalScore;

    public void Show(int score)
    {
        gameObject.SetActive(true);

        finalScore = score;

        finalScoreText.text = "Puntaje final: " + score;

        int position = ScoreManager.Instance.GetPlayerPosition(score);

        positionText.text = "Posición: #" + position;

        if (position <= 20)
            rankingMessageText.text = "¡Entraste al Top 20!";
        else
            rankingMessageText.text = "No entraste al Top 20";
    }

    public void GoToRanking()
    {
        if (string.IsNullOrWhiteSpace(nameInput.text))
            return;

        ScoreManager.Instance.SaveScore(nameInput.text);

        SceneManager.LoadScene("Puntajes");
    }
}