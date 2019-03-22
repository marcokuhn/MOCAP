using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_ZoneExit : OSCContainerSetup
{
    string currentZoneExit;

    private void OnTriggerExit(Collider collExit)
    {
        if (collExit.gameObject.tag == "Zone")
        {
            currentZoneExit = collExit.gameObject.name;

            if (oSCContainer.values == null)
                return;

            oSCContainer.values[0].stringValue = currentZoneExit;

            SendOSCMessageViaManagement();
        }
    }
}
