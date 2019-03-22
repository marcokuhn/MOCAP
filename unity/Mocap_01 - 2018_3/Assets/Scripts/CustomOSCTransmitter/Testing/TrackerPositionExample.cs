using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackerPositionExample : OSCContainerSetup{

    public bool sendContinuously;
	
	private void Update()
    {
        if (oSCContainer.values == null)
            return;

        oSCContainer.values[0].floatValue = transform.position.x;
        oSCContainer.values[1].floatValue = transform.position.y;
        oSCContainer.values[2].floatValue = transform.position.z;

        oSCContainer.values[3].floatValue = transform.rotation.x;
        oSCContainer.values[4].floatValue = transform.rotation.y;
        oSCContainer.values[5].floatValue = transform.rotation.z;
        oSCContainer.values[6].floatValue = transform.rotation.w;

        if (sendContinuously)
        {
            SendOSCMessageViaManagement();
        }
	}
}
