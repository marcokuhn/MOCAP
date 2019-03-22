using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Velocity : OSCContainerSetup{

    public bool sendContinuously;
    public bool normalizeValue;

    public float velocity;
    private float minVelocity = 0;
    private float maxVelocity = 6;

    private Vector3 lastPosition;
    private Vector3 newPosition;

    private float oSCMessageTimer;
    public float oSCMessageTimerMax = 0.02f;

    [HideInInspector] public float lastVelocity = 0;

    void Update ()
    {
        if (oSCContainer.values == null)
            return;

        newPosition = transform.position;

        if (Time.deltaTime != 0.0f)
        {
            velocity = ((newPosition - lastPosition) / Time.deltaTime).magnitude;
        }

        lastPosition = newPosition;

        if (normalizeValue)
        {
            velocity = (velocity - minVelocity) / (maxVelocity - minVelocity);
            velocity = Mathf.Clamp01(velocity);
        }
        lastVelocity = velocity;

        oSCContainer.values[0].floatValue = velocity;

        oSCMessageTimer += Time.deltaTime;

        if (oSCMessageTimer >= oSCMessageTimerMax && sendContinuously && velocity != 0.0f)
        {
            SendOSCMessageViaManagement();
            oSCMessageTimer = 0;
        }

        //if (sendContinuously && velocity != 0.0f)
        //{
        //    SendOSCMessageViaManagement();
        //}
    }
}
