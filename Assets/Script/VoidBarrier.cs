using UnityEngine;

public class VoidBarrier : MonoBehaviour
{
    private GameManager gameManager;

    private void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerSmall") || other.CompareTag("PlayerBig"))
        {
            Debug.Log(other.gameObject.name + " spadl do voidu!");
            gameManager.KillPlayers(); // Reset levelu
        }
    }
}
