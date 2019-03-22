using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;

public class OSCContainerReciever : MonoBehaviour {

    private OSCReceiver receiver;

    void Start ()
    {
        receiver = GetComponent<OSCReceiver>();

        if (OSCContainerManagement.Instance != null)
        {
            BindAddresses();
        }
        else
        {
            Invoke("BindAddresses", 1);
        }
	}

    private void ReceivedMessage(OSCMessage message)
    {
       Debug.LogFormat("Received: {0}", message);
    }

    private void BindAddresses()
    {
        if (OSCContainerManagement.Instance != null)
        {
            foreach (var container in OSCContainerManagement.Instance.containerList)
            {
                receiver.Bind(container.oscAddress, ReceivedMessage);
            }
        }
        else
        {
            Debug.LogError("Reciever could not bind addreses due to mising OSCContainerManagement");
        }
    }
}
