using UnityEngine;

// bu script World Space Canvas'ın içinde olmalı.
public class Billboard : MonoBehaviour
{
    Transform cam;

    private void Start()
    {
        cam = Camera.main.transform;
    }

    void LateUpdate()
    {
        //World Space Canvas'ın ön yüzüne gösterecek şekilde bak.
        transform.LookAt(transform.position + cam.forward);
    }
}
