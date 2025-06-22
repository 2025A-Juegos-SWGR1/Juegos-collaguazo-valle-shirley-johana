

using TMPro;
using UnityEngine;

public class MensajeUI : MonoBehaviour
{
    public TextMeshProUGUI textoPantalla;
    public GameObject fondoPanel;
    public GameObject botonReiniciar; 
    public GameObject efectoFlores; 

    void Start()
    {
        MostrarMensajeInicio();
        botonReiniciar.SetActive(false);
    }

    public void MostrarMensajeInicio()
    {
        textoPantalla.text = "Hola eres Marie la gatita.\n Tu misión es salvar a tu pueblo \nllevando la flor al lago mágico.";
        Invoke("LimpiarMensaje", 5f); // lo borra a los 5 segundos
    }

    public void MostrarMensaje(string mensaje)
    {
        textoPantalla.text = mensaje;
        fondoPanel.SetActive(true);   // <- activa el fondo
        CancelInvoke();
        Invoke("LimpiarMensaje", 3f);
    }

    private void LimpiarMensaje()
    {
        textoPantalla.text = "";
        fondoPanel.SetActive(false);  // <- oculta el fondo
    }
}

