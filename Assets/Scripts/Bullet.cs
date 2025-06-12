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
        // MonsterHealth�� �پ� �ִ� ��� ������Ʈ�� ������ ����
        MonsterHealth mh = other.GetComponent<MonsterHealth>();
        if (mh != null)
        {
            mh.TakeDamage(damage);
            Destroy(gameObject);
            return;
        }

        // �� ó��
        if (other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }
}
