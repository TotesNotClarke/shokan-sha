using UnityEngine;

public class ChasingMeleeEnemy : MonoBehaviour
{
    [Header("Detection")]
    [SerializeField] private float detectionRange = 4f;
    [SerializeField] private float detectionColliderDistance = 0.5f;
    [SerializeField] private BoxCollider2D boxCollider;
    [SerializeField] private LayerMask playerLayer;

    [Header("Movement")]
    [SerializeField] private float moveSpeed = 2f;

    [Header("Attack")]
    [SerializeField] private float attackCooldown = 1.2f;
    [SerializeField] private float attackRange = 1f;
    [SerializeField] private int damage = 20;

    private float cooldownTimer = Mathf.Infinity;

    private Animator anim;
    private Transform player;

    private void Awake()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        cooldownTimer += Time.deltaTime;

        bool playerInSight = PlayerInSight();
        anim.SetBool("inSight", playerInSight);

        if (!playerInSight)
        {
            // Player not in range → idle
            anim.SetBool("isWalking", false);
            return;
        }

        // Player detected → walk towards player
        MoveTowardPlayer();

        // If close enough → attack
        if (Vector2.Distance(transform.position, player.position) <= attackRange)
        {
            if (cooldownTimer >= attackCooldown)
            {
                cooldownTimer = 0;
                anim.SetTrigger("attack");
            }
        }
    }

    private void MoveTowardPlayer()
    {
        anim.SetBool("isWalking", true);

        // Determine direction
        float direction = Mathf.Sign(player.position.x - transform.position.x);

        // Flip enemy
        if (direction != 0)
            transform.localScale = new Vector3(direction, 1, 1);

        // Move
        transform.position += new Vector3(direction * moveSpeed * Time.deltaTime, 0, 0);
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(
            boxCollider.bounds.center + transform.right * detectionRange * transform.localScale.x * detectionColliderDistance,
            new Vector3(boxCollider.bounds.size.x * detectionRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z),
            0,
            Vector2.left * transform.localScale.x,
            0,
            playerLayer
        );

        if (hit.collider != null) player = hit.transform;
        return hit.collider != null;
    }

    private void OnDrawGizmos()
    {
        if (boxCollider == null) return;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(
            boxCollider.bounds.center + transform.right * detectionRange * transform.localScale.x * detectionColliderDistance,
            new Vector3(boxCollider.bounds.size.x * detectionRange, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
        );
    }
}
