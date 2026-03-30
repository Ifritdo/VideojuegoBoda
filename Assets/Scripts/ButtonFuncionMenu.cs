using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFuncionMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void LoadGame()
    {
        // Cargar escena Game
        SceneManager.LoadScene("Game");
    }
}
