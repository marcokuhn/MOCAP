using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class TrackerAccelerationExample : OSCContainerSetup {

    public bool sendContinuously;

    public float velocity;

    private Vector3 lastPosition;
    private Vector3 newPosition;

    void Update()
    {
        if (oSCContainer.values == null)
            return;

        newPosition = transform.position;

        if (Time.deltaTime != 0.0f)
        {
            velocity = ((newPosition -lastPosition) /  Time.deltaTime).magnitude;
        }

        lastPosition = newPosition;

        oSCContainer.values[0].floatValue = velocity;

        if (sendContinuously && velocity != 0.0f)
        {
            SendOSCMessageViaManagement();
        }
    }
}
