using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneratorScript : MonoBehaviour
{
    public GameObject[] availableRoom;
    public List<GameObject> currentRoom;
    public float screenWidthInPoint;

    [Header("Obj Generatee")]
    public GameObject[] availableObj;
    public List<GameObject> currentObjList;

    public float objMinDistance = 5f;
    public float objMaxDistance = 10f;

    public float objectsMinY = -1.4f;
    public float objectsMaxY = 1.4f;

    public float objMinRotation = -45f;
    public float objMaxRotation = 45f;



    private void Start()
    {
        float height = 2 * Camera.main.orthographicSize;   // camera height
        screenWidthInPoint = height * Camera.main.aspect;  //camear width

        StartCoroutine(GeneratorCheck());

    }

    private IEnumerator GeneratorCheck()
    {
        while (true)
        {
            GenerateRoomIfRequired();
            GenerateObjIfRequired();
            yield return new WaitForSeconds(.25f);
        }
    }

    private void AddObjects(float lastObjX  )
    {
        GameObject obj = Instantiate(availableObj[Random.Range(0, availableObj.Length)]);

        float objPositionX = lastObjX + Random.Range(objMinDistance, objMaxDistance);
        float objPositionY = Random.Range(objectsMinY, objectsMaxY);

        obj.transform.position = new Vector3(objPositionX, objPositionY, 0);

        float rotation = Random.Range(objMinRotation, objMaxRotation);
        obj.transform.rotation = Quaternion.Euler(Vector3.forward * rotation);

        currentObjList.Add(obj);

    }

    private void GenerateObjIfRequired()
    {
        float farthestObjX = 0;
        List<GameObject> objToRemove = new List<GameObject>();

        foreach(var obj in currentObjList)
        {
            farthestObjX = Mathf.Max(farthestObjX, obj.transform.position.x);

            if(obj.transform.position.x < transform.position.x - screenWidthInPoint)
            {
                objToRemove.Add(obj);
            }
        }

        foreach(var obj in objToRemove)
        {
            currentObjList.Remove(obj);
            Destroy(obj);
        }

        if(farthestObjX < transform.position.x + screenWidthInPoint)
        {
            AddObjects(farthestObjX);
        }

        
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


}

