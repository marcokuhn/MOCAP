using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Rotation : OSCContainerSetup
{

    public bool sendContinuously;
    public RotationData rotationData;

    [DrawIf("rotationData", RotationData.EulerAngles)]
    public bool normalizeValues;

    private Vector3 oldRotation;
    private Vector3 newRotation;

    private float oSCMessageTimer;
    public float oSCMessageTimerMax = 0.02f;

    private void Update()
    {
        if (oSCContainer.values == null)
            return;

        switch (rotationData)
        {
            case RotationData.Quaternion:

                oSCContainer.values[0].floatValue = transform.rotation.x;
                oSCContainer.values[1].floatValue = transform.rotation.y;
                oSCContainer.values[2].floatValue = transform.rotation.z;
                oSCContainer.values[3].floatValue = transform.rotation.w;

                break;

            case RotationData.EulerAngles:

                newRotation = transform.eulerAngles;

                if (normalizeValues)
                    CalculateNormalizedPosition();

                newRotation.x = (Mathf.Round(newRotation.x * 100000.0f) / 100000.0f);
                newRotation.y = (Mathf.Round(newRotation.y * 100000.0f) / 100000.0f);
                newRotation.z = (Mathf.Round(newRotation.z * 100000.0f) / 100000.0f);

                oSCContainer.values[0].floatValue = newRotation.x;
                oSCContainer.values[1].floatValue = newRotation.y;
                oSCContainer.values[2].floatValue = newRotation.z;

                oSCContainer.values[3].floatValue = 99.0f; // value is not used

                break;

            default:
                break;
        }

        oSCMessageTimer += Time.deltaTime;

        if (oSCMessageTimer >= oSCMessageTimerMax && sendContinuously)
        {
            SendOSCMessageViaManagement();
            oSCMessageTimer = 0;
        }
        //if (sendContinuously)
        //{
        //    SendOSCMessageViaManagement();
        //}
        else
        {
            if (oldRotation != transform.eulerAngles)
            {
                SendOSCMessageViaManagement();
            }

            oldRotation = transform.eulerAngles;
        }
    }

    private void CalculateNormalizedPosition()
    {
        newRotation.x = Mathf.InverseLerp(0, 360, transform.eulerAngles.x);
        newRotation.y = Mathf.InverseLerp(0, 360, transform.eulerAngles.y);
        newRotation.z = Mathf.InverseLerp(0, 360, transform.eulerAngles.z);
    }

}

public enum RotationData { Quaternion, EulerAngles }
