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
    [SerializeField] private GameObject viewRankingButton;

    private void Awake()
    {
        gameObject.SetActive(false);
    }

    public void Show(int finalScore)
    {
        gameObject.SetActive(true);

        finalScoreText.text = finalScore.ToString();
        viewRankingButton.SetActive(false);
    }

    public void OnConfirm()
    {
        if (string.IsNullOrWhiteSpace(nameInput.text))
            return;

        int position = ScoreManager.Instance.SaveScore(nameInput.text);

        positionText.text = position.ToString();

        if (position <= 20)
            rankingMessageText.text = "¡Entraste al Top 20!";
        else
            rankingMessageText.text = "No entraste al Top 20";

        viewRankingButton.SetActive(true);
    }

    public void GoToRanking()
    {
        SceneManager.LoadScene("Ranking");
    }
}