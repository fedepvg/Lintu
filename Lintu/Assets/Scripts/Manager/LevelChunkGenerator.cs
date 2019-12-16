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
    IEnumerator DeactivateChunkCorutine;
    bool CanBeDeactivated = true;

    GameObject NextChunk;
    Vector3 NextObstaclePosition;
    string PrevObstacleName;

    private void Start()
    {
        BirdController.EndLevelAction += StopDestroying;
        if (FirstChunk)
        {
            GenerateNextChunk();
        }
    }

    private void OnEnable()
    {
        if (!FirstChunk)
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
            if (go != null)
            {
                if (PrevObstacleName == go.name)
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
                {
                    NextObstaclePosition += Vector3.forward * obstacleScript.GetDistanceToFinalPos();
                    obstacleScript.GenerateOrb();
                }
                ObstaclesList.Add(go);
            }

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
        go.GetComponent<LevelChunkGenerator>().FirstObstaclePosition.transform.position = NextObstaclePosition;
    }

    void StopDestroying()
    {
        CanBeDeactivated = false;
        if(DeactivateChunkCorutine != null)
            StopCoroutine(DeactivateChunkCorutine);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            if (FirstChunk)
            {
                DeactivateChunkCorutine = DestroyChunk(1f);
                StartCoroutine(DeactivateChunkCorutine);
            }
            else
            {
                DeactivateChunkCorutine = DeactivateChunk(1f);
                StartCoroutine(DeactivateChunkCorutine);
            }
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

    private void OnDestroy()
    {
        BirdController.EndLevelAction -= StopDestroying;
    }

    IEnumerator DeactivateChunk(float t)
    {
        yield return new WaitForSeconds(t);

        if(CanBeDeactivated)
            gameObject.SetActive(false);
    }

    IEnumerator DestroyChunk(float t)
    {
        yield return new WaitForSeconds(t);

        if(CanBeDeactivated)
            Destroy(gameObject);
    }
}
