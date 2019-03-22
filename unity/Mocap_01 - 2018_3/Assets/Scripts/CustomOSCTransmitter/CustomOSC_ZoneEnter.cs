using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_ZoneEnter : OSCContainerSetup
{
    string currentZoneEnter;

    private void OnTriggerEnter(Collider collEnter)
    {
        if (collEnter.gameObject.tag == "Zone")
        {
            currentZoneEnter = collEnter.gameObject.name;

            if (oSCContainer.values == null)
                return;

            oSCContainer.values[0].stringValue = currentZoneEnter;

            SendOSCMessageViaManagement();
        }
    }
}
