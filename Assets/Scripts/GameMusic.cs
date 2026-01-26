using UnityEngine;

public class GameMusic : MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("Game");
    }
}
