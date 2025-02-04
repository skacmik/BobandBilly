using UnityEngine;
using System.Collections;

public class TimedButtonDoor : MonoBehaviour
{
    public GameObject door; // Dveøe, které se otevøou
    public float openTime = 3f; // Jak dlouho zùstanou dveøe otevøené
    private bool isActivated = false; // Sleduje, jestli už bylo tlaèítko aktivováno

    void OnTriggerEnter2D(Collider2D other)
    {
        if ((other.CompareTag("PlayerBig") || other.CompareTag("PlayerSmall")) && !isActivated)
        {
            StartCoroutine(OpenDoorTemporarily());
            Debug.Log(other.gameObject.name + " aktivoval èasované tlaèítko!");
        }
    }

    private IEnumerator OpenDoorTemporarily()
    {
        isActivated = true;
        door.SetActive(false); // Otevøít dveøe
        Debug.Log("Dveøe jsou OTEVØENY na " + openTime + " sekund!");

        yield return new WaitForSeconds(openTime); // Poèkej nastavený èas

        door.SetActive(true); // Zavøít dveøe
        Debug.Log("Dveøe jsou ZAVØENY!");
        isActivated = false; // Tlaèítko lze znovu aktivovat
    }
}
