using UnityEngine;

public class AdminPanelToggle : MonoBehaviour
{
    public GameObject panel;

    void Update()
    {
        if (Input.GetKey(KeyCode.F1) && Input.GetKeyDown(KeyCode.Alpha2))
        {
            panel.SetActive(!panel.activeSelf);
        }
    }
}