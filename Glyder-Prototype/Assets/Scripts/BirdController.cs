using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    Vector3 GravityVec;
    Rigidbody Rigi;
    PlayerControlsPS4 PlayerInput;
    bool IsJumping;
    Quaternion LerpBaseRot;
    float RotLerpT;
    public float TurboMultiplier;
    public AnimationCurve TurboCurve;
    public float TurboTimer;
    bool TurboActivated;
    public Animator AnimatonController;
    public AnimationCurve JumpCurve;
    float JumpTimer;
    Quaternion TurboBaseRot;
    Quaternion TurboDestRot;
    float TurboLerpT;
    Quaternion TempRotation;

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
        PlayerInput.Gameplay.Turbo.performed += ctx => ActivateTurbo();
        PlayerInput.Gameplay.Turbo.canceled += ctx => DeactivateTurbo();
        LastHorizontalSpeed = 0;
        RotLerpT = 0.1f;
        TurboMultiplier = 1f;
        TurboTimer = 0f;
        TurboActivated = false;
        TurboLerpT = 0.1f;
        TurboBaseRot = Quaternion.identity;
        TurboDestRot = Quaternion.identity;
        TempRotation = Quaternion.identity;
    }

    // Update is called once per frame
    void Update()
    {
        if (PlayerInput.Gameplay.Jump.triggered && !IsJumping && !TurboActivated)
        {
            IsJumping = true;
            AnimatonController.SetTrigger("Fly");
        }

        if (IsJumping)
        {
            Gravity = JumpSpeed * JumpCurve.Evaluate(JumpTimer);
            JumpTimer = Mathf.Clamp01(JumpTimer += Time.deltaTime * 2f);
            
            if (JumpTimer >= 1)
                IsJumping = false;
        }
        else
        {
            JumpTimer = 0;
            Gravity = -0.5f;
        }

        if (TurboActivated)
            AccelerateTurbo();
        else if (TurboMultiplier > 1f)
            DeaccelerateTurbo();
        else
            TurboMultiplier = 1f;

        Gravity *= TurboMultiplier * TurboMultiplier * TurboMultiplier;
        GravityVec.y = Gravity;

        if (HorizontalSpeed < 0)
        {
            if (LastHorizontalSpeed >= 0)
            {
                LastHorizontalSpeed = HorizontalSpeed;
                LerpBaseRot = transform.rotation;
                RotLerpT = 0;
            }
            TempRotation = Quaternion.Lerp(LerpBaseRot, Quaternion.identity * Quaternion.Euler(0f, 0f, 40f), RotLerpT);
            RotLerpT = Mathf.Clamp01(RotLerpT);
            RotLerpT += 3f * Time.deltaTime;
        }
        else if(HorizontalSpeed > 0)
        {
            if (LastHorizontalSpeed <= 0)
            {
                LastHorizontalSpeed = HorizontalSpeed;
                LerpBaseRot = transform.rotation;
                RotLerpT = 0;
            }
            TempRotation = Quaternion.Lerp(LerpBaseRot, Quaternion.identity * Quaternion.Euler(0f, 0f, -40f), RotLerpT);
            RotLerpT = Mathf.Clamp01(RotLerpT);
            RotLerpT += 3f * Time.deltaTime;
        }
        else
        {
            if (LastHorizontalSpeed != 0)
            {
                LastHorizontalSpeed = HorizontalSpeed;
                LerpBaseRot = transform.rotation;
                RotLerpT = 0;
            }
            TempRotation = Quaternion.Lerp(LerpBaseRot, Quaternion.identity * Quaternion.Euler(0f, 0f, 0f), RotLerpT);
            RotLerpT = Mathf.Clamp01(RotLerpT);
            RotLerpT += 4f * Time.deltaTime;
        }

        if (TurboActivated || transform.rotation != Quaternion.identity)
        {
            TempRotation *= Quaternion.Lerp(TurboBaseRot, TurboDestRot, TurboLerpT);
            TurboLerpT += 2f * Time.deltaTime;
            TurboLerpT = Mathf.Clamp01(TurboLerpT);
            Debug.Log(TurboLerpT);
        }
        //transform.rotation = TempRotation;

        Camera.main.transform.position = transform.position - Vector3.forward * 3 + Vector3.up * 0.2f;
        Camera.main.transform.LookAt(transform.position);

        if(Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    private void FixedUpdate()
    {
        Vector3 pos = transform.position;
        pos += (Vector3.forward * Speed + Vector3.right * HorizontalSpeed * HorizontalMultiplier) * TurboMultiplier * Time.deltaTime;
        pos += GravityVec;
        Rigi.MovePosition(pos);
        Rigi.MoveRotation(TempRotation);
    }

    void ActivateTurbo()
    {
        TurboActivated = true;
        TurboBaseRot = transform.rotation;
        TurboDestRot = Quaternion.Euler(10f, 0f, 0f);
        TurboLerpT = 0.1f;
    }

    void DeactivateTurbo()
    {
        TurboActivated = false;
        TurboBaseRot = transform.rotation;
        TurboDestRot = Quaternion.identity;
        TurboLerpT = 0.1f;
    }

    void AccelerateTurbo()
    {
        TurboMultiplier = TurboCurve.Evaluate(TurboTimer);
        TurboTimer = Mathf.Clamp01(TurboTimer += Time.deltaTime);
    }

    void DeaccelerateTurbo()
    {
        TurboMultiplier = TurboCurve.Evaluate(TurboTimer);
        TurboTimer = Mathf.Clamp01(TurboTimer -= Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("zarlanga");
    }
}
