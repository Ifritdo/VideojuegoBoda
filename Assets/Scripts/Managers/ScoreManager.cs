using UnityEngine;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    private int finalScore;

    public List<ScoreData> scores = new List<ScoreData>();

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        LoadScores();
    }

    public void SetFinalScore(int score)
    {
        finalScore = score;
    }

    public int GetFinalScore()
    {
        return finalScore;
    }

    public void SaveScore(string playerName)
    {
        scores.Add(new ScoreData(playerName, finalScore));
        scores.Sort((a, b) => b.points.CompareTo(a.points));

        if (scores.Count > 10)
            scores.RemoveAt(10);

        SaveToPrefs();
    }

    void SaveToPrefs()
    {
        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetString("Name" + i, scores[i].name);
            PlayerPrefs.SetInt("Points" + i, scores[i].points);
        }
    }

    void LoadScores()
    {
        scores.Clear();

        for (int i = 0; i < 10; i++)
        {
            if (PlayerPrefs.HasKey("Name" + i))
            {
                string name = PlayerPrefs.GetString("Name" + i);
                int points = PlayerPrefs.GetInt("Points" + i);
                scores.Add(new ScoreData(name, points));
            }
        }
    }
}
