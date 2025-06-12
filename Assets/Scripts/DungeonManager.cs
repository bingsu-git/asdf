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
        // 기존 방 제거
        if (currentRoom != null)
            Destroy(currentRoom);

        // 모든 방 클리어 시 종료 처리
        if (currentRoomIndex >= roomPrefabs.Length)
        {
            Debug.Log("모든 방 클리어");
            return;
        }

        // 순서대로 방 생성
        GameObject nextRoom = roomPrefabs[currentRoomIndex];
        currentRoom = Instantiate(nextRoom, nextRoomPos, Quaternion.identity);
        currentRoomIndex++; // 다음 방 인덱스 증가

        // 플레이어 위치 이동
        Vector3 playerSpawn = nextRoomPos + new Vector3(0f, -roomHeight / 2f + 1f, 0f);
        player.transform.position = playerSpawn;

        // 다음 방 위치 준비
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
