using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Zones : OSCContainerSetup
{
    public bool sendContinuously;

    private List<string> myZones = new List<string>();
    public string[] currentZones;

    private float oSCMessageTimer;
    public float oSCMessageTimerMax = 0.02f;

    private void OnTriggerEnter(Collider collEnter)
    {
        if (collEnter.gameObject.tag == "Zone")
        {
            myZones.Add(collEnter.gameObject.name);
            currentZones = myZones.ToArray();
        }
    }


    private void OnTriggerExit(Collider collExit)
    {
        if (myZones.Contains(collExit.gameObject.name))
        {
            foreach (string zone in currentZones)
            {
                if (collExit.gameObject.name.Equals(zone))
                {
                    myZones.Remove(zone);
                    currentZones = myZones.ToArray();
                }
            }
        }
    }

    private void Update()
    {
        if (oSCContainer.values == null)
            return;

        for (int i = 0; i <= oSCContainer.values.Length - 1; i++)
        {
            if (i <= currentZones.Length - 1)
            {
                oSCContainer.values[i].stringValue = currentZones[i];
            }
            else
            {
                oSCContainer.values[i].stringValue = "";
            }
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
    }
}

