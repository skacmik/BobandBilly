using UnityEngine;

public class DestroyButton : MonoBehaviour
{
    public GameObject objectToDestroy; // Objekt, který má být zničen
    private int objectsOnButton = 0; // Počítá objekty na tlačítku

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pokud na tlačítko vstoupí velký hráč, malý hráč nebo pushable objekt
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall") || collision.CompareTag("PushableObject"))
        {
            objectsOnButton++;

            if (objectsOnButton == 1) // Pokud je to první objekt na tlačítku, odstraníme překážku
            {
                DestroyObject();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Když objekt opustí tlačítko
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall") || collision.CompareTag("PushableObject"))
        {
            objectsOnButton--;

            if (objectsOnButton == 0) // Pokud tlačítko není zatížené, překážka se vrátí
            {
                RestoreObject();
            }
        }
    }

    private void DestroyObject()
    {
        if (objectToDestroy != null)
        {
            objectToDestroy.SetActive(false); // Objekt "zmizí", ale není zničen
        }
        else
        {
            Debug.LogWarning("⚠️ Žádný objekt není přiřazen k odstranění!");
        }
    }

    private void RestoreObject()
    {
        if (objectToDestroy != null)
        {
            objectToDestroy.SetActive(true); // Překážka se vrátí zpět
        }
    }
}
