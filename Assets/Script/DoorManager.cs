using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door; // Br�na, kter� se otev�e/zav�e
    private bool playerBigOnButton = false;
    private bool playerSmallOnButton = false;

    public void SetPlayerBig(bool state)
    {
        playerBigOnButton = state;
        CheckDoorState();
    }

    public void SetPlayerSmall(bool state)
    {
        playerSmallOnButton = state;
        CheckDoorState();
    }

    void CheckDoorState()
    {
        Debug.Log("Kontrola tla��tek: PlayerBig: " + playerBigOnButton + ", PlayerSmall: " + playerSmallOnButton);

        if (playerBigOnButton && playerSmallOnButton)
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

