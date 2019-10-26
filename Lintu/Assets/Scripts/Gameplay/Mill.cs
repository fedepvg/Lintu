using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mill : MonoBehaviour
{
    public float RotSpeed;
    void Update()
    {
        transform.Rotate(Vector3.right * RotSpeed * Time.deltaTime);
    }
}
