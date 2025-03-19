using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Patrullaje : MonoBehaviour
{
    [SerializeField] private float VelocidadMovimiento;
    [SerializeField] private Transform[] puntosMovimiento;
    [SerializeField] private float distanciaMinima;
    private int SiguientPaso = 0;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        Girar();
    }

    private void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, puntosMovimiento[SiguientPaso].position, VelocidadMovimiento * Time.deltaTime);

        if (Vector2.Distance(transform.position, puntosMovimiento[SiguientPaso].position) < distanciaMinima)
        {
            SiguientPaso += 1;
            if (SiguientPaso >= puntosMovimiento.Length)
            {
                SiguientPaso = 0;
            }
            Girar();
        }
    }

    private void Girar()
    {
        if (transform.position.x < puntosMovimiento[SiguientPaso].position.x)
        {
            spriteRenderer.flipX = true;
        }
        else
        {
            spriteRenderer.flipX = false;
        }
    }
}
