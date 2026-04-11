using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;

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

        finalScoreText.text = "" + score;

        int position = ScoreManager.Instance.GetPlayerPosition(score);

        positionText.text = "#" + position;

        if (position <= 20)
            rankingMessageText.text = "¡Entraste al Top 20!";
        else
            rankingMessageText.text = "No entraste al Top 20";

        // IMPORTANTE
        nameInput.text = "";
        nameInput.Select();
        nameInput.ActivateInputField();
    }

    public void GoToRanking()
    {
        if (string.IsNullOrWhiteSpace(nameInput.text))
            return;

        ScoreManager.Instance.SaveScore(nameInput.text);

        SceneManager.LoadScene("Puntajes");
    }
}