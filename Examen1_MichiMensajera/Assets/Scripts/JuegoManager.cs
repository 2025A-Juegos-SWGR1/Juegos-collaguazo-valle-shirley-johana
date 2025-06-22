
using UnityEngine;
using UnityEngine.SceneManagement;

public class JuegoManager : MonoBehaviour
{
    public void ReiniciarJuego()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}

