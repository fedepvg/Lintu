using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WingSuitController : MonoBehaviour
{
    public float Speed;
    Rigidbody Rigi;
    FlyingControls PlayerInput;
    public float Gravity;
    bool IsJumping;
    public float CameraZOffset;
    public float CameraYOffset;
    float XAxisFrameRotation;
    float ZAxisFrameRotation;
    float XAxisRotation;
    float ZAxisRotation;
    public float MinXRotation;
    public float MaxXRotation;
    public float MinZRotation;
    public float MaxZRotation;
    Quaternion DestRotation;
    Vector3 DestPosition;
    float SpeedMultiplier;
    public float RotationSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Rigi = GetComponent<Rigidbody>();
        PlayerInput = new FlyingControls();
        PlayerInput.Enable();
        IsJumping = false;
        XAxisRotation = 0f;
        ZAxisRotation = 0f;
        PlayerInput.Gameplay.Horizontal.performed += ctx => ZAxisFrameRotation = ctx.ReadValue<float>();
        PlayerInput.Gameplay.Horizontal.canceled += ctx => ZAxisFrameRotation = 0f;
        PlayerInput.Gameplay.Vertical.performed += ctx => XAxisFrameRotation = ctx.ReadValue<float>();
        PlayerInput.Gameplay.Vertical.canceled += ctx => XAxisFrameRotation = 0f;
        DestRotation = Quaternion.identity;
        DestPosition = transform.position;
        SpeedMultiplier = 0.9f;
    }

    // Update is called once per frame
    void Update()
    {
        XAxisRotation += XAxisFrameRotation * RotationSpeed * Time.deltaTime;
        ZAxisRotation += ZAxisFrameRotation * RotationSpeed * Time.deltaTime;
        if(XAxisRotation < MinXRotation)
        {
            XAxisRotation = MinXRotation;
        }
        if (XAxisRotation > MaxXRotation)
        {
            XAxisRotation = MaxXRotation;
        }
        if (ZAxisRotation < MinZRotation)
        {   
            ZAxisRotation = MinZRotation;
        }   
        if (ZAxisRotation > MaxZRotation)
        {   
            ZAxisRotation = MaxZRotation;
        }
        DestRotation = Quaternion.Euler(XAxisRotation, 0f, ZAxisRotation);

        if (XAxisRotation < -1f || XAxisRotation > 1f)
        {
            SpeedMultiplier = XAxisRotation / MaxXRotation * 1.5f;
            SpeedMultiplier += 2f;
        }
        else
        {
            SpeedMultiplier = 1f;
        }

        DestPosition += transform.forward * Speed * SpeedMultiplier * Time.deltaTime;
        DestPosition += new Vector3(0f, Gravity, 0f);

        Camera.main.transform.position = transform.position - Vector3.forward * CameraZOffset + Vector3.up * CameraYOffset;
        Camera.main.transform.LookAt(transform.position);
    }

    private void FixedUpdate()
    {
        Rigi.MovePosition(DestPosition);
        Rigi.MoveRotation(DestRotation);
    }
}
