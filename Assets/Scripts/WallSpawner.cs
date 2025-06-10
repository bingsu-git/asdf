using UnityEngine;

public class WallSpawner : MonoBehaviour
{
    public GameObject wallPrefab;
    public Vector2 roomSize = new Vector2(50, 70); // �� ũ��

    void Start()
    {
        SpawnBorderWalls(roomSize);
    }

    void SpawnBorderWalls(Vector2 size)
    {
        Vector3 origin = transform.position;

        int width = Mathf.RoundToInt(size.x);
        int height = Mathf.RoundToInt(size.y);

        // ���Ʒ� ��
        for (int x = -width / 2; x <= width / 2; x++)
        {
            Instantiate(wallPrefab, origin + new Vector3(x, -height / 2, 0), Quaternion.identity);
            Instantiate(wallPrefab, origin + new Vector3(x, height / 2, 0), Quaternion.identity);
        }

        // �¿� ��
        for (int y = -height / 2 + 1; y <= height / 2 - 1; y++)
        {
            Instantiate(wallPrefab, origin + new Vector3(-width / 2, y, 0), Quaternion.identity);
            Instantiate(wallPrefab, origin + new Vector3(width / 2, y, 0), Quaternion.identity);
        }
    }
}
