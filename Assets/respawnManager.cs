using UnityEngine;

public class RespawnManager : MonoBehaviour
{
    public static RespawnManager instance;
    public Transform respawnPoint;

    private void Awake()
    {
        instance = this;
    }

    public void RespawnPlayer()
    {
        Health playerHealth = FindObjectOfType<SideScrollPlayer>().GetComponent<Health>();
        playerHealth.Respawn(respawnPoint.position);
    }
}
