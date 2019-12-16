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
    public int MinObstacleDistance;
    public int MaxObstacleDistance;
    public List<GameObject> ObstaclesList;

    GameObject NextChunk;
    Vector3 NextObstaclePosition;
    string PrevObstacleName;

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
            GameObject go = ObjectPooler.Instance.GetRandomPooledObject("Obstacle");
            if(PrevObstacleName == go.name)
            {
                go.SetActive(false);
                go = ObjectPooler.Instance.GetRandomPooledObject("Obstacle");
            }
            PrevObstacleName = go.name;
            go.transform.SetParent(ObstaclesParent);
            go.transform.localScale = new Vector3(1, 1, 1);
            go.transform.localRotation = Quaternion.identity;
            go.transform.position = NextObstaclePosition;

            EndlessObstacle obstacleScript = go.GetComponent<EndlessObstacle>();
            if (obstacleScript != null)
                NextObstaclePosition += Vector3.forward * obstacleScript.GetDistanceToFinalPos();
            ObstaclesList.Add(go);

            float nextObstacleDistance = Random.Range(MinObstacleDistance, MaxObstacleDistance);
            NextObstaclePosition += Vector3.forward * nextObstacleDistance;
            if (NextObstaclePosition.z > ChunkFinishPosition.transform.position.z)
                generating = false;
        }
    }

    public void GenerateNextChunk()
    {
        GameObject go = ObjectPooler.Instance.GetPooledObject("Environment");
        go.transform.position = ChunkFinishPosition.transform.position;
        go.transform.rotation = LevelChunkPrefab.transform.rotation;
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
