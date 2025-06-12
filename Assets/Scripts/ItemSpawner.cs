using UnityEngine;

public class ItemSpawner : MonoBehaviour
{
    public GameObject[] itemPrefabs;
    public Vector2 roomSize = new Vector2(25, 30);
    public bool isFinalRoom = false;
    public float spawnInterval = 10f;

    void Start()
    {
        if (isFinalRoom)
        {
            InvokeRepeating(nameof(SpawnItem), 3f, spawnInterval);
        }
    }

    void SpawnItem()
    {
        if (itemPrefabs.Length == 0) return;

        int index = Random.Range(0, itemPrefabs.Length);
        GameObject prefab = itemPrefabs[index];

        float x = Random.Range(-roomSize.x / 2 + 1f, roomSize.x / 2 - 1f);
        float y = Random.Range(-roomSize.y / 2 + 1f, roomSize.y / 2 - 1f);
        Vector3 spawnPos = transform.position + new Vector3(x, y, 0);

        Instantiate(prefab, spawnPos, Quaternion.identity);
    }
}
