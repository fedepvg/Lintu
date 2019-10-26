using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour
{
    public int EnergyRecovered;
    public int RotationSpeed;
    public delegate void OrbPickUpAction(int energy);
    public static OrbPickUpAction OnOrbPickup;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * RotationSpeed * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (OnOrbPickup != null)
            OnOrbPickup(EnergyRecovered);
        Destroy(gameObject);
    }
}
