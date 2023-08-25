using UnityEngine;

public class CamFollow : MonoBehaviour
{
    public Transform Player;
    public float smoothSpeed = 10f;

    public Vector3 fixPosition = new Vector3(0, 30, 15);
    public Vector3 fixRotation = new Vector3(-10, 0, 0);

    private void LateUpdate()
    {
        transform.position = Vector3.Lerp(transform.position, CalculatePosition(), smoothSpeed * Time.deltaTime);
        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(CalculateRotation()), smoothSpeed * Time.deltaTime);
    }

    private Vector3 CalculateRotation()
    {
        Vector3 direction = Player.position - transform.position;
        float xRotation = -Mathf.Atan2(direction.y, direction.z) * Mathf.Rad2Deg + fixRotation.x;
        Vector3 _rotation = new Vector3(xRotation, fixRotation.y, fixRotation.z);
        return _rotation;
    }

    private Vector3 CalculatePosition()
    {
        Vector3 targetPosition = Player.position - Vector3.forward * fixPosition.z + Vector3.up * fixPosition.y;
        targetPosition += Vector3.right * fixPosition.x;
        return targetPosition;
    }
}
