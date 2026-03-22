using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PuntajesUI : MonoBehaviour
{
    [Header("Podio")]
    [SerializeField] private TextMeshProUGUI firstName;
    [SerializeField] private TextMeshProUGUI firstScore;
    [SerializeField] private TextMeshProUGUI secondName;
    [SerializeField] private TextMeshProUGUI secondScore;
    [SerializeField] private TextMeshProUGUI thirdName;
    [SerializeField] private TextMeshProUGUI thirdScore;

    [Header("Columnas")]
    [SerializeField] private Transform column1;
    [SerializeField] private Transform column2;
    [SerializeField] private Transform column3;

    [SerializeField] private GameObject rankingItemPrefab;
    [SerializeField] private int maxDisplay = 18;

    private void Start()
    {
        DisplayRanking();
    }

    void DisplayRanking()
    {

        var scores = ScoreManager.Instance.scores;

        // ===== PODIO =====
        if (scores.Count > 0)
        {
            firstName.text = scores[0].name;
            firstScore.text = scores[0].points.ToString();
        }

        if (scores.Count > 1)
        {
            secondName.text = scores[1].name;
            secondScore.text = scores[1].points.ToString();
        }

        if (scores.Count > 2)
        {
            thirdName.text = scores[2].name;
            thirdScore.text = scores[2].points.ToString();
        }

        // ===== COLUMNAS =====
        ClearColumn(column1);
        ClearColumn(column2);
        ClearColumn(column3);

        int listStart = 3;

        if (scores.Count <= listStart)
            return;

        int totalListItems = Mathf.Min(scores.Count, maxDisplay) - listStart;
        totalListItems = Mathf.Max(0, totalListItems);

        int rows = Mathf.CeilToInt(totalListItems / 3f);

        for (int i = 0; i < totalListItems; i++)
        {
            int scoreIndex = listStart + i;

            GameObject item = Instantiate(rankingItemPrefab);

            item.transform.Find("PositionText")
                .GetComponent<TextMeshProUGUI>().text =
                (scoreIndex + 1) + "°";

            item.transform.Find("NameText")
                .GetComponent<TextMeshProUGUI>().text =
                scores[scoreIndex].name;

            item.transform.Find("ScoreText")
                .GetComponent<TextMeshProUGUI>().text =
                scores[scoreIndex].points.ToString();

            if (i < rows)
                item.transform.SetParent(column1, false);
            else if (i < rows * 2)
                item.transform.SetParent(column2, false);
            else
                item.transform.SetParent(column3, false);
        }
    }

    void ClearColumn(Transform column)
    {
        foreach (Transform child in column)
            Destroy(child.gameObject);
    }

    public void BackToMenu()
    {
        StatsManager.instance.JugadorCompleto();
        SceneManager.LoadScene("Menu");
    }
}
