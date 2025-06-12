using UnityEngine;

public class TripleShotItem : MonoBehaviour
{
    public float duration = 10f;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerShooting shooter = other.GetComponent<PlayerShooting>();
            if (shooter != null)
            {
                shooter.StartCoroutine(shooter.TripleShot(duration));
            }

            Destroy(gameObject);
        }
    }
}
