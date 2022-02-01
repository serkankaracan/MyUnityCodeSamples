using UnityEngine;

public class DragControls : MonoBehaviour
{
    [SerializeField] float speedModifier = .01f;
    [SerializeField] MoveAxis moveAxis = MoveAxis.X;
    Vector2 prevMousePos = Vector2.zero;
    Vector2 currentMousePos = Vector2.zero;
    Vector2 deltaMousePos = Vector2.zero;

    void Update()
    {
        #region MobileInput
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            MoveGameObject(Input.GetTouch(0).deltaPosition);
        }
        #endregion

        #region StandaloneInput
        if (Input.GetMouseButtonDown(0))
        {
            prevMousePos = Input.mousePosition;
        }
        else if (Input.GetMouseButton(0))
        {
            currentMousePos = Input.mousePosition;
            deltaMousePos = currentMousePos - prevMousePos;

            MoveGameObject(deltaMousePos);

            prevMousePos = Input.mousePosition;
        }
        #endregion
    }

    void MoveGameObject(Vector2 delta)
    {
        float xValue = transform.position.x + delta.x * speedModifier;
        float yValue = transform.position.y + delta.y * speedModifier;
        float zValue = transform.position.z + delta.y * speedModifier;

        switch (moveAxis)
        {
            case MoveAxis.X:
                transform.position = new Vector3(xValue, transform.position.y, transform.position.z);
                break;
            case MoveAxis.XY:
                transform.position = new Vector3(xValue, yValue, transform.position.z);
                break;
            case MoveAxis.XZ:
                transform.position = new Vector3(xValue, transform.position.y, zValue);
                break;
            default:
                break;
        }
    }

    enum MoveAxis
    {
        X,
        XY,
        XZ
    }
}
