using UnityEngine;

public class Monster : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public float speed = 2f;
    public float attackRange = 5f;
    public float attackCooldown = 2f;

    private float attackTimer = 0f;

    // 충돌 데미지 관련
    private float hitCooldown = 1f;
    private float lastHitTime = -999f;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;

        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    void Update()
    {
        attackTimer += Time.deltaTime;

        if (target != null)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            transform.position += (Vector3)(direction * speed * Time.deltaTime);

            float distance = Vector2.Distance(transform.position, target.position);

            if (distance <= attackRange && attackTimer >= attackCooldown)
            {
                Shoot();
                attackTimer = 0f;
            }
        }
    }

    void Shoot()
    {
        Vector3 dir = (target.position - transform.position).normalized;
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        bullet.transform.right = dir;

        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.linearVelocity = dir * 5f;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") && Time.time - lastHitTime >= hitCooldown)
        {
            PlayerHealth player = collision.gameObject.GetComponent<PlayerHealth>();
            if (player != null)
            {
                player.TakeDamage(1);
                lastHitTime = Time.time;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            MonsterHealth health = GetComponent<MonsterHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }

            Destroy(other.gameObject);
        }
    }
}
