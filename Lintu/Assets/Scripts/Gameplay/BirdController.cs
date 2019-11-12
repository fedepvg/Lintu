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
    public LayerMask FloorRaycastLayer;
    public GameObject []BlobShadows;
    public SceneLoader SceneManagement;
    public float FloorDistance;
    public float Energy;
    public float JumpEnergy;
    public float EnergyLossCoefficient;
    public PlayerControls PlayerInput;
    public delegate void OnEndLevel();
    public static OnEndLevel EndLevelAction;
    #endregion

    #region PrivateVariables
    Rigidbody Rigi;
    bool IsJumping;
    float XAxisFrameRotation;
    float ZAxisFrameRotation;
    float XAxisRotation;
    float ZAxisRotation;
    Quaternion DestRotation;
    float SpeedMultiplier;
    public float Gravity;
    float JumpTimer;
    float JumpGravity;
    const float FloorRayDistance = 300f;
    float rotationCoefficient;
    bool OffLeftLimit = false;
    bool OffRightLimit = false;
    bool EndedLevel = false;
    float OffLimitsSpeed;
    #endregion

    void Start()
    {
        Rigi = GetComponent<Rigidbody>();

        PlayerInput = GameManager.Instance.GameInput;
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
        if (PlayerInput.Gameplay.Jump.triggered && !IsJumping && Energy > EnergyLossCoefficient)
        {
            IsJumping = true;
            AnimatonController.SetTrigger("Fly");
        }

        if (IsJumping)
        {
            JumpGravity = JumpSpeed * JumpCurve.Evaluate(JumpTimer);
            JumpTimer = Mathf.Clamp01(JumpTimer += Time.deltaTime);

            if (JumpTimer >= 0.9f)
                IsJumping = false;

            JumpEnergy = JumpGravity / 2;
        }
        else
        {
            JumpTimer = 0;
            JumpGravity = 0f;
            JumpEnergy = 1f;
        }
        #endregion

        #region Rotation
        if (OffLeftLimit)
            ZAxisFrameRotation = -3;
        else if (OffRightLimit)
            ZAxisFrameRotation = 3;
        if (EndedLevel)
            XAxisFrameRotation = -2;

        XAxisRotation += XAxisFrameRotation * RotationSpeed * Time.deltaTime;
        ZAxisRotation += ZAxisFrameRotation * RotationSpeed * Time.deltaTime;

        XAxisRotation = Mathf.Clamp(XAxisRotation, MinXRotation, MaxXRotation);

        if(OffLeftLimit)
            ZAxisRotation = Mathf.Clamp(ZAxisRotation, 0, MaxZRotation);
        else if(OffRightLimit)
            ZAxisRotation = Mathf.Clamp(ZAxisRotation, MinZRotation, 0);
        else
            ZAxisRotation = Mathf.Clamp(ZAxisRotation, MinZRotation, MaxZRotation);

        if (ZAxisRotation == 0)
        {
            OffRightLimit = false;
            OffLeftLimit = false;
        }

        DestRotation = Quaternion.Euler(XAxisRotation, 0f, ZAxisRotation);
        #endregion

        #region MovementCalculations
        if (XAxisRotation < 4f && Energy > 0)
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

        Gravity = BaseGravity / SpeedMultiplier + JumpGravity;
        #endregion

        #region EnergyCalculation
        Energy -= EnergyLossCoefficient  * JumpEnergy * Time.deltaTime;
        Energy = Mathf.Clamp(Energy, 0, MaxEnergy);
        #endregion

        #region FloorDistance
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
        #endregion

        UpdateBlobShadowPosition();
    }

    void FixedUpdate()
    {
        Rigi.velocity = transform.forward * Speed * SpeedMultiplier;// * JumpGravity;
        Rigi.velocity += new Vector3(0f, Gravity, 0f);
        if (!OffLeftLimit && !OffRightLimit)
            Rigi.velocity += Vector3.right * -ZAxisRotation * HorizontalSpeed * Time.fixedDeltaTime;
        else
            Rigi.velocity += Vector3.right * OffLimitsSpeed * Time.fixedDeltaTime;
        Rigi.MoveRotation(DestRotation);
    }

    void UpdateBlobShadowPosition()
    {
        BlobShadows[0].transform.forward = transform.forward;
        BlobShadows[0].transform.position = transform.position;
        BlobShadows[1].transform.position = transform.position;
        //for (int i = 0; i < BlobShadows.Length; i++)
        //{
        //    BlobShadows[i].transform.position = transform.position;
        //}
    }

    void AddEnergy(int energy)
    {
        Energy += energy;
        if (Energy >= MaxEnergy)
            Energy = MaxEnergy;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall" || collision.gameObject.tag == "Obstacle" || collision.gameObject.tag == "Floor")
            SceneManagement.LoadGOScene(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CityLimit")
        {
            if (Rigi.velocity.x > 0)
            {
                OffRightLimit = true;
                OffLimitsSpeed = -200;
            }
            else
            {
                OffLeftLimit = true;
                OffLimitsSpeed = 200;
            }
        }

        if (other.tag == "Finish")
        {
            StartCoroutine(EndLevel());
            //SceneManagement.LoadNextScene(true);
            //Destroy(this);
        }
    }

    IEnumerator EndLevel()
    {
        PlayerInput.Disable();
        EndedLevel = true;
        BaseGravity = 0;
        if(EndLevelAction!=null)
            EndLevelAction();

        yield return new WaitForSeconds(2);

        SceneManagement.LoadNextScene(true);
        Destroy(this);
    }
}
