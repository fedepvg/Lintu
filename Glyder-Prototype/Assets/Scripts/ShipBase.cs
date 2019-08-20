using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShipBase : MonoBehaviour
{
    [SerializeField]
    GameObject ExplosionPrefab;
    protected int Health;

    protected void Explode()
    {
        Instantiate(ExplosionPrefab, transform.position, Quaternion.identity);
        this.gameObject.SetActive(false);
    }
}
