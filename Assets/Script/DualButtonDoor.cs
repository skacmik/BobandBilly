using UnityEngine;

public class DualButtonDoor : MonoBehaviour
{
    public GameObject door; // Dve�e, kter� se maj� otev��t
    private bool playerBigOnButton = false;
    private bool playerSmallOnButton = false;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig"))
        {
            playerBigOnButton = true;
            Debug.Log("PlayerBig je na sv�m tla��tku.");
        }
        if (other.CompareTag("PlayerSmall"))
        {
            playerSmallOnButton = true;
            Debug.Log("PlayerSmall je na sv�m tla��tku.");
        }

        CheckDoorState();
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig"))
        {
            playerBigOnButton = false;
            Debug.Log("PlayerBig slezl z tla��tka.");
        }
        if (other.CompareTag("PlayerSmall"))
        {
            playerSmallOnButton = false;
            Debug.Log("PlayerSmall slezl z tla��tka.");
        }

        CheckDoorState();
    }

    void CheckDoorState()
    {
        Debug.Log("Kontrola tla��tek: PlayerBig: " + playerBigOnButton + ", PlayerSmall: " + playerSmallOnButton);

        if (playerBigOnButton && playerSmallOnButton) // Pokud oba hr��i jsou na sv�ch tla��tk�ch
        {
            Debug.Log("Dve�e OTEV�ENY!");
            door.SetActive(false);
        }
        else
        {
            Debug.Log("Dve�e ZAV�ENY!");
            door.SetActive(true);
        }
    }
}
