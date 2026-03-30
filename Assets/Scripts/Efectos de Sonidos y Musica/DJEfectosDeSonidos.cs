using UnityEngine;
using UnityEngine.SceneManagement;

public class DJEfectosDeSonidos : MonoBehaviour
{
    public void ButtonClick()
    {
        // SFX de click
        AudioManager.Instance.PlaySFX("Boton");
    }
}
