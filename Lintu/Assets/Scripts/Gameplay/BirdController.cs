using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    #region PublicVariables
    public float Speed;
    public float HorizontalSpeed;
    public float BaseGravity;
    public float CameraZOffset;
    public float CameraYOffset;
    public float MinXRotation;
    public float MaxXRotation;
    public float MinZRotation;
    public float MaxZRotation;
    public float RotationSpeed;
    public float DownRotationCoefficient;
    public float MiddleRotationCoefficient;
    public float UpRotationCoefficient;
    public float JumpSpeed;
    public float MaxEnergy;
    public float MaxSpeedMultiplier;
    public float MinSpeedMultiplier;
    public Animator AnimatonController;
    public AnimationCurve JumpCurve;
    public Slider EnergyBar;
    public Image EnergyBarFill;
    public LayerMask FloorRaycastLayer;
    public Text FloorDistanceText;
    public GameObject []BlobShadows;
    public SceneLoader SceneManagement;
    public float FloorDistance;
    public float Energy;
    public float JumpEnergy;
    public float EnergyLossCoefficient;
    #endregion

    #region PrivateVariables
    Rigidbody Rigi;
    FlyingControls PlayerInput;
    bool IsJumping;
    float XAxisFrameRotation;
    float ZAxisFrameRotation;
    float XAxisRotation;
    float ZAxisRotation;
    Quaternion DestRotation;
    float SpeedMultiplier;
    float Gravity;
    float JumpTimer;
    float JumpGravity;
    const float FloorRayDistance = 300f;
    float rotationCoefficient;
    #endregion

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

        SpeedMultiplier = 0.8f;
        Energy = MaxEnergy;

        OrbBehaviour.OnOrbPickup = AddEnergy;
    }

    // Update is called once per frame
    void Update()
    {
        #region Jump
        if (PlayerInput.Gameplay.Jump.triggered && !IsJumping)
        {
            IsJumping = true;
            AnimatonController.SetTrigger("Fly");
        }

        if (IsJumping)
        {
            JumpGravity = JumpSpeed * JumpCurve.Evaluate(JumpTimer);
            JumpTimer = Mathf.Clamp01(JumpTimer += Time.deltaTime * 2f);

            if (JumpTimer >= 1)
                IsJumping = false;

            JumpEnergy = JumpGravity + 1;
        }
        else
        {
            JumpTimer = 0;
            JumpGravity = 1f;
            JumpEnergy = 1f;
        }
        #endregion

        #region Rotation
        XAxisRotation += XAxisFrameRotation * RotationSpeed * Time.deltaTime;
        ZAxisRotation += ZAxisFrameRotation * RotationSpeed * Time.deltaTime;

        XAxisRotation = Mathf.Clamp(XAxisRotation, MinXRotation, MaxXRotation);
        ZAxisRotation = Mathf.Clamp(ZAxisRotation, MinZRotation, MaxZRotation);

        DestRotation = Quaternion.Euler(XAxisRotation, 0f, ZAxisRotation);
        #endregion

        #region MovementCalculations
        if (XAxisRotation < 4f)
        {
            rotationCoefficient = DownRotationCoefficient;
            if (XAxisRotation > 0)
                rotationCoefficient = MiddleRotationCoefficient;
        }
        else
            rotationCoefficient = UpRotationCoefficient;

        SpeedMultiplier += (SpeedMultiplier * rotationCoefficient * XAxisRotation) * Time.deltaTime;

        SpeedMultiplier = Mathf.Clamp(SpeedMultiplier, MinSpeedMultiplier, MaxSpeedMultiplier);

        Vector3 horizontalMovement = new Vector3
        {
            x = -ZAxisRotation * HorizontalSpeed * Time.deltaTime
        };

        Gravity = BaseGravity / SpeedMultiplier;
        #endregion

        //ENERGY-------------------------------------
        Energy -= EnergyLossCoefficient  * JumpEnergy * Time.deltaTime;
        Energy = Mathf.Clamp(Energy, 0, MaxEnergy);

        //FLOOR DISTANCE-------------------------------
        RaycastHit hit;
        string layerHitted;
        if (Physics.Raycast(transform.position, - Vector3.up, out hit, FloorRayDistance, FloorRaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Floor")
            {
                FloorDistance = hit.distance;
            }
        }

        UpdateBlobShadowPosition();
    }

    void FixedUpdate()
    {
        Rigi.velocity = transform.forward * Speed * SpeedMultiplier * JumpGravity;
        Rigi.velocity += new Vector3(0f, Gravity, 0f);
        Rigi.velocity += Vector3.right * -ZAxisRotation * HorizontalSpeed * Time.fixedDeltaTime;
        Rigi.MoveRotation(DestRotation);
    }

    void UpdateBlobShadowPosition()
    {
        for (int i = 0; i < BlobShadows.Length; i++)
        {
            BlobShadows[i].transform.position = transform.position;
        }
    }

    void AddEnergy(int energy)
    {
        Energy += energy;
        if (Energy >= MaxEnergy)
            Energy = MaxEnergy;
    }

    void OnCollisionEnter(Collision collision)
    {
        SceneManagement.LoadGOScene();
    }
}
