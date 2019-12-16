using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Tier
{
    public int VRam;
    public int Ram;
    public int ProcessorCount;
}

public class QualityManagement : MonoBehaviour
{
    public List<Tier> TargetGraphicTiers;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
