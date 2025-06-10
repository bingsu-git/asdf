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

    // 가능한 한 빨리 이벤트 구독
    private void Awake()
    {
        Debug.Log("[Spawner] Awake: " + gameObject.name);
        MonsterHealth.OnAnyMonsterDeath -= HandleMonsterDeath;
        MonsterHealth.OnAnyMonsterDeath += HandleMonsterDeath;
    }

    void Start()
    {
        // 플레이어 참조 확인
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
                player = playerObj.transform;
        }

        // roomCenter가 없으면 자기 자신(프리팹 루트)을 사용
        if (roomCenter == null)
            roomCenter = transform;

        SpawnMonstersOnce();
    }

    void OnDestroy()
    {
        // 메모리 누수 방지를 위해 이벤트 구독 해제
        MonsterHealth.OnAnyMonsterDeath -= HandleMonsterDeath;
    }

    void SpawnMonstersOnce()
    {
        if (spawned) return;
        spawned = true;
        monstersAlive = 0; // 생성 전 초기화

        int spawnedCount = 0;
        int attempts = 0;

        while (spawnedCount < monsterCount && attempts < 100)
        {
            float x = Random.Range(-roomSize.x / 2f + margin, roomSize.x / 2f - margin);
            float y = Random.Range(-roomSize.y / 2f + margin, roomSize.y / 2f - margin);
            Vector2 localPos = new Vector2(x, y);
            Vector2 spawnPos = (Vector2)roomCenter.position + localPos;

            // 플레이어와의 최소 거리 보장
            if (Vector2.Distance(spawnPos, player.position) >= minDistance)
            {
                int index = Random.Range(0, monsterPrefabs.Length);
                Instantiate(monsterPrefabs[index], spawnPos, Quaternion.identity);
                spawnedCount++;
                monstersAlive++;
            }

            attempts++;
        }

        Debug.Log($"몬스터 {monstersAlive}마리 생성됨");
    }

    void HandleMonsterDeath()
    {
        monstersAlive--;
        Debug.Log($" 몬스터 죽음 감지됨. 남은 몬스터: {monstersAlive}");

        if (monstersAlive <= 0)
        {
            Debug.Log(" 모든 몬스터 제거됨! 포탈 생성!");
            // 포탈 위치: roomCenter에서 위로 5 단위. (필요시 player.position 등으로 조정 가능)
            Instantiate(portalPrefab, roomCenter.position + Vector3.up * 5f, Quaternion.identity);
        }
    }
}
