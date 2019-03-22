using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Switched_ZoneExit : OSCContainerSetup
{
    int currentTrackerExit;

    private void OnTriggerExit(Collider collExit)
    {
        if (collExit.gameObject.tag == "Tracker")
        {
            currentTrackerExit = collExit.gameObject.GetComponent<TrackerID>().trackerID;

            if (oSCContainer.values == null)
                return;

            oSCContainer.values[0].stringValue = gameObject.name;
            oSCContainer.values[1].intValue = currentTrackerExit;

            SendOSCMessageViaManagement();
        }
    }
}
