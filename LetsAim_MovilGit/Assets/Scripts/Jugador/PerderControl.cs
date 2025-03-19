using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PerderControl : MonoBehaviour
{
    private MovimientoMovil movimientoJugador;
    [SerializeField] private float tiempoPerdiaControl;
    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        movimientoJugador = GetComponent<MovimientoMovil>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TomarGolpe(Vector2 posicion)
    {
        StartCoroutine(PerdidaControl());
        movimientoJugador.Rebote(posicion);
    }

    public IEnumerator PerdidaControl()
    {
        movimientoJugador.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdiaControl);
        movimientoJugador.sePuedeMover = true;
    }
}
