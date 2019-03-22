using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mocap;

public class Attractor : MonoBehaviour
{
    public GameObject Target;
    private Rigidbody rb;
    public float power;
    public GameObject OSCManager;
    private SendOSCSimple sendOSC;
    public float totalPower = 0;
    public float scaleFactor = 1;
    public float minScale = 0.2f;
    public string TargetName;
    public float smoothTime = 0.2f;
    public float smoothMaxSpeed = 5f;
    float scSmooth;
    float currentVel;

    // Start is called before the first frame update
    void Start()
    {
        if (!Target)
        {
            Target = GameObject.Find(TargetName);
        }
        if (!OSCManager)
        {
            OSCManager = GameObject.Find("OSCManager");
        }
        rb = GetComponent<Rigidbody>();
        sendOSC = OSCManager.GetComponent<SendOSCSimple>();
    }

    // Update is called once per frame
    void Update()
    {
        if (sendOSC.totalVelocity != 0)
        {
            float sc = sendOSC.totalVelocity * 0.2f * scaleFactor + minScale;
            scSmooth = Mathf.SmoothDamp(scSmooth, sc, ref currentVel, smoothTime, smoothMaxSpeed);
            transform.localScale = new Vector3(scSmooth, scSmooth, scSmooth);
            totalPower = sendOSC.totalVelocity * power;
            rb.AddForce((Target.transform.position - transform.position).normalized * totalPower * Time.smoothDeltaTime);
        }

    }
}
