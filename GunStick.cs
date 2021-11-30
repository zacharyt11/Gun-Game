using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunStick : MonoBehaviour
{
    public Vector3 ogPos;
    Vector3 offsetPos;
    public bool joystickBeingUsed;
    SphereCollider colliderJ;
    public GameObject visualStick;
    [SerializeField] GameObject gunBackground;
    [SerializeField] GameObject visualGunStick;
    GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void OnTouchDown(Vector3 point)
    {
        offsetPos = point - Camera.main.transform.position;
        gunBackground.transform.position = point;
        visualGunStick.transform.position = point;
        gunBackground.SetActive(true);
        visualGunStick.SetActive(true);
    }

    public void OnTouchUp(Vector3 point)
    {
        //gunBackground.SetActive(false);
        //visualGunStick.SetActive(false);
        transform.position = Camera.main.transform.position + offsetPos;
        ogPos = transform.position;
        Debug.Log("touchUp");
        visualStick.transform.position = ogPos;
    }

    public void OnTouchStay(Vector3 point)
    {
        Vector2 projOffset = (point - ogPos) * 1000f;
        Vector2 direction = Vector2.ClampMagnitude(projOffset, 1f);
        Player.instance.ShootProjectile(direction, point);
        joystickBeingUsed = true;
        GetComponent<SphereCollider>().transform.position = new Vector3(point.x, point.y, 0);
        Vector3 offset = (point - ogPos);
        Vector3 direction2 = Vector2.ClampMagnitude(offset, 1f);
        visualStick.transform.position = Player.instance.transform.position + (new Vector3(offsetPos.x, offsetPos.y, 0f) + direction2);
        ogPos = player.transform.position + offsetPos;
        gunBackground.SetActive(true);
        visualGunStick.SetActive(true);
    }

    public void OnTouchExit(Vector3 point)
    {
        //gunBackground.SetActive(false);
        //visualGunStick.SetActive(false);
        transform.position = Camera.main.transform.position + offsetPos;
        ogPos = transform.position;
        visualGunStick.transform.position = ogPos;
    }
}
