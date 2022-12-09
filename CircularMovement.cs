using UnityEngine;

public class CircularMovement : MonoBehaviour
{
    public Transform target;
    public float frequency = 1;
    public float xDistance = 20;
    public float zDistance = 30;
    public float radius = 10;
    Vector3 initRot;
    float moveSpeed = 0;
    float yPos;

    public bool isCircular;

    void Start()
    {
        initRot = transform.eulerAngles;
        yPos = transform.position.y;
        radius = Vector3.Distance(transform.position, target.position);
    }


    void Update()
    {
        moveSpeed += Time.deltaTime * frequency;

        float x, z;

        if (isCircular)
        {
            x = Mathf.Cos(moveSpeed) * radius;
            z = Mathf.Sin(moveSpeed) * radius;
        }
        else
        {
            x = Mathf.Cos(moveSpeed) * xDistance;
            z = Mathf.Sin(moveSpeed) * zDistance;
        }

        transform.position = new Vector3(target.position.x + x, yPos, target.position.z + z);
        //transform.LookAt(target);

        Quaternion _lookRotation = Quaternion.LookRotation((target.position - transform.position).normalized);
        //transform.rotation = Quaternion.Slerp(transform.rotation, _lookRotation, Time.deltaTime * 15);
        transform.rotation = Quaternion.Euler(initRot.x, _lookRotation.eulerAngles.y, initRot.z);
    }

}
