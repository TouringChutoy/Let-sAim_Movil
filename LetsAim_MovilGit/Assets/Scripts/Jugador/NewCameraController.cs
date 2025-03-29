using UnityEngine;

public class NewCameraController : MonoBehaviour
{
    private Transform player;

    void Start()
    {
        FindPlayer();
    }

    void LateUpdate()
    {
        if (player == null)
        {
            FindPlayer();
            return;
        }

        // Tu lógica de seguimiento vertical aquí
        float targetY = player.position.y;
        transform.position = new Vector3(transform.position.x, targetY, transform.position.z);
    }

    void FindPlayer()
    {
        // Busca por tag (asegúrate que tu prefab jugador tenga el tag "Player")
        GameObject playerObj = GameObject.FindWithTag("Player");

        if (playerObj != null)
        {
            player = playerObj.transform;
            Debug.Log("Jugador encontrado: " + player.name);
        }
        else
        {
            Debug.LogWarning("No se encontró objeto con tag 'Player'");
        }
    }
}