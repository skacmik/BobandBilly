using UnityEngine;
using System.Collections;

public class TimedButtonDoor : MonoBehaviour
{
    public GameObject door; // Dve�e, kter� se otev�ou
    public float openTime = 3f; // Jak dlouho z�stanou dve�e otev�en�
    private bool isActivated = false; // Sleduje, jestli u� bylo tla��tko aktivov�no

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("PlayerBig") || other.CompareTag("PlayerSmall")) && !isActivated)
        {
            StartCoroutine(OpenDoorTemporarily());
            Debug.Log(other.gameObject.name + " aktivoval �asovan� tla��tko!");
        }
    }

    private IEnumerator OpenDoorTemporarily()
    {
        isActivated = true;
        door.SetActive(false); // Otev��t dve�e
        Debug.Log("Dve�e jsou OTEV�ENY na " + openTime + " sekund!");

        yield return new WaitForSeconds(openTime); // Po�kej nastaven� �as

        door.SetActive(true); // Zav��t dve�e
        Debug.Log("Dve�e jsou ZAV�ENY!");
        isActivated = false; // Tla��tko lze znovu aktivovat
    }
}
