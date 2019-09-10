using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class SpaceShip : ShipBase
{
    PlayerControlsPS4 PlayerInput;
    private Rigidbody rigi;
    public float SpeedZ;
    public float PitchRate;
    public float RollRate;
    public float YawRate;
    float InputPitch;
    float InputRoll;
    float InputYaw;
    float Throttle;
    public float CameraZOffset;
    public float CameraYOffset;
    float AccMultiplier;
    Vector2 Rot;
    bool Accelerate;
    bool Dashes;
    public float Bias;

    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        Physics.gravity = new Vector3(0f, -20f, 0f);
        Health = 200;
        Cursor.visible = false;

        PlayerInput = new PlayerControlsPS4();
        PlayerInput.Enable();

        PlayerInput.Gameplay.LeftRoll.performed += ctx => InputRoll = -1f;
        PlayerInput.Gameplay.LeftRoll.canceled += ctx => InputRoll = InputRoll != 1 ? 0 : 1;


        PlayerInput.Gameplay.RightRoll.performed += ctx => InputRoll = 1f;
        PlayerInput.Gameplay.RightRoll.canceled += ctx => InputRoll = InputRoll != -1 ? 0 : -1;

        PlayerInput.Gameplay.Rotate.performed += ctx => InputYaw = ctx.ReadValue<Vector2>().x;
        PlayerInput.Gameplay.Rotate.canceled += ctx => InputYaw = 0f;

        PlayerInput.Gameplay.Rotate.performed += ctx => InputPitch = ctx.ReadValue<Vector2>().y;
        PlayerInput.Gameplay.Rotate.canceled += ctx => InputPitch = 0f;


        PlayerInput.Gameplay.Accelerate.performed += ctx => Accelerate = true;
        PlayerInput.Gameplay.Accelerate.canceled += ctx => Accelerate = false;

        PlayerInput.Gameplay.Dash.performed += ctx => Dashes = true;
        PlayerInput.Gameplay.Dash.canceled += ctx => Dashes = false;
    }

    private void FixedUpdate()
    {
        if (Health > 0)
        {
            rigi.AddRelativeForce(new Vector3(0, 0, SpeedZ * Throttle), ForceMode.Force);
            SetCameraPosition();
        }
    }

    private void Update()
    {
        if (Health <= 0)
        {

        }
        else
        {
            GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);
            transform.Rotate(InputPitch * PitchRate * Time.deltaTime, InputYaw * YawRate * Time.deltaTime, InputRoll * RollRate * Time.deltaTime, Space.Self);
        }
        Debug.Log(rigi.velocity.magnitude);
    }

    void SetCameraPosition()
    {
        Vector3 CameraNewPos = transform.position - transform.forward * CameraZOffset + Vector3.up * CameraYOffset;
        Camera.main.transform.position = Camera.main.transform.position * Bias + CameraNewPos * (1f - Bias);
        Camera.main.transform.LookAt(transform.position + transform.forward * 2f);
    }

    void GetInput(ref float InputPitch, ref float InputRoll, ref float InputYaw, ref float Throttle)
    {
        Vector3 mousePos = Input.mousePosition;

        bool AccInput = false;
        float Target = Throttle;
        if (Accelerate && !Dashes)
        {
            Target = 1.0f;
            AccMultiplier = 0.2f;
            AccInput = true;
        }
        else if (Dashes)
        {
            Target = 2.0f;
            AccMultiplier = 0.5f;
        }
        else
        {
            Target = 0f;
            AccInput = false;
            AccMultiplier = 0.3f;
        }


        if (AccInput)
        {
            Throttle = Mathf.MoveTowards(Throttle, Target, Time.deltaTime * AccMultiplier);
        }
        else
        {
            Throttle = Mathf.MoveTowards(Throttle, Target, Time.deltaTime * AccMultiplier);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Floor" || LayerMask.LayerToName(other.gameObject.layer) == "Water")
        {
            Health = 0;
        }

        if (other.tag == "EnemyBullet")
        {
            Health -= 50;
        }

        if (other.tag == "Enemy")
        {
            Health = 0;
        }
        Debug.Log("Health: " + Health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Physics.gravity = new Vector3(0f, 0f, 0f);
        //rigi.useGravity = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        //Physics.gravity = new Vector3(0f, -20f, 0f);
        //IsFlying = true;
    }

    public int GetHealth()
    {
        return Health;
    }
}
