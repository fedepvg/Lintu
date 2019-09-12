using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    public float GravityMultiplier;
    public float HorizontalMultiplier;
    public float CameraZOffset;
    public float CameraYOffset;
    public float Bias;
    float HorizontalSpeed;
    float LastHorizontalSpeed;
    float Gravity;
    Rigidbody Rigi;
    PlayerControlsPS4 PlayerInput;
    bool IsJumping;
    Quaternion LerpBaseRot;
    float LerpT;

    // Start is called before the first frame update
    void Start()
    {
        Rigi = GetComponent<Rigidbody>();
        PlayerInput = new PlayerControlsPS4();
        PlayerInput.Enable();
        Gravity = 0f;
        IsJumping = false;
        PlayerInput.Gameplay.Horizontal.performed += ctx => HorizontalSpeed = ctx.ReadValue<float>();
        PlayerInput.Gameplay.Horizontal.canceled += ctx => HorizontalSpeed = 0f;
        LastHorizontalSpeed = 0;
        LerpT = 0;
    }

    // Update is called once per frame
    void Update()
    {
        Gravity -= GravityMultiplier * Time.deltaTime;

        if (PlayerInput.Gameplay.Jump.triggered)
        {
            Gravity = 0.5f;
            IsJumping = true;
        }

        transform.position += (transform.forward * Speed + Vector3.right * HorizontalSpeed * HorizontalMultiplier)* Time.deltaTime;
        Vector3 gravity = Vector3.zero;

        if (IsJumping)
        {
            gravity.y += Gravity;
            
            if (Gravity < 0f)
                IsJumping = false;
        }
        else
        {
            Gravity = -0.1f;
            gravity.y = Gravity;
        }
        transform.position += gravity;

        if(HorizontalSpeed < 0)
        {
            if (LastHorizontalSpeed >= 0)
            {
                LastHorizontalSpeed = HorizontalSpeed;
                LerpBaseRot = transform.rotation;
                LerpT = 0;
            }
            transform.rotation = Quaternion.Lerp(LerpBaseRot, Quaternion.identity * Quaternion.Euler(0f, 0f, 80f), LerpT);
            LerpT = Mathf.Clamp01(LerpT);
            LerpT += 2f * Time.deltaTime;
        }
        else if(HorizontalSpeed > 0)
        {
            if (LastHorizontalSpeed <= 0)
            {
                LastHorizontalSpeed = HorizontalSpeed;
                LerpBaseRot = transform.rotation;
                LerpT = 0;
            }
            transform.rotation = Quaternion.Lerp(LerpBaseRot, Quaternion.identity * Quaternion.Euler(0f, 0f, -80f), LerpT);
            LerpT = Mathf.Clamp01(LerpT);
            LerpT += 2f * Time.deltaTime;
        }
        else
        {
            if (LastHorizontalSpeed != 0)
            {
                LastHorizontalSpeed = HorizontalSpeed;
                LerpBaseRot = transform.rotation;
                LerpT = 0;
            }
            transform.rotation = Quaternion.Lerp(LerpBaseRot, Quaternion.identity * Quaternion.Euler(0f, 0f, 0f), LerpT);
            LerpT = Mathf.Clamp01(LerpT);
            LerpT += 4f * Time.deltaTime;
        }

        Camera.main.transform.position = transform.position - Vector3.forward * 10 + Vector3.up * 4;
        Camera.main.transform.LookAt(transform.position);
    }

    private void FixedUpdate()
    {
        //SetCameraPosition();
        //Rigi.AddForce(transform.forward * Speed);
        //if (PlayerInput.Gameplay.Jump.triggered)
        //{
        //    Rigi.AddForce(transform.up * JumpSpeed, ForceMode.Impulse);
        //}
    }

    void SetCameraPosition()
    {
        Vector3 CameraNewPos = transform.position - transform.forward * CameraZOffset + Vector3.up * CameraYOffset;
        Camera.main.transform.position = Camera.main.transform.position * Bias + CameraNewPos * (1f - Bias);
        Camera.main.transform.LookAt(transform.position + transform.forward * 2f);
    }
}
