using UnityEngine;
using UnityEngine.UI;  // Para mostrar el mensaje (si luego agregamos UI)
using TMPro;

public class MarieMovimiento : MonoBehaviour
{
    public float velocidad = 5f;
    public int vidas = 3;
    private Rigidbody2D rb;
    private Vector3 posicionInicial;
    private bool estaViva = true;

    private bool tieneFlor = false;
    private MensajeUI mensajeUI;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Start()
    {
        posicionInicial = transform.position;
        mensajeUI = FindObjectOfType<MensajeUI>();
        if (rb == null)
        {
            Debug.LogError("❌ Rigidbody2D no encontrado.");
            enabled = false;
        }
        else
        {
            rb.gravityScale = 0;
            rb.freezeRotation = true;
        }
    }

    void FixedUpdate()
    {
        if (!estaViva || rb == null) return;

        float moverX = Input.GetAxisRaw("Horizontal");
        float moverY = Input.GetAxisRaw("Vertical");

        Vector2 movimiento = new Vector2(moverX, moverY).normalized;
        Vector2 nuevaPos = rb.position + movimiento * velocidad * Time.fixedDeltaTime;

        rb.MovePosition(nuevaPos);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!estaViva) return;

        if (collision.gameObject.CompareTag("Obstaculo"))
        {
            vidas--;
            if (mensajeUI != null)
            {
                mensajeUI.MostrarMensaje("¡Cuidado! Has perdido una vida. Te quedan " + vidas + ".");
            }
            Debug.Log("¡Chocaste con un obstáculo! Vidas restantes: " + vidas);

            if (vidas > 0)
                rb.position = posicionInicial;
            else
            {
                estaViva = false;
                if (mensajeUI != null)
                {
                    mensajeUI.MostrarMensaje("💀 Game Over: Marie ha perdido todas sus vidas.");
                    mensajeUI.botonReiniciar.SetActive(true);
                }
                Debug.Log("💀 Game Over: Marie ha perdido todas sus vidas.");
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!estaViva) return;

        if (other.CompareTag("FlorMagica") && !tieneFlor)
        {
            tieneFlor = true;
            Destroy(other.gameObject); // elimina la flor
            if (mensajeUI != null)
            {
                mensajeUI.MostrarMensaje("🌸 Has recogido la flor mágica. ¡Llévala al lago!");
            }
            Debug.Log("🌸 Marie ha recogido la flor mágica. ¡Ahora llévala al pueblo!");
        }

        if (other.CompareTag("CentroPueblo") && tieneFlor)
        {
            estaViva = false;
            Debug.Log("🏆 Has salvado al pueblo, ¡eres una gran heroína!");
            if (mensajeUI != null)
            {
                mensajeUI.MostrarMensaje("SIII ¡Has salvado al pueblo, gran heroína!");
                mensajeUI.botonReiniciar.SetActive(true);
                mensajeUI.efectoFlores.SetActive(true);
            }
            // Aquí puedes activar animación, flores o UI
        }
    }
}

