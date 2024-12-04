using UnityEngine;

public class CameraTarget : MonoBehaviour
{
    public Transform player1;
    public Transform player2;

    void Update()
    {
        Vector3 midpoint = (player1.position + player2.position) / 2f;
        transform.position = new Vector3(midpoint.x, midpoint.y, transform.position.z);
    }
}
