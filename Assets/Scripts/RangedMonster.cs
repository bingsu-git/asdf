using UnityEngine;

public class RangedMonster : MonoBehaviour
{
    public Transform target;
    public GameObject bulletPrefab;
    public float attackRange = 7f;
    public float retreatDistance = 4f;
    public float attackCooldown = 2f;
    public float moveSpeed = 1.5f;

    private float attackTimer = 0f;

    void Start()
    {
        if (target == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                target = playerObj.transform;
        }
    }

    void Update()
    {
        if (target == null) return;

        attackTimer += Time.deltaTime;
        float distance = Vector2.Distance(transform.position, target.position);

        // 가까우면 도망
        if (distance < retreatDistance)
        {
            Vector2 dir = (transform.position - target.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }
        // 멀면 다가감
        else if (distance > attackRange)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            transform.position += (Vector3)(dir * moveSpeed * Time.deltaTime);
        }

        // 사정거리 안이면 공격
        if (distance <= attackRange && attackTimer >= attackCooldown)
        {
            Shoot();
            attackTimer = 0f;
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

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            //  MonsterHealth에게 체력 감소 + 사망 처리 위임
            MonsterHealth health = GetComponent<MonsterHealth>();
            if (health != null)
            {
                health.TakeDamage(1);
            }

            Destroy(other.gameObject);
        }
    }
}
