using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessObstacle : MonoBehaviour
{
    public List<GameObject> OrbPositions;
    public GameObject FinalPosition;
    public float OrbProbability;

    bool HasOrb;

    // Start is called before the first frame update
    void Start()
    {
        OrbProbability = 100f;
        if (Random.Range(0f, 100f) <= OrbProbability)
            HasOrb = true;
        else 
            HasOrb = false;

        if(HasOrb)
        {
            int rand = Random.Range(0, OrbPositions.Count);
            GameObject orb = ObjectPooler.Instance.GetPooledObject("Orb");
            if (orb != null)
            {
                orb.transform.SetParent(OrbPositions[rand].transform);
                orb.transform.position = OrbPositions[rand].transform.position;
            }
        }
    }

    public float GetDistanceToFinalPos()
    {
        return Vector3.Distance(transform.position,FinalPosition.transform.position);
    }
}
