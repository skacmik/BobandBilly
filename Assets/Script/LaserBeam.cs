using UnityEngine;
using System.Collections;

public class LaserBeam : MonoBehaviour
{
    public float activeTime = 3f; // Jak dlouho je laser zapnutý
    public float inactiveTime = 2f; // Jak dlouho je laser vypnutý
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
            Debug.Log("Laser zapnutý!");
            yield return new WaitForSeconds(activeTime);

            spriteRenderer.enabled = false;
            boxCollider.enabled = false;
            Debug.Log("Laser vypnutý!");
            yield return new WaitForSeconds(inactiveTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("PlayerBig") || other.CompareTag("PlayerSmall"))
        {
            Debug.Log(other.gameObject.name + " byl zasažen laserem!");
            Destroy(other.gameObject); // Hráè zemøe, pokud se dotkne laseru
        }
    }
}
