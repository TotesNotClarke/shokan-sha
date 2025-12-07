using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Animator myAnim;

    public bool isAttacking = false;
    public bool isParrying = false; // Controlled by animation events

    public static PlayerController instance;

    public GameObject parrySparksPrefab;
    public Transform parryEffectPoint;

    public Transform attackPoint;
    public float attackRange = 0.5f;
    public LayerMask enemyLayers;
    public void EndAttack() => isAttacking = false;
    public void SpawnParrySparks()
    {
        if (parrySparksPrefab != null && parryEffectPoint != null)
        {
            Instantiate(parrySparksPrefab, parryEffectPoint.position, Quaternion.identity);
        }
    }

    private void Awake()
    {
        instance = this;
    }

    void Start()
    {
        myAnim = GetComponent<Animator>();
    }

    void Update()
    {
        Attack();
        Parry();
    }

    // ---------------- ATTACK ---------------- //
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && !isAttacking)
        {
            isAttacking = true;
            myAnim.SetTrigger("doAttack"); // <-- Make sure your attack animation uses this
            if (!SideScrollPlayer.instance.isGrounded)
                myAnim.SetTrigger("AirAttack");
            else
                myAnim.SetTrigger("Combo 1");
        }
    }

    // Called by animation event when attack starts
    public void EnableAttack()
    {
        isAttacking = true;
    }

    // Called by animation event when attack ends
    public void DisableAttack()
    {
        isAttacking = false;
    }

    private void OnTriggerEnter2D(Collider2D enemy)
    {
        if (isAttacking)
        {
            Health hp = enemy.GetComponent<Health>();
            if (hp != null)
            {
                hp.TakeDamage(1);
            }
        }
    }

    // ---------------- PARRY ---------------- //
    void Parry()
    {
        if (Input.GetKeyDown(KeyCode.Mouse1) && !isParrying)
        {
            myAnim.SetTrigger("doParry");
            // DO NOT set isParrying here — the animation will do that
        }
    }

    // Called by animation event at start of parry frames
    public void EnableParry()
    {
        isParrying = true;
        // Debug.Log("Parry Active");
    }

    // Called by animation event at end of parry frames
    public void DisableParry()
    {
        isParrying = false;
        // Debug.Log("Parry Ended");
    }

    public bool IsParrying()
    {
        return isParrying;
    }


}
