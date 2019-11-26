﻿using System.Collections;
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
    public Animator AnimationController;
    public AnimationCurve JumpCurve;
    public LayerMask LevelRaycastLayer;
    public GameObject BlobShadow;
    public SceneLoader SceneManagement;
    public float LevelDistanceLeft;
    public float Energy;
    public float JumpEnergy;
    public float EnergyLossCoefficient;
    public PlayerControls PlayerInput;
    public delegate void OnEndLevel();
    public static OnEndLevel EndLevelAction;
    public float TimeToEndLevel;
    public float TimeToGameOverScreen;
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
    float Gravity;
    float JumpTimer;
    float JumpGravity;
    const float LevelRayDistance = 2000f;
    float RotationCoefficient;
    bool OffLeftLimit = false;
    bool OffRightLimit = false;
    bool EndedLevel = false;
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
            AnimationController.SetTrigger("Fly");
            AkSoundEngine.PostEvent("Pajaro_Aletea", gameObject);
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
        {
            if (ZAxisFrameRotation > 0)
                ZAxisFrameRotation = 0;
            ZAxisFrameRotation += -6 * Time.deltaTime;
        }
        else if (OffRightLimit)
        {
            if (ZAxisFrameRotation < 0)
                ZAxisFrameRotation = 0;
            ZAxisFrameRotation += 6 * Time.deltaTime;
        }
        if (EndedLevel)
            XAxisFrameRotation = -2;

        XAxisRotation += XAxisFrameRotation * RotationSpeed * Time.deltaTime;
        ZAxisRotation += ZAxisFrameRotation * RotationSpeed * Time.deltaTime;

        XAxisRotation = Mathf.Clamp(XAxisRotation, MinXRotation, MaxXRotation);

        ZAxisRotation = Mathf.Clamp(ZAxisRotation, MinZRotation, MaxZRotation);

        if (ZAxisRotation >= MaxZRotation && OffRightLimit)
        {
            PlayerInput.Gameplay.Horizontal.Enable();
            OffRightLimit = false;
        }
        else if (ZAxisRotation <= MinZRotation && OffLeftLimit)
        {
            PlayerInput.Gameplay.Horizontal.Enable();
            OffLeftLimit = false;
        }

        DestRotation = Quaternion.Euler(XAxisRotation, 0f, ZAxisRotation);
        #endregion

        #region MovementCalculations
        if (XAxisRotation < 4f && Energy > 0)
        {
            RotationCoefficient = DownRotationCoefficient;
            if (XAxisRotation > 0)
                RotationCoefficient = MiddleRotationCoefficient;
            AnimationController.SetBool("GoingDown", false);
        }
        else
        {
            RotationCoefficient = UpRotationCoefficient;
            if (XAxisRotation >= 4f)
                AnimationController.SetBool("GoingDown", true);
            else
                AnimationController.SetBool("GoingDown", false);
        }

        SpeedMultiplier += (SpeedMultiplier * RotationCoefficient * XAxisRotation) * Time.deltaTime;

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

        #region LevelDistance
        RaycastHit hit;
        string layerHitted;
        if (Physics.Raycast(transform.position, Vector3.forward, out hit, LevelRayDistance, LevelRaycastLayer))
        {
            layerHitted = LayerMask.LayerToName(hit.transform.gameObject.layer);

            if (layerHitted == "Finish")
            {
                LevelDistanceLeft = hit.distance;
            }
        }
        #endregion

        UpdateBlobShadowPosition();
    }

    void FixedUpdate()
    {
        Rigi.velocity = transform.forward * Speed * SpeedMultiplier;
        Rigi.velocity += new Vector3(0f, Gravity, 0f);
        Rigi.velocity += Vector3.right * -ZAxisRotation * HorizontalSpeed * Time.fixedDeltaTime;
        Rigi.MoveRotation(DestRotation);
    }

    void UpdateBlobShadowPosition()
    {
        BlobShadow.transform.position = transform.position;
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
        {
            Die(TimeToGameOverScreen);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CityLimit")
        {
            if (Rigi.velocity.x > 0)
            {
                OffRightLimit = true;
                PlayerInput.Gameplay.Horizontal.Disable();
            }
            else
            {
                OffLeftLimit = true;
                PlayerInput.Gameplay.Horizontal.Disable();
            }
        }

        if (other.tag == "Finish")
        {
            StartCoroutine(EndLevel(TimeToEndLevel));
        }
    }

    void Die(float t)
    {
        AkSoundEngine.PostEvent("Perder", gameObject);
        AnimationController.SetTrigger("Death");
        if (EndLevelAction != null)
            EndLevelAction();
        PlayerInput.Disable();
        Rigi.velocity /= 2;
        Rigi.useGravity = true;
        Rigi.mass = 100;
        Rigi.freezeRotation = true;
        SceneManagement.LoadGOScene(t);
        Destroy(this);
    }

    IEnumerator EndLevel(float t)
    {
        PlayerInput.Disable();
        EndedLevel = true;
        BaseGravity = 0;
        if(EndLevelAction!=null)
            EndLevelAction();

        yield return new WaitForSeconds(t);

        SceneManagement.LoadNextScene(true);
        Destroy(this);
    }
}
