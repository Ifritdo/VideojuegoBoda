using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(
    fileName = "QuizDatabase",
    menuName = "Quiz/Database"
)]
public class QuizDatabaseSO : ScriptableObject
{
    public List<CategorySO> categories;
    public List<QuestionSO> questions;


    public List<QuestionSO> GetQuestionsByCategory(CategorySO category)
    {
        List<QuestionSO> result = new List<QuestionSO>();

        foreach (var q in questions)
        {
            if (q.Category == category)
                result.Add(q);
        }

        return result;
    }
}
