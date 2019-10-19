using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CityCreator : MonoBehaviour
{
    #region PublicVariables
    public enum Orientations { left, right };
    public float Width;
    public float Height;
    public float Depth;
    public float BorderDistance;
    public Orientations Orientation;
    public List<GameObject> Buildings;
    #endregion


    void Start()
    {
        transform.localScale = new Vector3(Width, Height, Depth);
        CreateCity();
    }

    void CreateCity()
    {

    }
}
