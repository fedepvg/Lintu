using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GruaBehaviour : MonoBehaviour
{
    public GameObject Rope;
    public GameObject RopeBottom;
    public GameObject OrbBase;
    public float LerpSpeed;

    float InitialRopeYSize;
    float MinRopeYSize;
    bool GoingDown = true;
    float LerpTimer = 0;

    // Start is called before the first frame update
    void Start()
    {
        InitialRopeYSize = Rope.transform.localScale.y;
        MinRopeYSize = InitialRopeYSize / 2;
    }

    // Update is called once per frame
    void Update()
    {
        float YScale;
        LerpTimer += LerpSpeed * Time.deltaTime;
        if (GoingDown)
            YScale = Mathf.Lerp(InitialRopeYSize, MinRopeYSize, LerpTimer);
        else
            YScale = Mathf.Lerp(MinRopeYSize, InitialRopeYSize, LerpTimer);

        Rope.transform.localScale = new Vector3(Rope.transform.localScale.x, YScale, Rope.transform.localScale.z);

        if (YScale <= MinRopeYSize || YScale >= InitialRopeYSize)
        {
            GoingDown = !GoingDown;
            LerpTimer = 0;
        }

        OrbBase.transform.position = RopeBottom.transform.position;
    }
}
