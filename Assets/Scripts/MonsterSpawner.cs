using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;
    public GameObject portalPrefab;
    public GameObject[] itemPrefabs;

    public Transform player;
    public Transform roomCenter;
    public Vector2 roomSize = new Vector2(25, 30);
    public float margin = 2f;
    public float minDistance = 3f;

    public int monstersPerWave = 5;
    public int maxWaves = 3;

    private int currentWave = 1;
    private int monstersAlive = 0;

    void Start()
    {
        if (player == null)
            player = GameObject.FindGameObjectWithTag("Player")?.transform;
        if (roomCenter == null)
            roomCenter = transform;

        MonsterHealth.OnAnyMonsterDeath += HandleMonsterDeath;
        SpawnWave();
    }

    void OnDestroy()
    {
        MonsterHealth.OnAnyMonsterDeath -= HandleMonsterDeath;
    }

    void SpawnWave()
    {
        for (int i = 0; i < monstersPerWave; i++)
        {
            Vector2 spawnPos;
            int attempts = 0;
            do
            {
                float x = Random.Range(-roomSize.x / 2f + margin, roomSize.x / 2f - margin);
                float y = Random.Range(-roomSize.y / 2f + margin, roomSize.y / 2f - margin);
                spawnPos = roomCenter.position + new Vector3(x, y, 0);
                attempts++;
            } while (Vector2.Distance(spawnPos, player.position) < minDistance && attempts < 50);

            int index = Random.Range(0, monsterPrefabs.Length);
            Instantiate(monsterPrefabs[index], spawnPos, Quaternion.identity);
            monstersAlive++;
        }

        Debug.Log($"웨이브 {currentWave} 시작, 몬스터 수: {monstersAlive}");
    }

    void HandleMonsterDeath()
    {
        monstersAlive--;
        Debug.Log($"몬스터 처치됨. 남은 몬스터: {monstersAlive}");

        if (monstersAlive <= 0)
        {
            if (currentWave < maxWaves)
            {
                currentWave++;
                SpawnWave();  // 다음 웨이브
            }
            else
            {
                SpawnPortalAndItem();
            }
        }
    }

    void SpawnPortalAndItem()
    {
        Vector3 portalPos = roomCenter.position + Vector3.up * 5f;
        Instantiate(portalPrefab, portalPos, Quaternion.identity);
        Debug.Log("최종 웨이브 종료. 포탈 생성!");

        if (itemPrefabs.Length > 0)
        {
            int index = Random.Range(0, itemPrefabs.Length);
            Vector3 itemPos = roomCenter.position + new Vector3(
                Random.Range(-roomSize.x / 2f + 1f, roomSize.x / 2f - 1f),
                Random.Range(-roomSize.y / 2f + 1f, roomSize.y / 2f - 1f),
                0f
            );
            Instantiate(itemPrefabs[index], itemPos, Quaternion.identity);
            Debug.Log("아이템 생성됨!");
        }
    }
}
