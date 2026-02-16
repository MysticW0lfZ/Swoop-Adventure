
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public float patrolDistance = 2f;
    public int contactDamage = 1;

    private Vector3 startPos;
    private int direction = 1;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        // Simple left right patrol
        transform.position += Vector3.right * speed * direction * Time.deltaTime;

        if (Vector3.Distance(transform.position, startPos) >= patrolDistance)
        {
            direction *= -1;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerHealth health = collision.gameObject.GetComponent<PlayerHealth>();

            if (health != null)
            {
                health.TakeDamage(contactDamage);
            }
        }
    }
}
