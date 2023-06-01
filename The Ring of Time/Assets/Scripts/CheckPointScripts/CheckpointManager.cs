using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class ObjectPositionData
{
    public string objectId;
    public float posX;
    public float posY;
    public float posZ;

    public ObjectPositionData(string id, Vector3 position)
    {
        objectId = id;
        posX = position.x;
        posY = position.y;
        posZ = position.z;
    }
}

public class CheckpointManager : MonoBehaviour
{
    private string saveFileName = "checkpoint.dat";
    private List<ObjectPositionData> objectPositions = new List<ObjectPositionData>();

    // Save object positions to a file
    public void SavePositions()
    {
        objectPositions.Clear();

        // Iterate through all game objects with a position
        GameObject[] objectsWithPosition = GameObject.FindGameObjectsWithTag("Pickup");
        foreach (GameObject obj in objectsWithPosition)
        {
            if(!(obj.name == "Ring"))
            {
                string objectId = obj.name; // Use object's name as the identifier
                Vector3 position = obj.transform.position;
                ObjectPositionData positionData = new ObjectPositionData(objectId, position);
                objectPositions.Add(positionData);
            }
            
        }

        // Serialize and save the object positions
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Path.Combine(Application.persistentDataPath, saveFileName));
        bf.Serialize(file, objectPositions);
        file.Close();
    }

    // Load object positions from the saved file
    public void LoadPositions()
    {
        string filePath = Path.Combine(Application.persistentDataPath, saveFileName);
        if (File.Exists(filePath))
        {
            // Deserialize the object positions
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(filePath, FileMode.Open);
            objectPositions = (List<ObjectPositionData>)bf.Deserialize(file);
            file.Close();

            // Update the positions of the objects in the scene
            foreach (ObjectPositionData positionData in objectPositions)
            {
                GameObject obj = GameObject.Find(positionData.objectId);
                if (obj != null)
                {
                    Vector3 position = new Vector3(positionData.posX, positionData.posY, positionData.posZ);
                    obj.transform.position = position;
                    obj.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
                    if (obj.GetComponent<EnableMovementOnFreeze>() != null) obj.GetComponent<EnableMovementOnFreeze>().SetVelocity(Vector2.zero);   // Stores velocity for objects that only move when frozen
                }
            }
        }
    }
}