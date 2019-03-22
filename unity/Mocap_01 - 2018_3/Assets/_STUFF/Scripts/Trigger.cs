using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
public class Trigger : MonoBehaviour
{
    public OSCTransmitter Transmitter;
    [SerializeField]
    int TriggerState;
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("start");
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("enter0: " + other.name);
        if (other.CompareTag("Trigger"))
        {
            TriggerState = 1;
            OSCMessage message1;
            message1 = new OSCMessage("/trigger/" + gameObject.name);
            message1.AddValue(OSCValue.Int(TriggerState));
            Debug.Log("enter: " + other.name);
            Transmitter.Send(message1);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Trigger"))
        {
            TriggerState = 0;
            OSCMessage message1;
            message1 = new OSCMessage("/trigger/" + gameObject.name);
            message1.AddValue(OSCValue.Int(TriggerState));
            Debug.Log("exit: " + other.name);
            Transmitter.Send(message1);
        }
    }
}
