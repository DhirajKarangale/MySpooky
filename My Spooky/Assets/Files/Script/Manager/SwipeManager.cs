using UnityEngine;

public class SwipeManager : MonoBehaviour
{
    public static bool isTap, isLeft, isRight, isDown;

    private Touch touch;
    private Vector3 stTouchPos;
    private Vector3 enTouchPos;


    private void Update()
    {
        isTap = isLeft = isRight = isDown = false;

        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            switch (touch.phase)
            {
                case TouchPhase.Began:
                    stTouchPos = touch.position;
                    break;

                case TouchPhase.Ended:
                    enTouchPos = touch.position;

                    if (stTouchPos == enTouchPos)
                    {
                        isTap = true;
                        isLeft = isRight = isDown = false;
                    }
                    else
                    {
                        isTap = false;

                        float x = enTouchPos.x - stTouchPos.x;
                        float y = enTouchPos.y - stTouchPos.y;

                        if (Mathf.Abs(x) > Mathf.Abs(y))
                        {
                            if (x < 0) isLeft = true;
                            else isRight = true;
                        }
                        else
                        {
                            if (y < 0) isDown = true;
                        }
                    }

                    break;
            }
        }
    }
}
