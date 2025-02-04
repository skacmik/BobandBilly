using UnityEngine;

public class ButtonTrigger : MonoBehaviour
{
    public DoorManager doorManager; // Odkaz na spr�vce dve��
    public bool isForPlayerBig; // True = tla��tko pro velk�ho hr��e, False = tla��tko pro mal�ho

    void OnTriggerEnter2D(Collider2D other)
    {
        if (isForPlayerBig && other.CompareTag("PlayerBig"))
        {
            doorManager.SetPlayerBig(true);
            Debug.Log("PlayerBig vstoupil na tla��tko.");
        }
        if (!isForPlayerBig && other.CompareTag("PlayerSmall"))
        {
            doorManager.SetPlayerSmall(true);
            Debug.Log("PlayerSmall vstoupil na tla��tko.");
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (isForPlayerBig && other.CompareTag("PlayerBig"))
        {
            doorManager.SetPlayerBig(false);
            Debug.Log("PlayerBig slezl z tla��tka.");
        }
        if (!isForPlayerBig && other.CompareTag("PlayerSmall"))
        {
            doorManager.SetPlayerSmall(false);
            Debug.Log("PlayerSmall slezl z tla��tka.");
        }
    }
}
