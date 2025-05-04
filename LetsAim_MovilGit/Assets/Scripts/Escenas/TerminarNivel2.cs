using UnityEngine;
using UnityEngine.SceneManagement;

public class TerminarNivel2 : MonoBehaviour
{
    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Niveles");
            AchievementManager.Instance.TerminaNivel2();
        }
    }
}
