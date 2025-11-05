using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject[] availableRoom;
    public List<GameObject> currentRoom;
    public float screenWidthInPoint;

    private void Start()
    {
        float height = 2 * Camera.main.orthographicSize;   // camera height
        screenWidthInPoint = height * Camera.main.aspect;  //camear width

        StartCoroutine(GeneratorCheck());

    }

    private void AddRoom(float farthestRoomEx)
    {
        GameObject newRoom = Instantiate(availableRoom[Random.Range(0, availableRoom.Length)]);

        float roomWidth = newRoom.transform.Find("Floor").localScale.x;

        float roomCenter = farthestRoomEx + roomWidth * .5f;

        newRoom.transform.position = new Vector3( roomCenter,0, 0);

        //add that room to the current room list
        currentRoom.Add(newRoom);   
    }
    
    private void GenerateRoomIfRequired()
    {
        List<GameObject> roomToRemove = new List<GameObject>();

        bool addRoom = true;

        float farthestRoomEndX = 0;
        foreach(var room in currentRoom)
        {
            float roomWidth = room.transform.Find("Floor").localScale.x;
            float roomStartPosX = room.transform.position.x - (roomWidth * .5f);
            float roomEndPosX = roomStartPosX + roomWidth;
             

            if(roomStartPosX> transform.position.x+ screenWidthInPoint)
            {
                addRoom = false;  
            }

            if(roomEndPosX < transform.position.x - screenWidthInPoint) // if the start of the currentRoom > the check range : that means prevoius room is out 
            {
                roomToRemove.Add(room);
            }
            farthestRoomEndX = Mathf.Max(farthestRoomEndX, roomEndPosX);  // ???

        }

        foreach(var room in roomToRemove)
        {
            currentRoom.Remove(room);  // remove it from the current List
            Destroy(room);
        }

        if (addRoom)
        {
            AddRoom(farthestRoomEndX);
        }
    }

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            yield return new WaitForSeconds(.25f);
        }
    }
}

