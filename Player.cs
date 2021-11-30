using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [HideInInspector] public float armor, armorPiercing, health, damage, shootCooldown;

    [SerializeField] float speed = 2f;
    public static Player instance;
    [SerializeField] GameObject projectile;
    [SerializeField] float cooldownTimer;
    [SerializeField] Transform bulletParent;
    bool readyToShoot = true;
    Rigidbody2D rb;
 
    private void Start()
    {
        instance = this;
        rb = GetComponent<Rigidbody2D>();
    }

    public void MovePlayer(Vector2 translation)
    {
        rb.position += translation * (speed * Time.deltaTime);
    }

    public void ShootProjectile(Vector2 dir, Vector3 point)
    {
        if (readyToShoot)
        {
            Transform clonedProject = Instantiate(projectile, transform.position, transform.rotation, bulletParent).transform;
            clonedProject.transform.GetChild(0).gameObject.GetComponent<Projectile>().direction = dir;
            Vector3 pos = Camera.main.WorldToScreenPoint(clonedProject.transform.position);
            Vector3 dir1 = point - pos;
            float angle = Mathf.Atan2(-dir.x, dir.y) * Mathf.Rad2Deg;
            clonedProject.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            clonedProject.transform.eulerAngles = new Vector3(clonedProject.transform.eulerAngles.x, clonedProject.transform.eulerAngles.y, clonedProject.transform.eulerAngles.z);
            readyToShoot = false;
            Invoke("ResetCD", cooldownTimer);
        }
    }

    void ResetCD()
    {
        readyToShoot = true;
    }
}
