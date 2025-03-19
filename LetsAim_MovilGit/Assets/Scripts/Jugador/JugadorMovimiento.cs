using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JugadorMovimiento : MonoBehaviour
{
    public bool sePuedeMover = true;
    [SerializeField] public Vector2 velocidadRebote;
    
    [Header("Salto")]
    [SerializeField] private float fuerzaDeSalto;
    [SerializeField] private LayerMask queEsSuelo;
    [SerializeField] private Transform controladorsuelo;
    [SerializeField] private Vector3 dimensionesCaja;
    [SerializeField] private bool enSuelo;
    private bool salto = false;

    [SerializeField] private AudioClip saltoSonido;

    [SerializeField] private int saltosExtrasRestantes;
    [SerializeField] private int saltosExtras;

    private BoxCollider2D boxCollider;
    Rigidbody2D player;
    public Animator animator;
    public SpriteRenderer spriteRenderer;

    public float speed = 3f;

    void Start()
    {
        player = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        Movimiento(salto);
        float axisValue = Input.GetAxis("Horizontal");
        bool volteandoDerecha = true;
        if (Mathf.Abs(axisValue) > 0)
        {
            spriteRenderer.flipX = axisValue < 0;
        }

        volteandoDerecha = !spriteRenderer.flipX;

        if (sePuedeMover)
        {
            transform.Translate(Vector3.right * Time.deltaTime * Input.GetAxis("Horizontal") * speed);
        }

        if (enSuelo)
        {
            saltosExtrasRestantes = saltosExtras;
        }
        
        if (Input.GetButtonDown("Jump"))
        {
            salto = true;
        }
    }

    private void LateUpdate()
    {
        animator.SetFloat("Move", Mathf.Abs(Input.GetAxis("Horizontal")));
        //spriteRenderer.flipX = (Input.GetAxis("Horizontal") < 0);
    }

    private void Movimiento(bool salto)
    {
        if (salto)
        {
            if (enSuelo)
            {
                Salto();
            }
            else
            {
                DobleSalto();
            }
        }
    }

    private void Salto()
    {
        enSuelo = false;
        //player.AddForce(new Vector2(0f, fuerzaDeSalto));
        player.linearVelocity = new Vector2(0f, fuerzaDeSalto);
        ControladorSonido.Instance.EjecutarSonido(saltoSonido);
    }

    private void FixedUpdate()
    {
        enSuelo = Physics2D.OverlapBox(controladorsuelo.position, dimensionesCaja, 0f, queEsSuelo);
        animator.SetBool("salto", enSuelo);
        animator.SetFloat("VelY", player.linearVelocity.y);
       
        
        Movimiento(salto);
        salto = false;
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(controladorsuelo.position, dimensionesCaja);
    }

    public void DobleSalto()
    {
          if (salto && saltosExtrasRestantes > 0)
          {
               Salto();
               saltosExtrasRestantes -= 1;
          }
    }

    public void Rebote(Vector2 puntoGolpe)
    {
        player.linearVelocity = new Vector2(-velocidadRebote.x * puntoGolpe.x, velocidadRebote.y);
    }

}