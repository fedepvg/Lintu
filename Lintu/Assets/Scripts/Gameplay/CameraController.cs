using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraYOffset;
    public float CameraMaxYOffset;
    public Transform PlayerTransform;
    public float CameraYSpeedMultiplier;
    public Vector3 CurrentVelocity;
    public float SmoothTime;
    public Vector3 Distance;
    public float MaxFov;
    public float MinFov;
    public GameObject FarPlaneFog;

    float CameraYSpeed;
    float FovDifference;
    bool LevelEnded = false;
    GameObject FarPlaneParticleInstance;

    private void Start()
    {
        BirdController.EndLevelAction += StopMovingCamera;
        BirdController.OnPlayerMovingAction = SetCameraFov;
        FovDifference = MaxFov - MinFov;
        FarPlaneParticleInstance = Instantiate(FarPlaneFog, new Vector3(0f, 0f, transform.position.z + GetComponent<Camera>().farClipPlane), FarPlaneFog.transform.rotation);

    }

    void Update()
    {
        if (!LevelEnded)
        {
            CameraYSpeed = Time.deltaTime;
            CameraYOffset = Mathf.MoveTowards(CameraYOffset, CameraMaxYOffset, CameraYSpeed);
        }

        transform.LookAt(PlayerTransform.position, Vector3.up);
        FarPlaneParticleInstance.transform.position = Vector3.forward * (transform.position.z + GetComponent<Camera>().farClipPlane);
    }

    private void FixedUpdate()
    {
        if (!LevelEnded)
        {
            Vector3 targetPos = PlayerTransform.position + (PlayerTransform.rotation * Distance);
            float NewCameraYPosition = Mathf.SmoothDamp(transform.position.y, targetPos.y, ref CurrentVelocity.y, SmoothTime);
            float NewCameraZPosition = Mathf.SmoothDamp(transform.position.z, targetPos.z, ref CurrentVelocity.z, SmoothTime);

            transform.position = new Vector3(PlayerTransform.position.x, PlayerTransform.position.y, NewCameraZPosition);
        }
    }

    public void StopMovingCamera()
    {
        LevelEnded = true;
        BirdController.EndLevelAction -= StopMovingCamera;
    }

    public void SetCameraFov(float porc)
    {
        float newFov = FovDifference * porc;
        GetComponent<Camera>().fieldOfView = MinFov + newFov;
    }
}
