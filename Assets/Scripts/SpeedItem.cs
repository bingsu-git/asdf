using UnityEngine;

public class SpeedItem : MonoBehaviour
{
    public float duration = 10f;
    public float fireRateBoost = 0.1f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooter = other.GetComponent<PlayerShooting>();
            if (shooter != null)
            {
                shooter.StartCoroutine(shooter.BoostFireRate(fireRateBoost, duration));
            }

            Destroy(gameObject);
        }
    }
}
