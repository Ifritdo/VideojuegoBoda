using UnityEngine;

public class IntroCurtains : MonoBehaviour
{
    public float tiempo = 2f;

    void Start()
    {
        Destroy(gameObject, tiempo);
    }
}