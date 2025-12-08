using UnityEngine;

public class Launchable : MonoBehaviour
{
    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void Launch(Vector2 force)
    {
        if (rb == null)
        {
            Debug.LogWarning("Launch() called but no Rigidbody2D found on " + gameObject.name);
            return;
        }

        // Reset velocity for consistent launch
        rb.linearVelocity = Vector2.zero;

        // Apply launch force
        rb.AddForce(force, ForceMode2D.Impulse);
    }
}
