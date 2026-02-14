using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PuntajesUI : MonoBehaviour
{
    public InputField nameInput;
    public Text scoreText;
    public Text rankingText;

    private void Start()
    {
        scoreText.text = "Puntaje: " + ScoreManager.Instance.GetFinalScore();
        DisplayRanking();
    }

    public void ConfirmScore()
    {
        ScoreManager.Instance.SaveScore(nameInput.text);
        DisplayRanking();
    }

    void DisplayRanking()
    {
        rankingText.text = "";

        for (int i = 0; i < ScoreManager.Instance.scores.Count; i++)
        {
            var s = ScoreManager.Instance.scores[i];
            rankingText.text += $"{i + 1}. {s.name} - {s.points}\n";
        }
    }

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }
}
