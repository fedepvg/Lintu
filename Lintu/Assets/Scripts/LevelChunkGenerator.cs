using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelChunkGenerator : MonoBehaviour
{
    public List<GameObject> ObstaclesPrefabs;
    public GameObject LevelChunkPrefab;
    public GameObject FirstObstaclePosition;
    public GameObject ChunkFinishPosition;
    public bool FirstChunk;
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
            //GameObject go = Instantiate(ObstaclesPrefabs[rand], NextObstaclePosition, Quaternion.identity);
            GameObject go = ObjectPooler.Instance.GetPooledObject("Obstacle");
            go.transform.position = NextObstaclePosition;
            go.transform.rotation = Quaternion.identity;
            ObstaclesList.Add(go);
            
            PrevObstaclePosition = NextObstaclePosition;
            NextObstaclePosition += Vector3.forward * 200;
            if (NextObstaclePosition.z > ChunkFinishPosition.transform.position.z)
                generating = false;
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
        GameObject go = ObjectPooler.Instance.GetPooledObject("Environment");
        go.transform.position = ChunkFinishPosition.transform.position;
        go.transform.rotation = LevelChunkPrefab.transform.rotation;
        //GameObject go = Instantiate(LevelChunkPrefab, ChunkFinishPosition.transform.position, LevelChunkPrefab.transform.rotation);
        NextChunk = go;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (FirstChunk)
                Destroy(gameObject, 1f);
            else
                StartCoroutine(DeactivateChunk(1f));
            //Destroy(gameObject, 1f);
            NextChunk.GetComponent<LevelChunkGenerator>().GenerateNextChunk();
        }
    }

    private void OnDisable()
    {
        foreach (GameObject go in ObstaclesList)
        {
            go.transform.SetParent(null);
            go.SetActive(false);
        }
    }

    IEnumerator DeactivateChunk(float t)
    {
        yield return new WaitForSeconds(t);

        gameObject.SetActive(false);
    }
}
