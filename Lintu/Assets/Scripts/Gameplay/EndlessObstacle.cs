using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessObstacle : MonoBehaviour
{
    public List<GameObject> OrbPositions;
    public GameObject OrbPrefab;
    public GameObject FinalPosition;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public float GetDistanceToFinalPos()
    {
        return Vector3.Distance(transform.position,FinalPosition.transform.position);
    }
}
