using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public int randomWallCount = 5;               // 추가 랜덤 벽 수
    public Vector2 roomSize = new Vector2(10, 8); // 방 크기
    public float spacing = 1f;                    // 겹침 방지용 크기

    void Start()
    {
        Vector3 origin = transform.position;

        // 1. 일자형 벽 한 줄 생성 (가로 또는 세로)
        bool horizontal = Random.value > 0.5f; // 방향 랜덤
        float fixedCoord = Random.Range(-roomSize.y / 2f + 1f, roomSize.y / 2f - 1f); // Y 또는 X 위치 랜덤

        for (int i = -4; i <= 4; i++)
        {
            Vector3 pos = horizontal
                ? origin + new Vector3(i, fixedCoord, 0f)   // 가로줄
                : origin + new Vector3(fixedCoord, i, 0f);  // 세로줄

            if (!IsOccupied(pos))
                Instantiate(wallPrefab, pos, Quaternion.identity);
        }

        // 2. 추가 랜덤 벽 몇 개 뿌리기
        int placed = 0;
        int attempts = 0;

        while (placed < randomWallCount && attempts < randomWallCount * 10)
        {
            attempts++;

            Vector2 randPos = new Vector2(
                Random.Range(-roomSize.x / 2f, roomSize.x / 2f),
                Random.Range(-roomSize.y / 2f, roomSize.y / 2f)
            );

            if (Mathf.Abs(randPos.x) < 2f && Mathf.Abs(randPos.y) < 2f)
                continue; // 중앙 피하기

            Vector3 spawnPos = origin + new Vector3(randPos.x, randPos.y, 0f);

            if (IsOccupied(spawnPos)) continue;

            Instantiate(wallPrefab, spawnPos, Quaternion.identity);
            placed++;
        }
    }

    // 위치 겹침 감지 함수
    bool IsOccupied(Vector3 position)
    {
        return Physics2D.OverlapBox(position, Vector2.one * spacing, 0f) != null;
    }
}
