using UnityEngine;
using System.Collections;

public class MovingPlatformLR: MonoBehaviour
{
    public Transform platform; // Plošina, která se bude pohybovat
    public float moveSpeed = 2f; // Rychlost pohybu plošiny
    public float moveDistance = 5f; // Jak daleko se plošina pohne (zleva doprava)
    public float stopTime = 0.5f; // Pauza na konci pohybu

    private Vector3 initialPosition; // Poèáteèní pozice plošiny
    private Vector3 targetPosition; // Cílová pozice plošiny
    private int playersOnButton = 0; // Poèet hráèù na tlaèítku
    private Coroutine moveCoroutine; // Odkaz na aktuální pohybovou rutinu

    private void Start()
    {
        if (platform != null)
        {
            initialPosition = platform.position;
            targetPosition = initialPosition + new Vector3(moveDistance, 0, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            playersOnButton++;
            if (playersOnButton == 1) // Pokud je první hráè na tlaèítku, pohni plošinou
            {
                StartMoving(targetPosition);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            playersOnButton--;
            if (playersOnButton == 0) // Plošina se vrátí pouze když tlaèítko opustí poslední hráè
            {
                StartCoroutine(ReturnWithDelay());
            }
        }
    }

    private void StartMoving(Vector3 destination)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine); // Zastaví pøedchozí pohyb
        moveCoroutine = StartCoroutine(MovePlatformSmooth(destination));
    }

    private IEnumerator MovePlatformSmooth(Vector3 destination)
    {
        float elapsedTime = 0f;
        Vector3 startPosition = platform.position;
        float journeyTime = Vector3.Distance(startPosition, destination) / moveSpeed;

        while (elapsedTime < journeyTime)
        {
            elapsedTime += Time.deltaTime;
            platform.position = Vector3.Lerp(startPosition, destination, elapsedTime / journeyTime);
            yield return null;
        }

        platform.position = destination; // Zajisti pøesné zarovnání na cílovou pozici
        moveCoroutine = null; // Pohyb je dokonèen
    }

    private IEnumerator ReturnWithDelay()
    {
        yield return new WaitForSeconds(stopTime); // Pauza pøed návratem plošiny
        StartMoving(initialPosition);
    }
}
