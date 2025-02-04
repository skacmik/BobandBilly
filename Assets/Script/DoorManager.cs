using UnityEngine;

public class DoorManager : MonoBehaviour
{
    public GameObject door; // Brána, která se otevøe/zavøe
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
        Debug.Log("Kontrola tlaèítek: PlayerBig: " + playerBigOnButton + ", PlayerSmall: " + playerSmallOnButton);

        if (playerBigOnButton && playerSmallOnButton)
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

