using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpaceShip : ShipBase
{
    private Rigidbody rigi;
    public float SpeedZ;
    public float PitchRate;
    public float RollRate;
    public float YawRate;
    float InputPitch;
    float InputRoll;
    float InputYaw;
    float Throttle;
    bool IsFlying;
    public float CameraZOffset;
    public float CameraYOffset;
    float LastRoll = 0;
    public float Multiplier = 1;

    void Awake()
    {
        rigi = GetComponentInParent<Rigidbody>();
        Physics.gravity = new Vector3(0f,-60f, 0f);
        Health = 200;
        Cursor.visible = false;
    }

    private void FixedUpdate()
    {
        if (Health > 0)
        {
            //GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);

            rigi.AddRelativeForce(new Vector3(0, 0, SpeedZ * Throttle), ForceMode.Force);
            SetCameraPosition();
        }
    }

    private void Update()
    {
        if (Health <= 0)
        {
            //CameraManager.Instance.SwitchToExplosionCamera(transform.position);
            //Explode();
            //GameManager.Instance.GameOver();
        }
        else
        {
            GetInput(ref InputPitch, ref InputRoll, ref InputYaw, ref Throttle);
            transform.Rotate(InputPitch * PitchRate * Time.deltaTime, InputYaw * YawRate * Time.deltaTime, InputRoll * RollRate * Time.deltaTime, Space.Self);
            //Gun.Attack();
        }
        Debug.Log(rigi.velocity.magnitude);
    }

    void SetCameraPosition()
    {
        Vector3 CameraNewPos = transform.position - transform.forward * CameraZOffset + Vector3.up * CameraYOffset;
        Camera.main.transform.position = Camera.main.transform.position * 0.96f + CameraNewPos * 0.04f;
        Camera.main.transform.LookAt(transform.position /*+ transform.forward * 30f*/);
    }

    void GetInput(ref float InputPitch, ref float InputRoll, ref float InputYaw, ref float Throttle)
    {
        Vector3 mousePos = Input.mousePosition;
        if (IsFlying)
        {
            //Vector3 mousePos = Input.mousePosition;

            InputPitch = (mousePos.y - (Screen.height * 0.5f)) / (Screen.height * 0.5f);
            //InputRoll = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);

            InputPitch = -Mathf.Clamp(InputPitch, -1.0f, 1.0f);
            //InputRoll = Mathf.Clamp(InputRoll, -1.0f, 1.0f);
        }

        InputRoll = Input.GetAxis("Mouse X");
        InputYaw = Input.GetAxis("Horizontal");
        
        if(InputRoll!=0)
        {
            InputRoll = (mousePos.x - (Screen.width * 0.5f)) / (Screen.width * 0.5f);
            InputRoll = Mathf.Clamp(InputRoll, -1.0f, 1.0f);
        }

        bool AccInput;
        float Target = Throttle;
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            Target = 1.0f;
            AccInput = true;
        }
        else
        { 
            Target = 0f;
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
                AccInput = true;
            else
                AccInput = false;
        }
        

        if (AccInput)
        {
            Throttle = Mathf.MoveTowards(Throttle, Target, Time.deltaTime * 0.05f);
        }
        else
        {
            Throttle = Mathf.MoveTowards(Throttle, Target, Time.deltaTime * 0.1f);
        }
        
    }    

    private void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer) == "Floor" || LayerMask.LayerToName(other.gameObject.layer) == "Water")
        {
            Health = 0;
        }

        if(other.tag=="EnemyBullet")
        {
            Health -= 50;
        }

        if(other.tag=="Enemy")
        {
            Health = 0;
        }
        Debug.Log("Health: " + Health);
    }

    private void OnCollisionEnter(Collision collision)
    {
        IsFlying = false;
        rigi.useGravity = false;
    }

    private void OnCollisionExit(Collision collision)
    {
        rigi.useGravity = true;
        IsFlying = true;
    }

    public int GetHealth()
    {
        return Health;
    }
}
