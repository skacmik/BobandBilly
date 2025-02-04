using UnityEngine;

public class DualButtonDoor : MonoBehaviour
{
    public GameObject door; // Dveøe, které se mají otevøít
    private bool playerBigOnButton = false;
    private bool playerSmallOnButton = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig"))
        {
            playerBigOnButton = true;
            Debug.Log("PlayerBig je na svém tlaèítku.");
        }
        if (other.CompareTag("PlayerSmall"))
        {
            playerSmallOnButton = true;
            Debug.Log("PlayerSmall je na svém tlaèítku.");
        }

        CheckDoorState();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig"))
        {
            playerBigOnButton = false;
            Debug.Log("PlayerBig slezl z tlaèítka.");
        }
        if (other.CompareTag("PlayerSmall"))
        {
            playerSmallOnButton = false;
            Debug.Log("PlayerSmall slezl z tlaèítka.");
        }

        CheckDoorState();
    }

    void CheckDoorState()
    {
        Debug.Log("Kontrola tlaèítek: PlayerBig: " + playerBigOnButton + ", PlayerSmall: " + playerSmallOnButton);

        if (playerBigOnButton && playerSmallOnButton) // Pokud oba hráèi jsou na svých tlaèítkách
        {
            Debug.Log("Dveøe OTEVØENY!");
            door.SetActive(false);
        }
        else
        {
            Debug.Log("Dveøe ZAVØENY!");
            door.SetActive(true);
        }
    }
}
