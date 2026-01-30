using UnityEngine;
using UnityEngine.SceneManagement;

public class DJEfectosDeSonidos : MonoBehaviour
{
    public void StartGame()
    {
        // SFX de click
        AudioManager.Instance.PlaySFX("Boton");

        // Cargar escena Game
        SceneManager.LoadScene("Game");
    }
}
