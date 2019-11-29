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

    float CameraYSpeed;
    float FovDifference;
    bool LevelEnded = false;

    private void Start()
    {
        BirdController.EndLevelAction = StopMovingCamera;
        BirdController.OnPlayerMovingAction = SetCameraFov;
        FovDifference = MaxFov - MinFov;
    }

    void Update()
    {
        if (!LevelEnded)
        {
            CameraYSpeed = Time.deltaTime;
            CameraYOffset = Mathf.MoveTowards(CameraYOffset, CameraMaxYOffset, CameraYSpeed);
        }

        transform.LookAt(PlayerTransform.position, Vector3.up);
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
    }

    public void SetCameraFov(float porc)
    {
        float newFov = FovDifference * porc;
        GetComponent<Camera>().fieldOfView = MinFov + newFov;
    }
}
