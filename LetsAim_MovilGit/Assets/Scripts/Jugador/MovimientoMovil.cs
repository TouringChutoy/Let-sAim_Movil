using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovimientoMovil : MonoBehaviour
{
    [Header("Movimiento")]
    [Range(0, 0.3f)][SerializeField] private float suavizadoDeMovimiento;
    private bool mirandoDerecha = true;
    [SerializeField] private float velocidadMovimiento;
    private float movimientoHorizontal = 0f;
    [SerializeField] private Rigidbody2D rb2D;
    private Vector3 velocidad = Vector3.zero;

    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorSuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;

    private EntradaMovimientos entradaMovimientos;
    [SerializeField] public Vector2 velocidadRebote;
    public bool sePuedeMover = true;
    public Animator animator;
    private BoxCollider2D boxCollider;




    private void Awake()
    {
        entradaMovimientos = new EntradaMovimientos();
    }

    private void OnEnable()
    {
        entradaMovimientos.Enable();
    }

    private void OnDisable()
    {
        entradaMovimientos.Disable();
    }

    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        entradaMovimientos.Movimiento.Salto.performed += contexto => Salto(contexto);
        animator = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    private void Update()
    {
        if (sePuedeMover)
        {
        movimientoHorizontal = entradaMovimientos.Movimiento.Horizontal.ReadValue<float>() * velocidadMovimiento;
        }

    }

    private void Salto(InputAction.CallbackContext contexto)
    {
        salto = true;
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorSuelo.position, dimensionesCaja, 0f, queEsSuelo);

        Mover(movimientoHorizontal * Time.fixedDeltaTime, salto);

        salto = false;
    }

    private void Mover(float mover, bool saltar)
    {
        Vector3 velocidadObjetivo = new Vector2(mover, rb2D.linearVelocity.y);
        rb2D.linearVelocity = Vector3.SmoothDamp(rb2D.linearVelocity, velocidadObjetivo, ref velocidad, suavizadoDeMovimiento);

        if (mover > 0 && !mirandoDerecha)
        {
            Girar();
        }
        else if (mover < 0 && mirandoDerecha)
        {
            Girar();
        }

        if (enSuelo && saltar)
        {
            enSuelo = false;
            rb2D.AddForce(new Vector2(0f, fuerzaDeSalto));
        }
    }

    private void Girar()
    {
        mirandoDerecha = !mirandoDerecha;
        Vector3 escala = transform.localScale;
        escala.x *= -1;
        transform.localScale = escala;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorSuelo.position, dimensionesCaja);
    }

        public void Rebote(Vector2 puntoGolpe)
    {
        rb2D.linearVelocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    }
}
