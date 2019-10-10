using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : MonoBehaviour
{
    public float RotSpeed;
    void Update()
    {
        transform.Rotate(new Vector3(0f, RotSpeed * Time.deltaTime, 0f));
    }
}
