using UnityEngine;

[CreateAssetMenu(
    fileName = "Category",
    menuName = "Quiz/Category"
)]
public class CategorySO : ScriptableObject
{
    public string id;
    public string categoryName;
    public Sprite icon;
}
