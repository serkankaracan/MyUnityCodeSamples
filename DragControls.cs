using UnityEngine;

public class DragControls : MonoBehaviour
{
    [SerializeField] float speedModifier = .01f;
    Vector2 prevMousePos = Vector2.zero;
    Vector2 currentMousePos = Vector2.zero;
    Vector2 deltaMousePos = Vector2.zero;

    void Update()
    {
        #region MobileInput
        if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
        {
            transform.position = new Vector3(transform.position.x + Input.GetTouch(0).deltaPosition.x * speedModifier,
                transform.position.y + Input.GetTouch(0).deltaPosition.y * speedModifier,
                transform.position.z);
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

            transform.position = new Vector3(transform.position.x + deltaMousePos.x * speedModifier,
                transform.position.y + deltaMousePos.y * speedModifier,
                transform.position.z);

            prevMousePos = Input.mousePosition;
        }
        #endregion
    }
}
