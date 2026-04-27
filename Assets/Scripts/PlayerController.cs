using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 7f;
    public float jumpForce = 12f;

    [Header("Audio")]
    public AudioSource jumpSound;

    public Animator animator; // Animation reference

    private Rigidbody2D rb;
    private bool isGrounded;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Movement input
        float input = Input.GetAxisRaw("Horizontal");

        // Move character
        Vector2 velocity = rb.linearVelocity;
        velocity.x = input * moveSpeed;
        rb.linearVelocity = velocity;

        // ANIMATION
        animator.SetFloat("Speed", Mathf.Abs(input));
        animator.SetBool("isGrounded", isGrounded);

        // FLIP CHARACTER
        if (input > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else if (input < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        // JUMP
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpForce);

            if (jumpSound != null)
                jumpSound.Play();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            return;

        if (collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            if (normal.y > 0.5f)
                isGrounded = true;
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            return;

        if (collision.contacts.Length > 0)
        {
            Vector2 normal = collision.contacts[0].normal;
            if (normal.y > 0.5f)
                isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
            return;

        isGrounded = false;
    }
}