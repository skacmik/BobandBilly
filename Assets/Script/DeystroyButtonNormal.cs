using UnityEngine;

public class DestroyButtonNormal : MonoBehaviour
{
    public GameObject objectToDestroy; // Objekt, který má být znièen

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrola, zda hráè interaguje s tlaèítkem
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            DestroyObject(); // Zavolá metodu na znièení objektu
        }
    }

    private void DestroyObject()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy); // Znièí objekt
        }
        else
        {
            Debug.LogWarning("Žádný objekt není pøiøazen k odstranìní!");
        }
    }
}