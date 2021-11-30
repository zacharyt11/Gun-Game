using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Turret : MonoBehaviour
{
    Transform plr;
    [SerializeField] GameObject projectile;
    int layerMask;
    bool readyToShoot;
    bool enemySpotted;
    public float health, armor, shootCooldown, armorPiercing, damage, spottedMultiplier;
    bool scaleMultiplierApplied;
    [SerializeField] Color spottedSightAreaColor;

    private void Start()
    {
        plr = GameObject.FindGameObjectWithTag("Player").transform;
        layerMask =~ LayerMask.GetMask("Enemy");
    }

    private void FixedUpdate()
    {
        if ((EnemyInSightArea()))
        {
            if (readyToShoot)
            {
                Instantiate(projectile, transform);
                readyToShoot = false;
            }
        }
    }

    void ResetShootCooldown()
    {
        readyToShoot = true;
    }

    void ApplyDamageToSelf(float enemyBulletDamage, float enemyArmorPiercing)
    {
        health -= enemyBulletDamage / (armor - enemyArmorPiercing) < 0 ? 0f : enemyBulletDamage / (armor - enemyArmorPiercing);
    }

    bool EnemyInSightArea()
    {
        if (transform.GetChild(0).GetComponent<CircleCollider2D>().bounds.Contains(plr.transform.position))
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position, plr.position - transform.position, 100f, layerMask);
            if (hit.point != null && hit.collider.gameObject.CompareTag("Player"))
            {
                transform.GetChild(0).localScale = transform.GetChild(0).localScale * (scaleMultiplierApplied ?  1 : spottedMultiplier);
                transform.GetChild(0).GetComponent<SpriteRenderer>().color = spottedSightAreaColor;
                scaleMultiplierApplied = true;
                return true;
            }
        }
        return false;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            ApplyDamageToSelf(collision.gameObject.transform.parent.GetComponent<Player>().damage, collision.gameObject.transform.parent.GetComponent<Player>().armorPiercing);
        }
    }
}
