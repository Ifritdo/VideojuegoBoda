using System.Collections;
using TMPro;
using UnityEngine;

public class ScoreUI : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI addScoreText;
    [SerializeField] private GameObject textPuntaje;
    [SerializeField] private GameObject textPuntajeTotal;
    [SerializeField] private GameObject textVolverMenu;

    [Header("Animation")]
    [SerializeField] private float floatDuration = 1f;
    [SerializeField] private float floatDistance = 40f;

    private int currentScore = 0;

    private void Start()
    {
        addScoreText.gameObject.SetActive(false);

        currentScore = GameManager.Instance.GetScore();
        UpdateScoreText();

        GameManager.Instance.OnScoreAdded += OnScoreAdded;
    }

    private void OnDestroy()
    {
        if (GameManager.Instance != null)
            GameManager.Instance.OnScoreAdded -= OnScoreAdded;
    }

    private void OnScoreAdded(int amount)
    {
        StartCoroutine(PlayAddScore(amount));
    }

    private IEnumerator PlayAddScore(int amount)
    {
        addScoreText.gameObject.SetActive(true);
        addScoreText.text = $"+{amount}";

        Vector3 startPos = addScoreText.transform.localPosition;
        Vector3 endPos = startPos + Vector3.up * floatDistance;

        float t = 0f;

        while (t < floatDuration)
        {
            t += Time.deltaTime;
            float normalized = t / floatDuration;

            addScoreText.transform.localPosition =
                Vector3.Lerp(startPos, endPos, normalized);

            addScoreText.alpha = 1f - normalized;

            yield return null;
        }

        addScoreText.gameObject.SetActive(false);
        addScoreText.transform.localPosition = startPos;
        addScoreText.alpha = 1f;

        currentScore += amount;
        UpdateScoreText();
    }

    private void UpdateScoreText()
    {
        scoreText.text = currentScore.ToString();
    }

    public void HideScoreUI()
    {
        textPuntaje.SetActive(false);
        textPuntajeTotal.SetActive(false);
        textVolverMenu.SetActive(false);
    }
}
