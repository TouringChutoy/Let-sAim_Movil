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
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            ReiniciarPuntaje();
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
