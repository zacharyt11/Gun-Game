using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] Transform player;
    [SerializeField] Vector3 offset;
    [SerializeField] MoveStick moveStick;
    [SerializeField] GunStick gunStick;
    Vector3 startingPos;

    private void Start()
    {
        startingPos = transform.position;
    }

    private void Update()
    {
        if (transform.position != startingPos)
        {
            Vector3 direction = startingPos - transform.position;
            //moveStick.gameObject.transform.parent.position += direction;
            //gunStick.gameObject.transform.parent.position += direction;
            //moveStick.ogPos += direction;
            //gunStick.ogPos += direction;
            startingPos = transform.position;
        }
        transform.position = player.position + offset;
    }
}