using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Distance : OSCContainerSetup
{
    // Distance 1, 2 3, 4, 5, 6... in Reihenfolge -> ID Weglassen
    public bool sendContinuously;

    public bool normalizeValues;

    private Vector3 oldPosition;
    private Vector3 newPosition;

    private float longestDistance;

    private float oSCMessageTimer;
    public float oSCMessageTimerMax = 0.02f;

    private void Start ()
    {
        //longestDistance = StageVolume.Instance.volumeCollider.bounds.size.magnitude;
    }	

	private void Update ()
    {
        if (oSCContainer.values == null || oSCContainer.oscAddress != "/tracker/distance")
            return;

        newPosition = transform.position;

        oSCMessageTimer += Time.deltaTime;

        if (oSCMessageTimer >= oSCMessageTimerMax && sendContinuously)
        {
            SetupContainer(normalizeValues);
            SendOSCMessageViaManagement();
            oSCMessageTimer = 0;
        }
        //if (sendContinuously)
        //{
        //    SetupContainer(normalizeValues);
        //    SendOSCMessageViaManagement();
        //}
        else
        {
            if (oldPosition != transform.position)
            {
                SetupContainer(normalizeValues);
                SendOSCMessageViaManagement();
            }

            oldPosition = transform.position;
        }
    }

    private void SetupContainer(bool normalize)
    {
        ResetDistanceValues();

        //for (int i = 0; i < OSCContainerManagement.Instance.trackerIDRegister.Count * 2; i += 2)
        //{
        //    oSCContainer.values[i].intValue = OSCContainerManagement.Instance.trackerIDRegister[(i / 2)].ID;

        //    if (normalize)
        //        oSCContainer.values[i + 1].floatValue = Mathf.InverseLerp
        //            (0, longestDistance, CalculateDistance(OSCContainerManagement.Instance.trackerIDRegister[(i / 2)]));
        //    else
        //        oSCContainer.values[i + 1].floatValue = CalculateDistance(OSCContainerManagement.Instance.trackerIDRegister[(i / 2)]);
        //}

        for (int i = 1; i <= OSCContainerManagement.Instance.trackerIDRegister.Count; i++)
        {
            if (normalize)
            {
                oSCContainer.values[i-1].floatValue = Mathf.InverseLerp(0, longestDistance, CalculateDistance(OSCContainerManagement.Instance.trackerIDRegister.Find(x => x.ID == i)));
            }
            else
                oSCContainer.values[i-1].floatValue = CalculateDistance(OSCContainerManagement.Instance.trackerIDRegister.Find(x => x.ID == i));
        }
    }

    private void ResetDistanceValues()
    {
        for (int i = 0; i < oSCContainer.values.Length; i++)
        {
            oSCContainer.values[i].intValue = 99;
            oSCContainer.values[i].floatValue = 99.0f;
        }
    }

    private float CalculateDistance(Tracker target)
    {
        return Vector3.Distance(target.gameObjectReference.transform.position, transform.position);
    }
}
