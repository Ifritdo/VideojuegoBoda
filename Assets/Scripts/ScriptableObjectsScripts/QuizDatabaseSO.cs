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
}
