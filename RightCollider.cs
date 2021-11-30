using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RightCollider : MonoBehaviour
{
    [SerializeField] GunStick instance;

    void OnTouchDown(Vector3 point)
    {
        instance.OnTouchDown(point);
        Debug.Log("c");
    }

    void OnTouchUp(Vector3 point)
    {
        instance.OnTouchUp(point);
        Debug.Log("c");
    }

    void OnTouchStay(Vector3 point)
    {
        instance.OnTouchStay(point);
        Debug.Log("c");
    }

    void OnTouchExit(Vector3 point)
    {
        instance.OnTouchExit(point);
        Debug.Log("c");
    }
}
