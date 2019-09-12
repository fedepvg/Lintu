using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    public float Speed;
    public float JumpSpeed;
    public float GravityMultiplier;
    public float HorizontalSpeed;
    public float CameraZOffset;
    public float CameraYOffset;
    public float Bias;
    float Gravity;
    Rigidbody Rigi;
    PlayerControlsPS4 PlayerInput;
    bool IsJumping;

    // Start is called before the first frame update
    void Start()
    {
        Rigi = GetComponent<Rigidbody>();
        PlayerInput = new PlayerControlsPS4();
        PlayerInput.Enable();
        Gravity = 0f;
        IsJumping = false;
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

        transform.position += transform.forward * Speed * Time.deltaTime;
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
        //if (Gravity <= -0.05f)
        //    Gravity = -0.05f;
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
