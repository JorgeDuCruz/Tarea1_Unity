using UnityEngine;
using UnityEngine.SceneManagement; // Necesario para reiniciar la escena

public class GameManager : MonoBehaviour
{
    public static GameManager gameController;
    public static float bestTime = 0;
    private bool esperandoReinicio = false;

    void Start()
    {
        if (gameController == null)
        {
            gameController = this;
        }
    }

    void Update()
    {
        print("GameManager");
        if (esperandoReinicio && Input.GetKeyDown(KeyCode.R))
        {
            ReiniciarJuego();
        }
    }

    public void ActivarEstadoEspera()
    {
        esperandoReinicio = true;
        Time.timeScale = 0f; // Congela el movimiento y la física
        Debug.Log("Presiona ESPACIO para volver a empezar");
        
        // Aquí podrías activar un panel de UI que diga "Presiona Espacio"
    }

    void ReiniciarJuego()
    {
        Time.timeScale = 1f; // ¡Importante! Si no lo devuelves a 1, el juego seguirá congelado
        
        // Carga la escena actual de nuevo
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}