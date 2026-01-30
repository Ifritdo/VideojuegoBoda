using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "Question",
    menuName = "Quiz/Question"
)]
public class QuestionSO : ScriptableObject
{
    public string id;

    [TextArea(2, 4)]
    public string QuestionText;

    public CategorySO Category;

    public string CorrectAnswer;

    public List<string> WrongAnswers;

    // Opcional para el futuro
    public int difficulty = 1;
}
