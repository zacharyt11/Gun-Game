using UnityEngine;

public class Projectile : MonoBehaviour
{
    public Vector2 direction;
    [SerializeField] float startingSpeed;
    [SerializeField] float cappedVelocity;
    Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        rb.position += direction * (startingSpeed * Time.deltaTime);
    }
}
