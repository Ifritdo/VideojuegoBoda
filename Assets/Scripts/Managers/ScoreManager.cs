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

    public int SaveScore(string playerName)
    {
        ScoreData newScore = new ScoreData(playerName, finalScore);
        scores.Add(newScore);

        scores.Sort((a, b) => b.points.CompareTo(a.points));

        SaveToPrefs();

        return scores.IndexOf(newScore) + 1;
    }

    public int GetPlayerPosition(int points)
    {
        int position = scores.Count + 1;

        for (int i = 0; i < scores.Count; i++)
        {
            if (points > scores[i].points)
            {
                position = i + 1;
                break;
            }
        }

        return position;
    }

    void SaveToPrefs()
    {
        PlayerPrefs.SetInt("ScoreCount", scores.Count);

        for (int i = 0; i < scores.Count; i++)
        {
            PlayerPrefs.SetString("Name" + i, scores[i].name);
            PlayerPrefs.SetInt("Points" + i, scores[i].points);
        }

        PlayerPrefs.Save();
    }


    void LoadScores()
    {
        scores.Clear();

        int count = PlayerPrefs.GetInt("ScoreCount", 0);

        for (int i = 0; i < count; i++)
        {
            string name = PlayerPrefs.GetString("Name" + i);
            int points = PlayerPrefs.GetInt("Points" + i);
            scores.Add(new ScoreData(name, points));
        }
    }
    public void ClearScores() // Temporal
    {
        PlayerPrefs.DeleteAll();
        scores.Clear();
    }
}
