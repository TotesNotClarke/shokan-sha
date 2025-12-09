using UnityEngine;
using System.Collections;


public class Health : MonoBehaviour
{
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    private Animator anim;
    private bool dead;
    private IEnumerator RespawnDelay()
    {
        yield return new WaitForSeconds(7f); // wait for death animation
        RespawnManager.instance.RespawnPlayer();
    }


    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
    }

    public void TakeDamage(float _damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
        }
        else
        {
            if (!dead)
            {
                anim.SetTrigger("die");
                //StartCoroutine(RespawnDelay());


                //Player
                if (GetComponent<SideScrollPlayer>() != null)
                {
                    GetComponent<SideScrollPlayer>().enabled = false;
                    StartCoroutine(RespawnDelay());
                }

                //Enemy
                if (GetComponentInParent<EnemyPatrol>() != null)
                    GetComponentInParent<EnemyPatrol>().enabled = false;

                if (GetComponent<MeleeEnemy>() != null)
                    GetComponent<MeleeEnemy>().enabled = false;
               
                dead = true;
            }
        }
    }
    public void Respawn(Vector3 respawnPosition)
    {
        // Restore health
        currentHealth = startingHealth;
        dead = false;

        // Re-enable components disabled on death
        if (GetComponent<SideScrollPlayer>() != null)
            GetComponent<SideScrollPlayer>().enabled = true;

        if (GetComponentInParent<EnemyPatrol>() != null)
            GetComponentInParent<EnemyPatrol>().enabled = true;

        if (GetComponent<MeleeEnemy>() != null)
            GetComponent<MeleeEnemy>().enabled = true;

        // Move player to respawn point
        transform.position = respawnPosition;
        anim.ResetTrigger("die");
        anim.SetTrigger("respawn");


        // Play revive animation (optional)
        if (anim != null)
            anim.Play("Idle"); // or “respawn” if you have one

    }

}