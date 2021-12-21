using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveStick : MonoBehaviour
{
    public Vector3 ogPos;
    Vector3 offsetPos;
    public bool joystickBeingUsed;
    [SerializeField] GameObject moveBackground;
    [SerializeField] GameObject visualMoveStick;
    [SerializeField] GameObject visualGunStick;
    [SerializeField] GameObject gunStick;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTouchDown(Vector3 point)
    {
        offsetPos = point - Camera.main.transform.position;
        offsetPos = new Vector3(offsetPos.x, offsetPos.y, 10f);
        moveBackground.transform.position = new Vector3(point.x, point.y, 0f);
        visualMoveStick.transform.position = new Vector3(point.x, point.y, 0f);
        moveBackground.SetActive(true);
        visualMoveStick.SetActive(true);
        joystickBeingUsed = true;
    }

    public void OnTouchUp(Vector3 point)
    {
        //moveBackground.SetActive(false);
        //visualMoveStick.SetActive(false);
        transform.position = Camera.main.transform.position + offsetPos;
        if (!gunStick.GetComponent<GunStick>().joystickBeingUsed)
        {
            gunStick.transform.position = Camera.main.transform.position + new Vector3(-offsetPos.x, offsetPos.y, 10f);
            visualGunStick.transform.position = Camera.main.transform.position + new Vector3(-offsetPos.x, offsetPos.y, 10f);
        }
        ogPos = transform.position;
        Debug.Log("touchUp");
        visualMoveStick.transform.position = ogPos;
        joystickBeingUsed = false;
    }

    public void OnTouchStay(Vector3 point)
    {
        Vector2 playerOffset = point - ogPos;
        Vector2 direction = Vector2.ClampMagnitude(playerOffset, 1f);
        Player.instance.MovePlayer(direction);
        GetComponent<SphereCollider>().transform.position = new Vector3(point.x, point.y, 0);
        Vector3 offset = (point - ogPos);
        Vector3 direction2 = Vector2.ClampMagnitude(offset, 1f);
        visualMoveStick.transform.position = Player.instance.transform.position + (new Vector3(offsetPos.x, offsetPos.y, 10f) + direction2);
        if (!gunStick.GetComponent<GunStick>().joystickBeingUsed)
        {
            gunStick.transform.position = Camera.main.transform.position + new Vector3(-offsetPos.x, offsetPos.y, 0f);
            visualGunStick.transform.position = Camera.main.transform.position + new Vector3(-offsetPos.x, offsetPos.y, 10f);
        }
        ogPos = player.transform.position + offsetPos;
        moveBackground.SetActive(true);
        visualMoveStick.SetActive(true);
        joystickBeingUsed = true;
    }

    public void OnTouchExit(Vector3 point)
    {
        //moveBackground.SetActive(false);
        //visualMoveStick.SetActive(false);
        transform.position = Camera.main.transform.position + offsetPos;
        if (!gunStick.GetComponent<GunStick>().joystickBeingUsed)
        {
            gunStick.transform.position = Camera.main.transform.position + new Vector3(-offsetPos.x, offsetPos.y, 10f);
            visualGunStick.transform.position = Camera.main.transform.position + new Vector3(-offsetPos.x, offsetPos.y, 10f);
        }
        ogPos = transform.position;
        visualMoveStick.transform.position = ogPos;
        Debug.Log("Exit");
        joystickBeingUsed = false;
    }
}
