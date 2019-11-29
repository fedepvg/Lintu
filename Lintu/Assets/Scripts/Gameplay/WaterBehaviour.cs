using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterBehaviour : MonoBehaviour
{
    public GameObject CollisionParticlesPrefab;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Player")
            Instantiate(CollisionParticlesPrefab, collision.contacts[0].point, Quaternion.identity);
    }
}
