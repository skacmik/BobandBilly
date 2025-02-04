using UnityEngine;
using System.Collections;

public class MovingPlatformLR: MonoBehaviour
{
    public Transform platform; // Plo�ina, kter� se bude pohybovat
    public float moveSpeed = 2f; // Rychlost pohybu plo�iny
    public float moveDistance = 5f; // Jak daleko se plo�ina pohne (zleva doprava)
    public float stopTime = 0.5f; // Pauza na konci pohybu

    private Vector3 initialPosition; // Po��te�n� pozice plo�iny
    private Vector3 targetPosition; // C�lov� pozice plo�iny
    private int playersOnButton = 0; // Po�et hr��� na tla��tku
    private Coroutine moveCoroutine; // Odkaz na aktu�ln� pohybovou rutinu

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
            if (playersOnButton == 1) // Pokud je prvn� hr�� na tla��tku, pohni plo�inou
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
            if (playersOnButton == 0) // Plo�ina se vr�t� pouze kdy� tla��tko opust� posledn� hr��
            {
                StartCoroutine(ReturnWithDelay());
            }
        }
    }

    private void StartMoving(Vector3 destination)
    {
        if (moveCoroutine != null) StopCoroutine(moveCoroutine); // Zastav� p�edchoz� pohyb
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

        platform.position = destination; // Zajisti p�esn� zarovn�n� na c�lovou pozici
        moveCoroutine = null; // Pohyb je dokon�en
    }

    private IEnumerator ReturnWithDelay()
    {
        yield return new WaitForSeconds(stopTime); // Pauza p�ed n�vratem plo�iny
        StartMoving(initialPosition);
    }
}
