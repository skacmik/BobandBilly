using UnityEngine;

public class DestroyButtonNormal : MonoBehaviour
{
    public GameObject objectToDestroy; // Objekt, kter� m� b�t zni�en

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Kontrola, zda hr�� interaguje s tla��tkem
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            DestroyObject(); // Zavol� metodu na zni�en� objektu
        }
    }

    private void DestroyObject()
    {
        if (objectToDestroy != null)
        {
            Destroy(objectToDestroy); // Zni�� objekt
        }
        else
        {
            Debug.LogWarning("��dn� objekt nen� p�i�azen k odstran�n�!");
        }
    }
}