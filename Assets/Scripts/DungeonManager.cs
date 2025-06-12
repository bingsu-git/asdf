using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject[] roomPrefabs;   
    public Transform roomSpawnPoint;      
    public GameObject player;             

    private GameObject currentRoom;
    private Vector3 nextRoomPos;
    private int floor = 0;
    private float roomHeight = 12f;

    private int currentRoomIndex = 0;      

    void Start()
    {
        nextRoomPos = roomSpawnPoint.position;
        LoadNextRoom();
    }

    public void LoadNextRoom()
    {
        // ���� �� ����
        if (currentRoom != null)
            Destroy(currentRoom);

        // ��� �� Ŭ���� �� ���� ó��
        if (currentRoomIndex >= roomPrefabs.Length)
        {
            Debug.Log("��� �� Ŭ����");
            return;
        }

        // ������� �� ����
        GameObject nextRoom = roomPrefabs[currentRoomIndex];
        currentRoom = Instantiate(nextRoom, nextRoomPos, Quaternion.identity);
        currentRoomIndex++; // ���� �� �ε��� ����

        // �÷��̾� ��ġ �̵�
        Vector3 playerSpawn = nextRoomPos + new Vector3(0f, -roomHeight / 2f + 1f, 0f);
        player.transform.position = playerSpawn;

        // ���� �� ��ġ �غ�
        nextRoomPos += new Vector3(0f, -roomHeight, 0f);
        floor++;
        Debug.Log("Floor " + floor);

        ItemSpawner itemSpawner = currentRoom.GetComponentInChildren<ItemSpawner>();
        if (itemSpawner != null)
        {
            itemSpawner.isFinalRoom = (currentRoomIndex == roomPrefabs.Length);
        }
    }
}
