using UnityEngine;

public class RusherMonster : MonoBehaviour
{
    public Transform target;
    public float rushSpeed = 5f;
    public float rushInterval = 3f;

    private float rushTimer = 0f;

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
        rushTimer += Time.deltaTime;

        if (rushTimer >= rushInterval && target != null)
        {
            Vector2 dir = (target.position - transform.position).normalized;
            GetComponent<Rigidbody2D>().AddForce(dir * rushSpeed, ForceMode2D.Impulse);
            rushTimer = 0f;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Bullet"))
        {
            Destroy(gameObject); // 간단한 처리
        }
    }
}
