using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float lifeTime = 2f;
    public int damage = 1;

    void Start()
    {
        Destroy(gameObject, lifeTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        // MonsterHealth가 붙어 있는 모든 오브젝트에 데미지 적용
        MonsterHealth mh = other.GetComponent<MonsterHealth>();
        if (mh != null)
        {
            mh.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // 벽 처리
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
