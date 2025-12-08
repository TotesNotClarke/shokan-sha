using UnityEngine;

public class AirAttack : MonoBehaviour
{
    public Animator anim;
    public bool isLaunching;
    public float launchForce = 12f;
    public LayerMask enemyLayers;
    public Transform attackPoint;
    public float attackRange = 0.5f;
    void LaunchAttack()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            anim.SetTrigger("airLaunch");
        }
        // Detect enemies in range
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach (Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<MeleeEnemy>().GetLaunched(launchForce);
        }
    }
}
