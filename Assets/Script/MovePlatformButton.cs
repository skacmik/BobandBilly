using UnityEngine;

public class MovePlatformButton : MonoBehaviour
{
    public Transform platform; // Plo�ina, kter� se bude pohybovat
    public float moveSpeed = 2f; // Rychlost pohybu plo�iny
    public float moveDistance = 5f; // Jak vysoko se plo�ina pohne

    private Vector3 initialPosition; // Po��te�n� pozice plo�iny
    private Vector3 targetPosition; // Kone�n� pozice plo�iny
    private bool isPlayerOnButton = false; // Sleduje, zda je hr�� na tla��tku
    private Coroutine moveCoroutine; // Odkaz na aktu�ln� pohybovou rutinu

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
        // Pokud na tla��tko vstoup� hr��
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            isPlayerOnButton = true;
            if (moveCoroutine != null) StopCoroutine(moveCoroutine); // Zastav aktu�ln� pohyb
            moveCoroutine = StartCoroutine(MovePlatform(targetPosition)); // Plo�ina jede nahoru
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // Pokud hr�� opust� tla��tko
        if (collision.CompareTag("PlayerBig") || collision.CompareTag("PlayerSmall"))
        {
            isPlayerOnButton = false;
            if (moveCoroutine != null) StopCoroutine(moveCoroutine); // Zastav aktu�ln� pohyb
            moveCoroutine = StartCoroutine(MovePlatform(initialPosition)); // Plo�ina jede dol�
        }
    }

    private System.Collections.IEnumerator MovePlatform(Vector3 destination)
    {
        while (Vector3.Distance(platform.position, destination) > 0.01f)
        {
            platform.position = Vector3.MoveTowards(platform.position, destination, moveSpeed * Time.deltaTime);
            yield return null; // Po�kej na dal�� sn�mek
        }

        platform.position = destination; // Zajisti p�esn� zarovn�n� na c�lovou pozici
        moveCoroutine = null; // Pohyb je dokon�en
    }
}
