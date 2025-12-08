using UnityEngine;

public class MeleeEnemy : MonoBehaviour
{
    Rigidbody2D rb;
    public bool isAirborne = false;
    public float gravityScaleDuringLaunch = 2f;

    [Header("Attack Parameters")]
    [SerializeField] private float attackCooldown;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [Header("ColliderParameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    //references
    private Animator anim;
    private Health playerHealth;

    private EnemyPatrol enemyPatrol;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    public void GetLaunched(float force)
    {
        isAirborne = true;

        // Reset vertical velocity so the launch is consistent
        rb.linearVelocity = new Vector2(rb.linearVelocity.x, 0);

        // Apply upward force
        rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

        // Optional: increase gravity for satisfying fall
        rb.gravityScale = gravityScaleDuringLaunch;

        // Trigger launch animation
        GetComponent<Animator>().SetTrigger("Launched");
    }

    private void Awake()
    {
        anim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }
    private void Update()
    {
        // Detect when the enemy lands
        if (isAirborne && rb.linearVelocity.y < 0.1f && IsGrounded())
        {
            isAirborne = false;
            rb.gravityScale = 1f;
            GetComponent<Animator>().SetTrigger("Land");
        }
        cooldownTimer += Time.deltaTime;

        //Attack only when player in sight?
        if (PlayerInSight())
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("meleeAttack");
            }
        }


        if (enemyPatrol != null)
            enemyPatrol.enabled = !PlayerInSight();
    }
    bool IsGrounded()
    {
        return Physics2D.Raycast(transform.position, Vector2.down, 1f, LayerMask.GetMask("Ground"));
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit =
            Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
             0, Vector2.left, 0, playerLayer);

        if (hit.collider != null)
            playerHealth = hit.transform.GetComponent<Health>();


        return hit.collider != null;
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z));
    }

    private void DamagePlayer()
    {
        if (!PlayerInSight()) return;

        // Get player controller reference
        PlayerController player = playerHealth.GetComponent<PlayerController>();

        // If player is parrying, enemy attack is negated
        if (player != null && player.IsParrying())
        {
            player.SpawnParrySparks();

            return; // Do NOT damage the player
        }

        // If player wasn't parrying → deal damage
        playerHealth.TakeDamage(damage);
    }

}