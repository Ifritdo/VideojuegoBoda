using UnityEngine;

public class ResetRankingButton : MonoBehaviour
{
    public void ResetRanking()
    {
        ScoreManager.Instance.ClearScores();
    }
}