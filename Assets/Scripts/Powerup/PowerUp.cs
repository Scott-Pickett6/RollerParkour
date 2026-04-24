using Assets.Scripts.Managers;
using UnityEngine;

public class PowerUp : MonoBehaviour
{

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && PowerUpManager.Instance != null)
        {
            PowerUpManager.Instance.HitPowerUp();
            Destroy(gameObject);
            Destroy(transform.parent.gameObject);
        }
    }
}
