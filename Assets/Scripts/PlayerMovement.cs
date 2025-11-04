using UnityEngine;

public class SideScrollPlayer : MonoBehaviour
{
    public float moveSpeed = 10.0f;
    public float jumpForce = 500.0f;

    Rigidbody2D rb;

    public bool isGrounded = false;
    public bool shouldJump = false;

    Animator animator;
    SpriteRenderer spriteRenderer;

    // ? Dash (only needed variables)
    public bool shouldDash = false;
    public float dashForce = 20f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Translate(new Vector3(horizontalInput, 0, 0) * moveSpeed * Time.deltaTime);

        if (horizontalInput > 0)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = false;
        }
        else if (horizontalInput < 0)
        {
            animator.SetBool("isRunning", true);
            spriteRenderer.flipX = true;
        }
        else
        {
            animator.SetBool("isRunning", false);
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            shouldJump = true;
        }

        // ? Dash input
        if (Input.GetKeyDown(KeyCode.LeftShift) )
        {
            shouldDash = true;
        }
    }

    void FixedUpdate()
    {
        if (shouldJump == true)
        {
            shouldJump = false;
            rb.AddForce(Vector2.up * jumpForce);
            animator.SetBool("isJumping", true);
        }

        // ? Perform Dash
        if (shouldDash == true)
        {
            shouldDash = false;
            animator.SetTrigger("Dash");
            float direction = spriteRenderer.flipX ? -1f : 1f;
            rb.linearVelocity = new Vector2(direction * dashForce, rb.linearVelocity.y);
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        isGrounded = true;
        animator.SetBool("isJumping", false);
    }

    void OnTriggerExit2D(Collider2D other)
    {
        isGrounded = false;
    }
}