using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float CameraYOffset;
    public float CameraZOffset;
    public float CameraMaxYOffset;
    public float CameraMiddleYOffset;
    public float CameraMinYOffset;
    public Transform PlayerTransform;
    public float CameraYSpeedMultiplier;
    public CameraPositions CameraPosition;

    CameraPositions CameraLastposition;
    float CameraYSpeed;
    bool LevelEnded = false;


    public enum CameraPositions
    {
        up, middle, down
    }

    private void Start()
    {
        CameraPosition = CameraPositions.middle;
        BirdController.EndLevelAction = StopMovingCamera;

    }

    void Update()
    {
        if (!LevelEnded)
        {
            CameraYSpeed = Time.deltaTime;
            switch (CameraPosition)
            {
                case CameraPositions.up:
                    CameraYOffset = Mathf.MoveTowards(CameraYOffset, CameraMaxYOffset, CameraYSpeed);
                    break;
                case CameraPositions.middle:
                    CameraYOffset = Mathf.MoveTowards(CameraYOffset, CameraMiddleYOffset, CameraYSpeed);
                    break;
                case CameraPositions.down:
                    CameraYOffset = Mathf.MoveTowards(CameraYOffset, CameraMinYOffset, CameraYSpeed);
                    break;
            }
            CameraYOffset = Mathf.Clamp(CameraYOffset, CameraMinYOffset, CameraMaxYOffset);

            Vector3 NewCameraPosition = PlayerTransform.position - Vector3.forward * CameraZOffset + Vector3.up * CameraYOffset;
            NewCameraPosition.x = PlayerTransform.position.x;
            transform.position = NewCameraPosition;
            transform.LookAt(PlayerTransform.position + Vector3.up * CameraYOffset);
        }
    }

    public void SetCameraNextPosition(CameraPositions newPos)
    {
        CameraLastposition = CameraPosition;
        CameraPosition = newPos;
    }

    public void StopMovingCamera()
    {
        LevelEnded = true;
    }
}
