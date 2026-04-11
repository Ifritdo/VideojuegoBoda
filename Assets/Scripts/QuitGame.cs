using UnityEngine;

public class QuitGame : MonoBehaviour
{
    public void Quit()
    {
        Debug.Log("Saliendo del juego...");
        Application.Quit();
    }
}