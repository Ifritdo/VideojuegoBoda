using UnityEngine;
using TMPro;
using System.Collections;

public class RouletteFlow : MonoBehaviour
{
    [Header("Refs")]
    [SerializeField] private WheelController wheel;
    [SerializeField] private QuizManager quizManager;
    [SerializeField] private QuizUI quizUI;

    [Header("UI")]
    [SerializeField] private GameObject panelRuleta;
    [SerializeField] private GameObject panelCategoria;
    [SerializeField] private TMP_Text categoryText;

    public void Spin()
    {
        wheel.Spin();
    }

    public void OnWheelFinished()
    {
        Debug.Log("📢 Ruleta finalizada");
        CategorySO category = quizManager.GetRandomCategory();

        categoryText.text = category.categoryName;
        panelCategoria.SetActive(true);

        StartCoroutine(ShowQuiz(category));
    }

    private IEnumerator ShowQuiz(CategorySO category)
    {
        yield return new WaitForSeconds(2f);

        panelRuleta.SetActive(false);
        panelCategoria.SetActive(false);

        quizUI.ShowQuestion(category);
    }

    public void ReturnToRoulette()
    {
        panelRuleta.SetActive(true);
    }

}
