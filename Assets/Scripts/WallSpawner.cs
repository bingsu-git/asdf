using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public int randomWallCount = 5;               // �߰� ���� �� ��
    public Vector2 roomSize = new Vector2(10, 8); // �� ũ��
    public float spacing = 1f;                    // ��ħ ������ ũ��

    void Start()
    {
        Vector3 origin = transform.position;

        // 1. ������ �� �� �� ���� (���� �Ǵ� ����)
        bool horizontal = Random.value > 0.5f; // ���� ����
        float fixedCoord = Random.Range(-roomSize.y / 2f + 1f, roomSize.y / 2f - 1f); // Y �Ǵ� X ��ġ ����

        for (int i = -4; i <= 4; i++)
        {
            Vector3 pos = horizontal
                ? origin + new Vector3(i, fixedCoord, 0f)   // ������
                : origin + new Vector3(fixedCoord, i, 0f);  // ������

            if (!IsOccupied(pos))
                Instantiate(wallPrefab, pos, Quaternion.identity);
        }

        // 2. �߰� ���� �� �� �� �Ѹ���
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
                continue; // �߾� ���ϱ�

            Vector3 spawnPos = origin + new Vector3(randPos.x, randPos.y, 0f);

            if (IsOccupied(spawnPos)) continue;

            Instantiate(wallPrefab, spawnPos, Quaternion.identity);
            placed++;
        }
    }

    // ��ġ ��ħ ���� �Լ�
    bool IsOccupied(Vector3 position)
    {
        return Physics2D.OverlapBox(position, Vector2.one * spacing, 0f) != null;
    }
}
