using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Joystick : MonoBehaviour
{
    bool isDragging;
    Vector2 ogPos;
    float stickRadius;
    [SerializeField] CircleCollider2D background;
    [SerializeField] bool gunJoystick;
    int moveTouchIndex = -1;
    int gunTouchIndex = -1;
    Touch moveStickTouch;
    Touch gunStickTouch;

    void Start()
    {
        ogPos = transform.position;
        stickRadius = background.transform.lossyScale.x / 2.1875f;
    }

    private void Update()
    {
        if (!gunJoystick)
        {
            Player.instance.MovePlayer(MoveStick());
        }
        else
        {
            GunStick();
        }
    }

    Vector2 MoveStick()
    {
        if (Input.touchCount > 0)
        {
            //first assignment of touch input
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (moveTouchIndex == -1 && Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    moveTouchIndex = Input.GetTouch(i).fingerId;
                }
            }
            if (moveTouchIndex != -1)
            {
                foreach (Touch t in Input.touches)
                {
                    if (t.fingerId == moveTouchIndex)
                    {
                        moveStickTouch = t;
                    }
                }
            }
            Touch touch = moveStickTouch;
            Vector2 touchPos = Camera.main.ScreenToWorldPoint((Vector3)touch.position + Vector3.forward * 10f);
            float distance = Vector2.Distance(ogPos, touchPos);
            if (distance <= stickRadius && background.bounds.Contains(touchPos))
            {
                transform.position = touchPos;
                isDragging = true;
            }
            else if (distance > stickRadius)
            {
                Vector2 direction = (touchPos - ogPos).normalized;
                transform.position = ogPos + (direction * stickRadius);
            }
        }
        Vector2 pos = transform.position;
        return new Vector2((pos.x - ogPos.x) / stickRadius, (pos.y - ogPos.y) / stickRadius);
    }

    Vector2 GunStick()
    {
        if (Input.touchCount > 0)
        {
            //first assignment of touch input
            for (int i = 0; i < Input.touchCount; i++)
            {
                if (gunTouchIndex == -1 && Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    gunTouchIndex = Input.GetTouch(i).fingerId;
                    gunStickTouch = Input.GetTouch(i);
                }
            }         
            var tch = new Touch();
            if (gunTouchIndex != -1)
            {
                foreach (Touch t in Input.touches)
                {
                    if (t.fingerId == gunTouchIndex)
                    {
                        tch = t;
                    }
                }
            }
            Touch touch = tch;
            Vector2 touchPos = Camera.main.ScreenToWorldPoint((Vector3)touch.position + Vector3.forward * 10f);
            float distance = Vector2.Distance(ogPos, touchPos);
            if (distance <= stickRadius && background.bounds.Contains(touchPos))
            {
                transform.position = touchPos;
                isDragging = true;
            }
            else if (distance > stickRadius && isDragging)
            {
                Vector2 direction = (touchPos - ogPos).normalized;
                transform.position = ogPos + (direction * stickRadius);
            }
            if (touch.phase == TouchPhase.Ended)
            {
                isDragging = false;
                transform.position = ogPos;
                gunTouchIndex = -1;
            }
        }
        Vector2 pos = transform.position;
        return new Vector2((pos.x - ogPos.x) / stickRadius, (pos.y - ogPos.y) / stickRadius);
    }
}