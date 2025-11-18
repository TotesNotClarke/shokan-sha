using UnityEngine;

public class PlayerDamage : MonoBehaviour
{

    public int maxHealth = 8;
    public int currentHealth;

    public HealthBar healthBar;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentHealth = maxHealth;
        healthBar.SetMaxHealth(maxHealth);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            TakeDamage(12);
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            TakeDamage(25);
        }
    }
    void TakeDamage(int damage)
    {
        currentHealth -= damage;

        healthBar.SetHealth(currentHealth);
    }

}