using UnityEngine;

public class BackgroundScaler : MonoBehaviour
{
    private void Start()
    {
        SpriteRenderer sr = GetComponent<SpriteRenderer>();
        if (sr == null)
        {
            Debug.LogError("❌ BackgroundScaler: Chybí SpriteRenderer!");
            return;
        }

        Transform cam = Camera.main.transform;
        float camHeight = Camera.main.orthographicSize * 2f;
        float camWidth = camHeight * Camera.main.aspect;

        Vector2 spriteSize = sr.sprite.bounds.size;
        transform.localScale = new Vector3(camWidth / spriteSize.x, camHeight / spriteSize.y, 1);
        transform.position = new Vector3(cam.position.x, cam.position.y, transform.position.z);

        Debug.Log("✅ Pozadí správně upraveno!");
    }
}
