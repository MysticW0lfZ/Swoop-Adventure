using UnityEngine;

public class Hazard : MonoBehaviour
{
    public int damage = 1;

    void OnCollisionEnter2D(Collision2D collision)
    {
        TryDamage(collision.collider);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        TryDamage(other);
    }

    void TryDamage(Collider2D col)
    {
        PlayerHealth player = col.GetComponent<PlayerHealth>();
        if (player != null)
        {
            player.TakeDamage(damage);
        }
    }
}
