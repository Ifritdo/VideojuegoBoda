using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class VolumeUI : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI porcentajeTexto;

    private void Start()
    {
        UpdateText();
    }

    public void UpdateText()
    {
        int porcentaje = Mathf.RoundToInt(slider.value * 100);
        porcentajeTexto.text = porcentaje + "%";
    }
}