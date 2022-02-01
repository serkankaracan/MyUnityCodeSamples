using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform Player;
    public float smoothSpeed = .1f;
    Vector3 offset;

    void Start()
    {
        offset = transform.position - Player.position;
    }


    void LateUpdate()
    {
        Vector3 desiredPosition = Player.position + offset;
        Vector3 smoothedPosition;

        smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);

        transform.position = smoothedPosition;
    }
}
