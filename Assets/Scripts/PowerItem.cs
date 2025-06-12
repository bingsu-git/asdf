using UnityEngine;

public class PowerItem : MonoBehaviour
{
    public float duration = 10f;
    public int damageBonus = 1;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooter = other.GetComponent<PlayerShooting>();
            if (shooter != null)
            {
                shooter.StartCoroutine(shooter.BoostDamage(damageBonus, duration));
            }

            Destroy(gameObject);
        }
    }
}
