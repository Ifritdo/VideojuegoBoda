using TMPro;
using UnityEngine;

public class AdminPanelUI : MonoBehaviour
{
    public TextMeshProUGUI textoTop;
    public TextMeshProUGUI textoMejor;
    public TextMeshProUGUI textoPeor;
    public TextMeshProUGUI textoJugadores;

    void OnEnable()
    {
        ActualizarPanel();
    }

    void ActualizarPanel()
    {
        // TOP 1
        var scores = ScoreManager.Instance.scores;

        if (scores.Count > 0)
        {
            var top = scores[0];
            textoTop.text = "TOP 1:\n" + top.name + " - " + top.points + " pts";
        }

        // Mejor pregunta
        var mejor = StatsManager.instance.GetMejorPregunta();
        if (mejor != null)
        {
            textoMejor.text = "Mejor pregunta:\n\"" + mejor.QuestionText + "\"";
        }

        // Peor pregunta
        var peor = StatsManager.instance.GetPeorPregunta();
        if (peor != null)
        {
            textoPeor.text = "Peor pregunta:\n\"" + peor.QuestionText + "\"";
        }

        // Jugadores
        textoJugadores.text = "Jugadores completados: " +
            StatsManager.instance.jugadoresCompletados;
    }
}