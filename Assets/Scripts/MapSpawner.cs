using System.Collections.Generic;
using UnityEngine;

public class MapSpawner : MonoBehaviour
{
    public GameObject[] mapPrefabs;  
    public Transform player;  
    public float spawnDistance = 30f; 
    private float nextSpawnPos = 0f;
    private Queue<GameObject> activeSegments = new Queue<GameObject>();

    void Start()
    {
        for (int i = 0; i < 2; i++)
        {
            SpawnFirstSegment();
        }
        for (int i = 0; i < 3; i++) 
        {
            SpawnSegment();
        }
    }

    void Update()
    {
        if (player.position.z + spawnDistance > nextSpawnPos)
        {
            SpawnSegment();
            RemoveOldSegment();
        }
    }

    void SpawnFirstSegment()
    {
        GameObject newSegment = Instantiate(mapPrefabs[0],
                                           new Vector3(0, 0, nextSpawnPos),
                                           Quaternion.identity);
        activeSegments.Enqueue(newSegment);
        nextSpawnPos += 30f;
    }
    void SpawnSegment()
    {
        GameObject newSegment = Instantiate(mapPrefabs[Random.Range(0, mapPrefabs.Length)],
                                           new Vector3(0, 0, nextSpawnPos),
                                           Quaternion.identity);
        activeSegments.Enqueue(newSegment);
        nextSpawnPos += 30f; 
    }

    void RemoveOldSegment()
    {
        if (activeSegments.Count > 5) 
        {
            Destroy(activeSegments.Dequeue());
        }
    }
}
