using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WingSuitController : MonoBehaviour
{
    public float Speed;
    Rigidbody Rigi;
    FlyingControls PlayerInput;
    public float BaseGravity;
    float Gravity;
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
    public float HorizontalSpeed;
    float MaxSpeedMultiplier = 2f;
    float MinSpeedMultiplier = 0.05f;
    public Animator AnimatonController;
    public float JumpSpeed;
    public AnimationCurve JumpCurve;
    float JumpTimer;
    float JumpGravity;
    public float MaxEnergy;
    float Energy;
    float JumpEnergy = 20;
    public Slider EnergyBar;
    public Image EnergyBarFill;
    public LayerMask RaycastLayer;
    const float RayDistance = 300f;
    float FloorDistance;
    public Text FloorDistanceText;
    float FinalSpeed;

    // Start is called before the first frame update
    void Start()
    {
        Rigi = GetComponent<Rigidbody>();
        PlayerInput = new FlyingControls();
        PlayerInput.Enable();
        IsJumping = false;
        XAxisRotation = 0f;
        ZAxisRotation = 0f;
        PlayerInput.Gameplay.Horizontal.performed += ctx => ZAxisFrameRotation = -ctx.ReadValue<float>();
        PlayerInput.Gameplay.Horizontal.canceled += ctx => ZAxisFrameRotation = 0f;
        PlayerInput.Gameplay.Vertical.performed += ctx => XAxisFrameRotation = ctx.ReadValue<float>();
        PlayerInput.Gameplay.Vertical.canceled += ctx => XAxisFrameRotation = 0f;
        DestRotation = Quaternion.identity;
        DestPosition = transform.position;
        SpeedMultiplier = 0.8f;
        Energy = MaxEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Gameplay.Jump.triggered && !IsJumping && Energy > JumpEnergy)
        {
            IsJumping = true;
            AnimatonController.SetTrigger("Fly");
            Energy -= JumpEnergy;
        }

        if (IsJumping)
        {
            JumpGravity = JumpSpeed * JumpCurve.Evaluate(JumpTimer);
            JumpTimer = Mathf.Clamp01(JumpTimer += Time.deltaTime * 2f);

            if (JumpTimer >= 1)
                IsJumping = false;
        }
        else
        {
            JumpTimer = 0;
            JumpGravity = 0f;
        }

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

        float speedCoefficient = 0.05f;

        if (XAxisRotation < 4f)
        {
            speedCoefficient = 0.03f;
            if (XAxisRotation > 0)
                speedCoefficient = -0.01f;
        }

        SpeedMultiplier += (SpeedMultiplier * speedCoefficient * XAxisRotation) * Time.deltaTime;

        SpeedMultiplier = Mathf.Clamp(SpeedMultiplier, MinSpeedMultiplier, MaxSpeedMultiplier);

        Vector3 horizontalMovement = new Vector3
        {
            x = -ZAxisRotation * HorizontalSpeed * Time.deltaTime
        };

        Gravity = BaseGravity / SpeedMultiplier + JumpGravity;
        
        DestPosition += transform.forward * Speed * SpeedMultiplier * Time.deltaTime;
        DestPosition += horizontalMovement;
        DestPosition += new Vector3(0f, Gravity, 0f);

        Vector3 NewCam = transform.position - Vector3.forward * CameraZOffset + transform.up * CameraYOffset;
        NewCam.x = transform.position.x;
        Camera.main.transform.position = NewCam;
        Camera.main.transform.LookAt(transform.position);

        //ENERGY-------------------------------------
        Energy += 10 * Time.deltaTime;
        Energy = Mathf.Clamp(Energy, 0, MaxEnergy);
        EnergyBar.value = Energy;
        if (Energy < 20)
            EnergyBarFill.color = Color.red;
        else
            EnergyBarFill.color = Color.white;
        //FLOOR DISTANCE-------------------------------
        RaycastHit hit;
        string layerHitted;
        if (Physics.Raycast(transform.position, - Vector3.up, out hit, RayDistance, RaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Floor")
            {
                FloorDistance = hit.distance;
            }
        }
        FloorDistanceText.text = FloorDistance.ToString("F2") + " mts.";
    }

    private void FixedUpdate()
    {
        Rigi.MovePosition(DestPosition);
        Rigi.MoveRotation(DestRotation);
    }

    private void OnCollisionEnter(Collision collision)
    {
        GameManager.RestartLevel();
    }
}
