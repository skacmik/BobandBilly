using UnityEngine;

public class MovePlatformButton : MonoBehaviour
{
    public Transform platform; // Plošina, která se bude pohybovat
    public float moveSpeed = 2f; // Rychlost pohybu plošiny
    public float moveDistance = 5f; // Jak vysoko se plošina pohne

    private Vector3 initialPosition; // Poèáteèní pozice plošiny
    private Vector3 targetPosition; // Koneèná pozice plošiny
    private bool isPlayerOnButton = false; // Sleduje, zda je hráè na tlaèítku
    private Coroutine moveCoroutine; // Odkaz na aktuální pohybovou rutinu

    private void Start()
    {
        if (platform != null)
        {
            initialPosition = platform.position;
            targetPosition = initialPosition + new Vector3(0, moveDistance, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Pokud na tlaèítko vstoupí hráè
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            isPlayerOnButton = true;
            if (moveCoroutine != null) StopCoroutine(moveCoroutine); // Zastav aktuální pohyb
            moveCoroutine = StartCoroutine(MovePlatform(targetPosition)); // Plošina jede nahoru
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Pokud hráè opustí tlaèítko
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            isPlayerOnButton = false;
            if (moveCoroutine != null) StopCoroutine(moveCoroutine); // Zastav aktuální pohyb
            moveCoroutine = StartCoroutine(MovePlatform(initialPosition)); // Plošina jede dolù
        }
    }

    private System.Collections.IEnumerator MovePlatform(Vector3 destination)
    {
        while (Vector3.Distance(platform.position, destination) > 0.01f)
        {
            platform.position = Vector3.MoveTowards(platform.position, destination, moveSpeed * Time.deltaTime);
            yield return null; // Poèkej na další snímek
        }

        platform.position = destination; // Zajisti pøesné zarovnání na cílovou pozici
        moveCoroutine = null; // Pohyb je dokonèen
    }
}
