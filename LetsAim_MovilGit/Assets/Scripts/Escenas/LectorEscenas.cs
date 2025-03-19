using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LectorEscenas : MonoBehaviour
{
    public void IniciarJuego()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;

    }

    public void Menu()
    {
        SceneManager.LoadScene(0);
    }

    public void Nivel1()
    {
        SceneManager.LoadScene(1);
        Time.timeScale = 1f;

    }

    public void Nivel2()
    {
        SceneManager.LoadScene(2);
        Time.timeScale = 1f;

    }

    public void Nivel3()
    {
        SceneManager.LoadScene(3);
        Time.timeScale = 1f;

    }

    public void Niveles()
    {
        SceneManager.LoadScene(4);
    }

        public void Shop()
    {
        SceneManager.LoadScene(5);
    }

    public void Creditos()
    {
        SceneManager.LoadScene(6);
    }

    public void Salir()
    {
        Application.Quit();
    }
}