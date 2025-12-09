using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Rendering;

public class BossWeapon : MonoBehaviour
{

    public int attackDamage = 1;

    public Vector3 attackOffset;
    public float attackRange = 1f;
    public LayerMask attackMask;

    public void Attack()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;


        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);

        if (colInfo != null)
        {
            //damage the player
            PlayerController player = colInfo.GetComponent<PlayerController>();
            if (player != null && player.IsParrying())
            {
                player.SpawnParrySparks();

                return; // Do NOT damage the player
            }
            colInfo.GetComponent<Health>().TakeDamage(attackDamage);
        }

    }

}

