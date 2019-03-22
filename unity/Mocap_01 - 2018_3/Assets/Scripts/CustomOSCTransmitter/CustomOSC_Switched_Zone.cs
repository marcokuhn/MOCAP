using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Switched_Zone : OSCContainerSetup
{
    public bool sendContinuously;

    private List<int> myTrackers = new List<int>();
    public int[] currentTrackers;

    private float oSCMessageTimer;
    public float oSCMessageTimerMax = 0.02f;

    private void OnTriggerEnter(Collider collEnter)
    {
        if (collEnter.gameObject.tag == "Tracker")
        {
            myTrackers.Add(collEnter.gameObject.GetComponent<TrackerID>().trackerID);
            currentTrackers = myTrackers.ToArray();
        }
    }


    private void OnTriggerExit(Collider collExit)
    {
        if (collExit.gameObject.tag == "Tracker")
        {
            if (myTrackers.Contains(collExit.gameObject.GetComponent<TrackerID>().trackerID))
            {
                foreach (int tracker in currentTrackers)
                {
                    if (collExit.gameObject.GetComponent<TrackerID>().trackerID.Equals(tracker))
                    {
                        myTrackers.Remove(tracker);
                        currentTrackers = myTrackers.ToArray();
                    }
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
            if (i == 0)
            {
                oSCContainer.values[0].stringValue = gameObject.name;
            }
            else
            {
                if (i <= currentTrackers.Length)
                {
                    oSCContainer.values[i].intValue = currentTrackers[i - 1];
                }
                else
                {
                    oSCContainer.values[i].intValue = 0;
                }
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
