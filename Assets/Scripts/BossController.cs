using UnityEngine;

public class BossController : MonoBehaviour
{
    public GameObject bulletPrefab;
    public GameObject[] minionPrefabs;
    public Transform spawnCenter;
    public Vector2 spawnRange = new Vector2(8f, 3f);

    public float shootInterval = 2f;
    public float summonInterval = 5f;

    private float shootTimer = 0f;
    private float summonTimer = 0f;

    void Start()
    {
        shootTimer = shootInterval;
        summonTimer = summonInterval;
    }

    void Update()
    {
        shootTimer -= Time.deltaTime;
        summonTimer -= Time.deltaTime;

        if (shootTimer <= 0f)
        {
            ShootCircle(12); // 12�� ź��
            shootTimer = shootInterval;
        }

        if (summonTimer <= 0f)
        {
            SummonMinion();
            summonTimer = summonInterval;
        }
    }

    void ShootCircle(int bulletCount)
    {
        for (int i = 0; i < bulletCount; i++)
        {
            float angle = i * (360f / bulletCount);
            Vector2 dir = Quaternion.Euler(0, 0, angle) * Vector2.right;

            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            bullet.GetComponent<Rigidbody2D>().linearVelocity = dir * 4f;
        }

        Debug.Log("���� ź�� �߻�!");
    }

    void SummonMinion()
    {
        if (minionPrefabs.Length == 0) return;

        int index = Random.Range(0, minionPrefabs.Length);
        Vector2 offset = new Vector2(
            Random.Range(-spawnRange.x, spawnRange.x),
            Random.Range(-spawnRange.y, spawnRange.y)
        );

        Vector2 spawnPos = (Vector2)spawnCenter.position + offset;
        Instantiate(minionPrefabs[index], spawnPos, Quaternion.identity);

        Debug.Log("������ ���͸� ��ȯ��!");
    }
}
