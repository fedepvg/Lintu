using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GateBehaviour : MonoBehaviour
{
    public GameObject LeftGate;
    public GameObject CenterGate;
    public GameObject RightGate;
    public List<GameObject> FirstDiagonalGears;
    public List<GameObject> SecondDiagonalGears;
    public float GateMovementTime;
    public float GearRotationSpeed;

    float TopYPosition;
    float BottomYPosition;
    float GearDirectionSign = 1;
    float GateMovementTimer;
    float PrevFrameTime;

    // Start is called before the first frame update
    void Start()
    {
        CenterGate.transform.localPosition -= Vector3.up * CenterGate.transform.localPosition.y;
        BottomYPosition = CenterGate.transform.localPosition.y;
        TopYPosition = LeftGate.transform.localPosition.y;
    }

    // Update is called once per frame
    void Update()
    {
        float LeftYPos;
        float CenterYPos;
        float RightYPos;

        GateMovementTimer = Mathf.PingPong(Time.time, GateMovementTime) / GateMovementTime;

        LeftYPos = Mathf.Lerp(BottomYPosition, TopYPosition, GateMovementTimer);
        CenterYPos = Mathf.Lerp(TopYPosition, BottomYPosition, GateMovementTimer);
        RightYPos = Mathf.Lerp(BottomYPosition, TopYPosition, GateMovementTimer);

        LeftGate.transform.localPosition = new Vector3(LeftGate.transform.localPosition.x, LeftYPos, LeftGate.transform.localPosition.z);
        CenterGate.transform.localPosition = new Vector3(CenterGate.transform.localPosition.x, CenterYPos, CenterGate.transform.localPosition.z);
        RightGate.transform.localPosition = new Vector3(RightGate.transform.localPosition.x, RightYPos, RightGate.transform.localPosition.z);

        if (PrevFrameTime <= GateMovementTimer)
            GearDirectionSign = 1;
        else if (PrevFrameTime >=GateMovementTimer)
            GearDirectionSign = -1;

        PrevFrameTime = GateMovementTimer;

        for (int i = 0; i < FirstDiagonalGears.Count; i++)
        {
            FirstDiagonalGears[i].transform.Rotate(Vector3.right * GearRotationSpeed * GearDirectionSign * Time.deltaTime);
        }

        for (int i = 0; i < SecondDiagonalGears.Count; i++)
        {
            SecondDiagonalGears[i].transform.Rotate(Vector3.right * -GearRotationSpeed * GearDirectionSign * Time.deltaTime);
        }
    }
}
