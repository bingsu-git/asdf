using UnityEngine;

public class MonsterBullet : MonoBehaviour
{
    public float speed = 5f;
    public float lifeTime = 3f;

    void Start()
    {
        Destroy(gameObject, lifeTime); // ���� ������ ����
    }

    void Update()
    {
        transform.Translate(Vector2.right * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth health = other.GetComponent<PlayerHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }

            Destroy(gameObject);
        }
        else if (other.CompareTag("Wall"))
        {
            Destroy(gameObject); // ���� �ε����� �ٷ� ����
        }
    }
}
