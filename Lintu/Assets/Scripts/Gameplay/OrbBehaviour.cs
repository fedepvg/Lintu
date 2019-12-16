using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour
{
    public int EnergyRecovered;
    public int RotationSpeed;
    public delegate void OrbPickUpAction(int energy);
    public static OrbPickUpAction OnOrbPickup;

    void Update()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            if (OnOrbPickup != null)
                OnOrbPickup(EnergyRecovered);
            gameObject.SetActive(false);
            transform.parent = null;
        }
    }
}
