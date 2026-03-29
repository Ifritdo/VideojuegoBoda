using UnityEngine;

public class ScoreMusic: MonoBehaviour
{
    private void Start()
    {
        AudioManager.Instance.PlayMusic("Puntajes");
    }

}
