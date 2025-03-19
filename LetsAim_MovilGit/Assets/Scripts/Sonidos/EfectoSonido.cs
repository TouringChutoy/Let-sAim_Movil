using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EfectoSonido : MonoBehaviour
{
    [SerializeField] private AudioClip Golpe;

    /*private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            ControladorSonido.Instance.EjecutarSonido(Golpe);
        }
    }*/

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            ControladorSonido.Instance.EjecutarSonido(Golpe);
        }
    }
}
