using UnityEngine;

public class EnemyProjectile : MonoBehaviour
{
    Transform player;
    [SerializeField] float speed;
    [SerializeField] float cappedVelocity;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        Vector2 direction = (player.position - transform.parent.position) * 1000f;
        direction = Vector2.ClampMagnitude(direction, 1f);
        rb.position += direction * (speed * Time.deltaTime);
    }
}
