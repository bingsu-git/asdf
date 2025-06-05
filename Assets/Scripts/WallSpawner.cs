using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public int wallCount = 10;
    public Vector2 roomSize = new Vector2(10, 8); // 가로 x 세로 범위

    void Start()
    {
        for (int i = 0; i < wallCount; i++)
        {
            Vector2 pos = new Vector2(
                Random.Range(-roomSize.x / 2f, roomSize.x / 2f),
                Random.Range(-roomSize.y / 2f, roomSize.y / 2f)
            );

            // 방 중심 근처는 비워두기 (플레이어 스폰 고려)
            if (Mathf.Abs(pos.x) < 2f && Mathf.Abs(pos.y) < 2f)
                continue;

            Vector3 spawnPos = transform.position + new Vector3(pos.x, pos.y, 0f);
            Instantiate(wallPrefab, spawnPos, Quaternion.identity);
        }
    }
}
