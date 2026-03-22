using System.Collections.Generic;
using UnityEngine;

public class StatsManager : MonoBehaviour
{
    public static StatsManager instance;

    public int jugadoresCompletados = 0;

    public Dictionary<QuestionSO, int> aciertos = new Dictionary<QuestionSO, int>();
    public Dictionary<QuestionSO, int> errores = new Dictionary<QuestionSO, int>();

    void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject);
    }

    public void RegistrarRespuesta(QuestionSO pregunta, bool correcta)
    {
        if (correcta)
        {
            if (!aciertos.ContainsKey(pregunta))
                aciertos[pregunta] = 0;

            aciertos[pregunta]++;
        }
        else
        {
            if (!errores.ContainsKey(pregunta))
                errores[pregunta] = 0;

            errores[pregunta]++;
        }
    }

    public void JugadorCompleto()
    {
        jugadoresCompletados++;
    }

    public QuestionSO GetMejorPregunta()
    {
        QuestionSO mejor = null;
        int max = -1;

        foreach (var p in aciertos)
        {
            if (p.Value > max)
            {
                max = p.Value;
                mejor = p.Key;
            }
        }

        return mejor;
    }

    public QuestionSO GetPeorPregunta()
    {
        QuestionSO peor = null;
        int max = -1;

        foreach (var p in errores)
        {
            if (p.Value > max)
            {
                max = p.Value;
                peor = p.Key;
            }
        }

        return peor;
    }
}