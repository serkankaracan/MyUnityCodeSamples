using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    [SerializeField] float speedModifier = 10;
    [SerializeField] float rangeModifier = 2;
    bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    bool isDraging = false;
    Vector2 startTouch, swipeDelta;
    Vector3 desiredPosition;

    private void Update()
    {
        tap = swipeDown = swipeUp = swipeLeft = swipeRight = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            tap = true;
            isDraging = true;
            startTouch = Input.mousePosition;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                tap = true;
                isDraging = true;
                startTouch = Input.touches[0].position;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                Reset();
            }
        }
        #endregion

        //Calculate the distance
        swipeDelta = Vector2.zero;

        if (isDraging)
        {
            if (Input.touches.Length < 0)
                swipeDelta = Input.touches[0].position - startTouch;
            else if (Input.GetMouseButton(0))
                swipeDelta = (Vector2)Input.mousePosition - startTouch;
        }

        //Did we cross the distance?
        if (swipeDelta.magnitude > 100)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                    swipeLeft = true;
                else
                    swipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0)
                    swipeDown = true;
                else
                    swipeUp = true;
            }

            Reset();
        }

        if (swipeLeft)
            desiredPosition = transform.position + Vector3.left * rangeModifier;
        if (swipeRight)
            desiredPosition = transform.position + Vector3.right * rangeModifier;
        if (swipeUp)
            desiredPosition = transform.position + Vector3.up * rangeModifier;
        if (swipeDown)
            desiredPosition = transform.position + Vector3.down * rangeModifier;

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, speedModifier * Time.deltaTime);
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
