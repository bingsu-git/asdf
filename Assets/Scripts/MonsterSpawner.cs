using UnityEngine;

public class MonsterSpawner : MonoBehaviour
{
    public GameObject[] monsterPrefabs;   // �پ��� ���� �����յ�
    public Transform player;              // �÷��̾� ��ġ
    public float spawnInterval = 3f;      // ���� ����
    public int maxMonsters = 10;          // �ִ� ��
    public float spawnRange = 5f;         // ���� �ݰ�
    public float minDistance = 3f;        // �÷��̾�� �ּ� �Ÿ�

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
                Debug.LogWarning("Player ������Ʈ�� ã�� �� �����ϴ�!");
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

            // �÷��̾�� �ʹ� ����� ��ġ�� ��õ�
            do
            {
                spawnPos = (Vector2)player.position + Random.insideUnitCircle * spawnRange;
                safety++;
                if (safety > 50) return; // ���ѷ��� ����
            }
            while (Vector2.Distance(spawnPos, player.position) < minDistance);

            // ������ ���� ������ ����
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
