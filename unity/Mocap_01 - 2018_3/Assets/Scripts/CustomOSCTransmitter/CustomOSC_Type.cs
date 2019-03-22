using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Type : OSCContainerSetup
{

    public bool sendContinuously;

    public string type;
    private string lastType;

    private float oSCMessageTimer;
    public float oSCMessageTimerMax = 0.02f;

    void Update()
    {
        if (oSCContainer.values == null)
            return;

        oSCContainer.values[0].stringValue = type;

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


        if (lastType != type)
        {
            SendOSCMessageViaManagement();
        }

        lastType = type;
    }

    public void Typechange(string newType)
    {
        type = newType;
        SendOSCMessageViaManagement();
    }
}
