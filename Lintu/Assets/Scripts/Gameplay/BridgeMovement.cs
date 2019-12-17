using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BridgeMovement : MonoBehaviour
{
    public GameObject LeftPart;
    public GameObject RightPart;
    public float  RotSpeed;

    float MaxRot = 15;
    int LeftDirectionMultiplier = -1;
    int RightDirectionMultiplier = 1;

    private void Update()
    {
        LeftPart.transform.Rotate(Vector3.forward * RotSpeed * LeftDirectionMultiplier * Time.deltaTime);
        RightPart.transform.Rotate(Vector3.forward * RotSpeed * RightDirectionMultiplier * Time.deltaTime);

        if (RightPart.transform.localRotation.eulerAngles.z >= MaxRot || RightPart.transform.localRotation.eulerAngles.z <= 0)
            RightDirectionMultiplier *= -1;

        if (LeftPart.transform.localRotation.eulerAngles.z <= 360 - MaxRot && LeftPart.transform.localRotation.eulerAngles.z > 0)
            LeftDirectionMultiplier *= -1;
    }
}
