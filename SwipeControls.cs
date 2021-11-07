using UnityEngine;

public class SwipeControls : MonoBehaviour
{
    [SerializeField] float speedModifier = 15;
    [SerializeField] float rangeModifier = 2;
    [SerializeField] float deadZone = 100;
    [SerializeField] bool debugWithArrowKeys = true;
    //[SerializeField] float MAX_TAP_TIME = 0.1f;
    //float tapTime = 0.0f;
    bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    bool isDraging = false;
    Vector2 startTouch, endTouch, swipeDelta;
    Vector3 desiredPosition;

    private void Update()
    {
        Tap = SwipeDown = SwipeUp = SwipeLeft = SwipeRight = false;

        #region Standalone Inputs
        if (Input.GetMouseButtonDown(0))
        {
            isDraging = true;
            startTouch = Input.mousePosition;
            //tapTime = Time.time;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            isDraging = false;
            endTouch = Input.mousePosition;

            if (startTouch == endTouch)
                Tap = true;

            //if (Time.time - tapTime < MAX_TAP_TIME)
            //    Tap = true;

            Reset();
        }
        #endregion

        #region Mobile Input
        if (Input.touches.Length > 0)
        {
            if (Input.touches[0].phase == TouchPhase.Began)
            {
                isDraging = true;
                startTouch = Input.touches[0].position;
                //tapTime = Time.time;
            }
            else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
            {
                isDraging = false;
                endTouch = Input.touches[0].position;

                if (startTouch == endTouch)
                    Tap = true;

                //if (Time.time - tapTime < MAX_TAP_TIME)
                //    Tap = true;

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
        if (swipeDelta.magnitude > deadZone)
        {
            //Which direction?
            float x = swipeDelta.x;
            float y = swipeDelta.y;

            if (Mathf.Abs(x) > Mathf.Abs(y))
            {
                //Left or Right
                if (x < 0)
                    SwipeLeft = true;
                else
                    SwipeRight = true;
            }
            else
            {
                //Up or Down
                if (y < 0)
                    SwipeDown = true;
                else
                    SwipeUp = true;
            }

            Reset();
        }

        if (debugWithArrowKeys)
        {
            Tap = Tap || Input.GetKeyDown(KeyCode.Space);
            SwipeLeft = SwipeLeft || Input.GetKeyDown(KeyCode.LeftArrow);
            SwipeRight = SwipeRight || Input.GetKeyDown(KeyCode.RightArrow);
            SwipeDown = SwipeDown || Input.GetKeyDown(KeyCode.DownArrow);
            SwipeUp = SwipeUp || Input.GetKeyDown(KeyCode.UpArrow);
        }

        if (Tap)
            desiredPosition = transform.position;
        if (SwipeLeft)
            desiredPosition = transform.position + Vector3.left * rangeModifier;
        if (SwipeRight)
            desiredPosition = transform.position + Vector3.right * rangeModifier;
        if (SwipeUp)
            desiredPosition = transform.position + Vector3.up * rangeModifier;
        if (SwipeDown)
            desiredPosition = transform.position + Vector3.down * rangeModifier;

        transform.position = Vector3.MoveTowards(transform.position, desiredPosition, speedModifier * Time.deltaTime);
    }

    private void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }

    private bool Tap { get { return tap; } set { tap = value; if (value) { Debug.Log("tap"); } } }
    private bool SwipeLeft { get { return swipeLeft; } set { swipeLeft = value; if (value) { Debug.Log("swipeLeft"); } } }
    private bool SwipeRight { get { return swipeRight; } set { swipeRight = value; if (value) { Debug.Log("swipeRight"); } } }
    private bool SwipeUp { get { return swipeUp; } set { swipeUp = value; if (value) { Debug.Log("swipeUp"); } } }
    private bool SwipeDown { get { return swipeDown; } set { swipeDown = value; if (value) { Debug.Log("swipeDown"); } } }
}
