using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndlessObstacle : MonoBehaviour
{
    public List<GameObject> OrbPositions;
    public GameObject FinalPosition;
    public float OrbProbability;

    bool HasOrb;
    float InitialOrbProbability = 100f;
    float TimeToLoseOrbChance = 10f;
    float ChanceLostByTime = 5f;
    float MinOrbChance = 20f;

    public void GenerateOrb(float time)
    {
        OrbProbability = InitialOrbProbability - ChanceLostByTime * (time / TimeToLoseOrbChance);
        if (OrbProbability <= 20f)
            OrbProbability = 20f;

        if (Random.Range(0f, 100f) <= OrbProbability)
            HasOrb = true;
        else
            HasOrb = false;

        if (HasOrb)
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
