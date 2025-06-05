using UnityEngine;

public class DungeonManager : MonoBehaviour
{
    public GameObject[] roomPrefabs;
    public Transform roomSpawnPoint;
    public GameObject player;
    public GameObject exitPortalPrefab;
    public Vector3 portalOffset = new Vector3(0f, 5f, 0f);

    private GameObject currentRoom;
    private int floor = 0;
    private Vector3 nextRoomPos;
    private float roomHeight = 12f;

    void Start()
    {
        nextRoomPos = roomSpawnPoint.position;
        LoadNextRoom();
    }

    public void LoadNextRoom()
    {
        if (currentRoom != null)
            Destroy(currentRoom);

        int index = Random.Range(0, roomPrefabs.Length);
        currentRoom = Instantiate(roomPrefabs[index], nextRoomPos, Quaternion.identity);

        // 포탈 생성
        Vector3 portalPos = nextRoomPos + portalOffset;
        Instantiate(exitPortalPrefab, portalPos, Quaternion.identity);

        // 플레이어 아래에서 시작
        Vector3 playerSpawn = nextRoomPos + new Vector3(0f, -roomHeight / 2f + 1f, 0f);
        player.transform.position = playerSpawn;

        nextRoomPos += new Vector3(0f, roomHeight, 0f);
        floor++;
        Debug.Log("Floor " + floor);
    }
}
