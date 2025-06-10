using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public GameObject portalPrefab;

    public Transform player;
    public Transform roomCenter;

    public Vector2 roomSize = new Vector2(25, 30);
    public float margin = 2f;
    public int monsterCount = 5;
    public float minDistance = 3f;

    private int monstersAlive = 0;
    private bool spawned = false;

    // ������ �� ���� �̺�Ʈ ����
    private void Awake()
    {
        Debug.Log("[Spawner] Awake: " + gameObject.name);
        MonsterHealth.OnAnyMonsterDeath -= HandleMonsterDeath;
        MonsterHealth.OnAnyMonsterDeath += HandleMonsterDeath;
    }

    void Start()
    {
        // �÷��̾� ���� Ȯ��
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        // roomCenter�� ������ �ڱ� �ڽ�(������ ��Ʈ)�� ���
        if (roomCenter == null)
            roomCenter = transform;

        SpawnMonstersOnce();
    }

    void OnDestroy()
    {
        // �޸� ���� ������ ���� �̺�Ʈ ���� ����
        MonsterHealth.OnAnyMonsterDeath -= HandleMonsterDeath;
    }

    void SpawnMonstersOnce()
    {
        if (spawned) return;
        spawned = true;
        monstersAlive = 0; // ���� �� �ʱ�ȭ

        int spawnedCount = 0;
        int attempts = 0;

        while (spawnedCount < monsterCount && attempts < 100)
        {
            float x = Random.Range(-roomSize.x / 2f + margin, roomSize.x / 2f - margin);
            float y = Random.Range(-roomSize.y / 2f + margin, roomSize.y / 2f - margin);
            Vector2 localPos = new Vector2(x, y);
            Vector2 spawnPos = (Vector2)roomCenter.position + localPos;

            // �÷��̾���� �ּ� �Ÿ� ����
            if (Vector2.Distance(spawnPos, player.position) >= minDistance)
            {
                int index = Random.Range(0, monsterPrefabs.Length);
                Instantiate(monsterPrefabs[index], spawnPos, Quaternion.identity);
                spawnedCount++;
                monstersAlive++;
            }

            attempts++;
        }

        Debug.Log($"���� {monstersAlive}���� ������");
    }

    void HandleMonsterDeath()
    {
        monstersAlive--;
        Debug.Log($" ���� ���� ������. ���� ����: {monstersAlive}");

        if (monstersAlive <= 0)
        {
            Debug.Log(" ��� ���� ���ŵ�! ��Ż ����!");
            // ��Ż ��ġ: roomCenter���� ���� 5 ����. (�ʿ�� player.position ������ ���� ����)
            Instantiate(portalPrefab, roomCenter.position + Vector3.up * 5f, Quaternion.identity);
        }
    }
}
