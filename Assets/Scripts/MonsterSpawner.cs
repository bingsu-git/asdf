using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;   // 다양한 몬스터 프리팹들
    public Transform player;              // 플레이어 위치
    public float spawnInterval = 3f;      // 생성 간격
    public int maxMonsters = 10;          // 최대 수
    public float spawnRange = 5f;         // 생성 반경
    public float minDistance = 3f;        // 플레이어와 최소 거리

    private float timer = 0f;

    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }
            else
            {
                Debug.LogWarning("Player 오브젝트를 찾을 수 없습니다!");
            }
        }
    }

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= spawnInterval && CountMonsters() < maxMonsters)
        {
            Vector2 spawnPos;
            int safety = 0;

            // 플레이어와 너무 가까운 위치는 재시도
            do
            {
                spawnPos = (Vector2)player.position + Random.insideUnitCircle * spawnRange;
                safety++;
                if (safety > 50) return; // 무한루프 방지
            }
            while (Vector2.Distance(spawnPos, player.position) < minDistance);

            // 랜덤한 몬스터 프리팹 선택
            int index = Random.Range(0, monsterPrefabs.Length);
            GameObject chosenPrefab = monsterPrefabs[index];

            Instantiate(chosenPrefab, spawnPos, Quaternion.identity);
            timer = 0f;
        }
    }

    int CountMonsters()
    {
        return GameObject.FindGameObjectsWithTag("Monster").Length;
    }
}
