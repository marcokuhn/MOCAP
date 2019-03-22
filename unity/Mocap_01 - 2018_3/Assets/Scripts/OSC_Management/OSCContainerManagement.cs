using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using extOSC;
using UnityEditor;

[ExecuteInEditMode]
public class OSCContainerManagement : Singleton<OSCContainerManagement>
{

    [Header("Setup")]
    public int maxTrackers;

    public List<Tracker> trackerIDRegister = new List<Tracker>();

    public List<OSCContainer> containerList = new List<OSCContainer>();
    public string[] addressArray = new string[0];

    private OSCTransmitter transmitter;


    private void OnValidate()
    {
        addressArray = new string[containerList.Count];

        for (int i = 0; i < containerList.Count; i++)
        {
            addressArray[i] = containerList[i].oscAddress;
        }
    }

    private void Start()
    {
        transmitter = GetComponent<OSCTransmitter>();
    }

    public void SendOSCMessage(OSCContainer messageContainer)
    {
        var message = OSCMessage.Create(messageContainer.oscAddress);

        message.AddValue(OSCValue.Int(messageContainer.trackerID));

        foreach (var value in messageContainer.values)
        {
            switch (value.type)
            {
                case OSCObjectTypes.String:
                    message.AddValue(OSCValue.String(value.stringValue));
                    break;
                case OSCObjectTypes.Int:
                    message.AddValue(OSCValue.Int(value.intValue));
                    break;
                case OSCObjectTypes.Float:
                    message.AddValue(OSCValue.Float(value.floatValue));
                    break;
                case OSCObjectTypes.Bool:
                    message.AddValue(OSCValue.Bool(value.boolValue));
                    break;
                default:
                    break;
            }
        }

        transmitter.Send(message);
    }

    public void CheckForIDDistanceContainer()
    {
        //if (containerList.Find(x => x.oscAddress == "tracker_id_distance") != null)
        //{
        //    OSCContainer oSCContainer = containerList.Find(x => x.oscAddress == "tracker_id_distance");
        //    OSCValueStruct[] newCollection = oSCContainer.values = new OSCValueStruct[trackerIDRegister.Count * 2];
        //    bool switchBool = true;

        //    for (int i = 0; i < newCollection.Length; i++)
        //    {
        //        if (switchBool)
        //        {
        //            newCollection[i].type = OSCObjectTypes.Int;
        //            newCollection[i].valueDescription = "ID (valueCount depending on 'trackerIDRegister.Count'! Do not edit manually!)";
        //        }
        //        else
        //        {
        //            newCollection[i].type = OSCObjectTypes.Float;
        //            newCollection[i].valueDescription = "Distance (valueCount depending on 'trackerIDRegister.Count'! Do not edit manually!)";
        //        }

        //        switchBool = !switchBool;
        //    }

        //    foreach (var containerSetup in oSCContainer.registeredContainerSetups)
        //    {
        //        containerSetup.GetOSCContainer();
        //    }
        //}

        if (containerList.Find(x => x.oscAddress == "tracker_distance") != null)
        {
            OSCContainer oSCContainer = containerList.Find(x => x.oscAddress == "tracker_distance");
            OSCValueStruct[] newCollection = oSCContainer.values = new OSCValueStruct[trackerIDRegister.Count];

            for (int i = 0; i < newCollection.Length; i++)
            {
                newCollection[i].type = OSCObjectTypes.Float;
                newCollection[i].valueDescription = "Distance (valueCount depending on 'trackerIDRegister.Count'! Do not edit manually!)";
            }

            foreach (var containerSetup in oSCContainer.registeredContainerSetups)
            {
                containerSetup.GetOSCContainer();
            }
        }
    }
}

public enum OSCObjectTypes
{
    Int,
    Float,
    Bool,
    String,
}

[System.Serializable]
public struct OSCValueStruct
{
    [Space(5)]

    [SerializeField]
    [Tooltip("choose which data type will be sent")]
    public OSCObjectTypes type;

    [Space(5)]

    [Tooltip("enter Value - might be overwritten in runtime")]
    //[DrawIf("type", OSCObjectTypes.String)]
    [HideInInspector]
    public string stringValue;

    [Tooltip("enter Value - might be overwritten in runtime")]
    //[DrawIf("type", OSCObjectTypes.Int)]
    [HideInInspector]
    public int intValue;

    [Tooltip("enter Value - might be overwritten in runtime")]
    //[DrawIf("type", OSCObjectTypes.Float)]
    [HideInInspector]
    public float floatValue;

    [Tooltip("enter Value - might be overwritten in runtime")]
    //[DrawIf("type", OSCObjectTypes.Bool)]
    [HideInInspector]
    public bool boolValue;

    //[Header("//////////////////////////")]
    [Tooltip("only for editor clarity. Description will not be used otherwise ")]
    public string valueDescription;
}

[System.Serializable]
public class OSCContainer
{
    [Header("/Address - (don't use '/' in the address field)")]
    [Space(5)]
    [Tooltip("this Address defines a specific usecase for the OSC Message - '/' will be added automatically")]
    public string oscAddress = "";

    [Space(5)]

    [HideInInspector]
    [Header("/TrackerID (will be assigned automatically)")]
    [Tooltip("this Integer defines from which Tracker ID the OSC Message was sent")]
    public int trackerID;

    [Space(5)]

    [Header("/Values")]

    public OSCValueStruct[] values;

    //Register Count - DO NOT SET
    //[HideInInspector]
    [ReadOnly]
    [Tooltip("not implemented yet")]
    public int registerCount = 0;

    public List<OSCContainerSetup> registeredContainerSetups = new List<OSCContainerSetup>();
}

[System.Serializable]
public class Tracker
{
    [ReadOnly]
    public GameObject gameObjectReference;

    public int ID;

    public Tracker(GameObject trackerGO, int trackerID) { gameObjectReference = trackerGO; ID = trackerID; }
}

//@TODO:
//registercount
