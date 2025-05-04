using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Puntaje : MonoBehaviour
{
    private int puntos;
    private TextMeshProUGUI textMesh;

    // Start is called before the first frame update
    void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        // Cargar el puntaje guardado
        puntos = PlayerPrefs.GetInt("PuntajeGuardado", 0);
        ActualizarTexto();

    }

    // Update is called once per frame
    void Update()
    {
        //puntos += Time.deltaTime;
        //textMesh.text = puntos.ToString("0");
        // Si se presiona la tecla "0", reiniciar el puntaje

        if (puntos >= 500)
        {
            AchievementManager.Instance.QuinientasMon();
        }
        if (puntos >= 1000)
        {
            AchievementManager.Instance.MilMon();
        }
        if (puntos >= 1500)
        {
            AchievementManager.Instance.MilQuinientasMon();
        }
        if (puntos >= 2000)
        {
            AchievementManager.Instance.DosMilMon();
        }
        if (puntos >= 2500)
        {
            AchievementManager.Instance.DosMilQuinientasMon();
        }
        if (puntos >= 3000)
        {
            AchievementManager.Instance.TresMilMon();
        }
        if (puntos >= 4000)
        {
            AchievementManager.Instance.CuatroMilMon();
        }
        if (puntos >= 5000)
        {
            AchievementManager.Instance.CincoMilMon();
        }
        if (puntos >= 10000)
        {
            AchievementManager.Instance.DiezMilMon();
        }
    }

    public void SumarPuntos(int puntosEntrada)
    {
        puntos += puntosEntrada;
        ActualizarTexto();

        // Guardar el puntaje actualizado
        PlayerPrefs.SetInt("PuntajeGuardado", puntos);
        PlayerPrefs.Save();

    }

    private void ReiniciarPuntaje()
    {
        puntos = 0;
        PlayerPrefs.SetInt("PuntajeGuardado", puntos);
        PlayerPrefs.Save();
        ActualizarTexto();
    }

    private void ActualizarTexto()
    {
        textMesh.text = puntos.ToString("0");
    }

}
