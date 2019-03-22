using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Position : OSCContainerSetup
{
    public bool sendContinuously;

    public bool normalizeValues;

    private Vector3 oldPosition;
    private Vector3 newPosition;

    private float oSCMessageTimer;
    public float oSCMessageTimerMax = 0.02f;

    private void Update()
    {
        if (oSCContainer.values == null)
            return;

        newPosition = transform.position;

        if (normalizeValues)
            CalculateNormalizedPosition();

        oSCContainer.values[0].floatValue = newPosition.x;
        oSCContainer.values[1].floatValue = newPosition.y;
        oSCContainer.values[2].floatValue = newPosition.z;

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
            if (oldPosition != transform.position)
            {
                SendOSCMessageViaManagement();
            }

            oldPosition = transform.position;
        }
    }

    private void CalculateNormalizedPosition()
    {       
        //newPosition.x = Mathf.InverseLerp(StageVolume.Instance.volumeCollider.bounds.min.x, StageVolume.Instance.volumeCollider.bounds.max.x, transform.position.x);
        //newPosition.y = Mathf.InverseLerp(StageVolume.Instance.volumeCollider.bounds.min.y, StageVolume.Instance.volumeCollider.bounds.max.y, transform.position.y);
        //newPosition.z = Mathf.InverseLerp(StageVolume.Instance.volumeCollider.bounds.min.z, StageVolume.Instance.volumeCollider.bounds.max.z, transform.position.z);
    }
}
