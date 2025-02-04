using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
    public float activeTime = 3f; // Jak dlouho je laser zapnut�
    public float inactiveTime = 2f; // Jak dlouho je laser vypnut�
    private SpriteRenderer spriteRenderer;
    private BoxCollider2D boxCollider;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        boxCollider = GetComponent<BoxCollider2D>();
        StartCoroutine(ActivateLaser());
    }

    private IEnumerator ActivateLaser()
    {
        while (true)
        {
            spriteRenderer.enabled = true;
            boxCollider.enabled = true;
            Debug.Log("Laser zapnut�!");
            yield return new WaitForSeconds(activeTime);

            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
            Debug.Log("Laser vypnut�!");
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig") || other.CompareTag("PlayerSmall"))
        {
            Debug.Log(other.gameObject.name + " byl zasa�en laserem!");
            Destroy(other.gameObject); // Hr�� zem�e, pokud se dotkne laseru
        }
    }
}
