using UnityEngine;

public class CurtainEvents : MonoBehaviour
{
    public void OnCurtainClosed()
    {
        GameManager.Instance.OnCurtainClosed();
    }
}