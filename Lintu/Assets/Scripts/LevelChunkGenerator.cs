﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkGenerator : MonoBehaviour
{
    public List<GameObject> ObstaclesPrefabs;
    public GameObject LevelChunkPrefab;
    public GameObject FirstObstaclePosition;
    public GameObject ChunkFinishPosition;
    public static bool FirstChunk = true;
    public Transform ObstaclesParent;

    public List<GameObject> ObstaclesList;
    GameObject NextChunk;
    Vector3 NextObstaclePosition;
    Vector3 PrevObstaclePosition;

    // Start is called before the first frame update
    void Start()
    {
        if (FirstChunk)
        {
            GenerateNextChunk();
            FirstChunk = false;
        }
        else
        {
            NextObstaclePosition = FirstObstaclePosition.transform.position;
            GenerateObstacles();
        }
    }

    void GenerateObstacles()
    {
        bool generating = true;

        while (generating)
        {
            int rand = Random.Range(0, ObstaclesPrefabs.Count);
            GameObject go = Instantiate(ObstaclesPrefabs[rand], NextObstaclePosition, Quaternion.identity);
            ObstaclesList.Add(go);
            
            PrevObstaclePosition = NextObstaclePosition;
            NextObstaclePosition += Vector3.forward * 200;
            if (NextObstaclePosition.z > ChunkFinishPosition.transform.position.z)
                generating = false;
            Debug.Log("zarlanga");
        }

         ReScaleObstacles();
    }

    void ReScaleObstacles()
    {
        foreach(GameObject go in ObstaclesList)
        {
            go.transform.SetParent(ObstaclesParent);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.localRotation = Quaternion.identity;
        }
    }

    public void GenerateNextChunk()
    {
        GameObject go = Instantiate(LevelChunkPrefab, ChunkFinishPosition.transform.position, LevelChunkPrefab.transform.rotation);
        NextChunk = go;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag=="Player")
        {
            NextChunk.GetComponent<LevelChunkGenerator>().GenerateNextChunk();
            Destroy(gameObject, 1f);
        }
    }
}
