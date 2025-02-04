using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public DoorManager doorManager; // Odkaz na správce dveøí
    public bool isForPlayerBig; // True = tlaèítko pro velkého hráèe, False = tlaèítko pro malého

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isForPlayerBig && other.CompareTag("PlayerBig"))
        {
            doorManager.SetPlayerBig(true);
            Debug.Log("PlayerBig vstoupil na tlaèítko.");
        }
        if (!isForPlayerBig && other.CompareTag("PlayerSmall"))
        {
            doorManager.SetPlayerSmall(true);
            Debug.Log("PlayerSmall vstoupil na tlaèítko.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isForPlayerBig && other.CompareTag("PlayerBig"))
        {
            doorManager.SetPlayerBig(false);
            Debug.Log("PlayerBig slezl z tlaèítka.");
        }
        if (!isForPlayerBig && other.CompareTag("PlayerSmall"))
        {
            doorManager.SetPlayerSmall(false);
            Debug.Log("PlayerSmall slezl z tlaèítka.");
        }
    }
}
