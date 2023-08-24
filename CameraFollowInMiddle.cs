using UnityEngine;

public class CameraFollowInMiddle : MonoBehaviour
{
    public Transform Player;
    public float smoothSpeed = 10f;
    public float distanceFromPlayer = 10f;
    public float yHeightOffset = 5f;

    private void LateUpdate()
    {
        Vector3 targetPosition = Player.position - Vector3.forward * distanceFromPlayer + Vector3.up * yHeightOffset;
        Quaternion targetRotation = Quaternion.Euler(CalculateXRotation(), 0, 0);

        transform.position = Vector3.Lerp(transform.position, targetPosition, smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, smoothSpeed * Time.deltaTime);
    }

    private float CalculateXRotation()
    {
        Vector3 direction = Player.position - transform.position;
        float xRotation = -Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg;
        return xRotation;
    }
}
