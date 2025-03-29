using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monedas : MonoBehaviour
{
    [SerializeField] private int cantidadPuntos;
    [SerializeField] private Puntaje puntaje;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        puntaje.SumarPuntos(cantidadPuntos);
        if(collision.CompareTag("Player")){
            Destroy(gameObject);
        }
    }
}
