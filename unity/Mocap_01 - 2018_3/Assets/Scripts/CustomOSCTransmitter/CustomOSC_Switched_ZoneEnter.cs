using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Switched_ZoneEnter : OSCContainerSetup
{
    int currentTrackerEnter;

    private void OnTriggerEnter(Collider collEnter)
    {
        if (collEnter.gameObject.tag == "Tracker")
        {
            currentTrackerEnter = collEnter.gameObject.GetComponent<TrackerID>().trackerID;

            if (oSCContainer.values == null)
                return;

            oSCContainer.values[0].stringValue = gameObject.name;
            oSCContainer.values[1].intValue = currentTrackerEnter;

            SendOSCMessageViaManagement();
        }
    }
}
