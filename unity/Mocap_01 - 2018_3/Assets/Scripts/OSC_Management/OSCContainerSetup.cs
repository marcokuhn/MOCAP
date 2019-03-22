using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;
using extOSC;
using UnityEditor;


//Do'nt use this class directly. Only derive from it!
public class OSCContainerSetup : MonoBehaviour
{
    [HideInInspector]
    public static bool statechangeInProgress;

    private string _address;
    public string address
    {
        get { return _address; }
        set
        {
            if (_address == value || statechangeInProgress) return;
            else
            {
                _address = value;
                GetOSCContainer();
            }
        }
    }

    public static int maxTrackerID = 1;

    [HideInInspector]
    private int _trackerID = 0;
    public int trackerID
    {
        get { return _trackerID; }
        set
        {
            if (_trackerID == value) return;
            _trackerID = value;
            if (oSCContainer != null)
            {
                oSCContainer.trackerID = _trackerID;
            }
        }
    }

    [HideInInspector]
    public TrackerID trackerIDComponent;

    [HideInInspector]
    public static string[] addresses = new[] { "" };

    [HideInInspector]
    public int addressDropdownIndex = 0;

    private OSCContainer tmpOSCContainer;

    protected OSCContainer oSCContainer = new OSCContainer();

    private bool firstStart = true;

    private void OnValidate()
    {
        if (GetComponent<TrackerID>() != null)
        {
            trackerIDComponent = GetComponent<TrackerID>();
            trackerID = GetComponent<TrackerID>().trackerID;
        }
        else
            trackerID = 0;
    }

    public void GetOSCContainer()
    {
        if (OSCContainerManagement.Instance != null && OSCContainerManagement.Instance.containerList.Find(x => x.oscAddress == _address) != null)
        {
            tmpOSCContainer = OSCContainerManagement.Instance.containerList.Find(x => x.oscAddress == _address);

            if (!tmpOSCContainer.registeredContainerSetups.Contains(this))
            {
                tmpOSCContainer.registeredContainerSetups.Add(this);
            }

            oSCContainer.oscAddress = "/" + tmpOSCContainer.oscAddress;

            if (oSCContainer.oscAddress.Contains("_"))           
                oSCContainer.oscAddress = oSCContainer.oscAddress.Replace("_", "/");

            oSCContainer.trackerID = trackerID;
            oSCContainer.values = new OSCValueStruct[tmpOSCContainer.values.Length];
            for (int i = 0; i < tmpOSCContainer.values.Length; i++)
            {
                switch (tmpOSCContainer.values[i].type)
                {
                    case OSCObjectTypes.String:
                        oSCContainer.values[i].type = OSCObjectTypes.String;
                        oSCContainer.values[i].stringValue = "";
                        break;
                    case OSCObjectTypes.Int:
                        oSCContainer.values[i].type = OSCObjectTypes.Int;
                        break;
                    case OSCObjectTypes.Float:
                        oSCContainer.values[i].type = OSCObjectTypes.Float;
                        break;
                    case OSCObjectTypes.Bool:
                        oSCContainer.values[i].type = OSCObjectTypes.Bool;
                        break;
                    default:
                        break;
                }

            }
        }
        else
        {
            //@TODO: Gets called alot on Playmode Start. To Be Investigated!

            //if(OSCContainerManagement.Instance == null)
            //    Debug.LogError("OSCContainerSetup could not find OSCContainerManagement. Please instantiate it in the Scene");

            //if(OSCContainerManagement.Instance.containerList.Find(x => x.oscAddress == _address) == null)
            //    Debug.LogError("OSCContainerManagement could not find Setup OSCAdress: " + _address);
        }
    }

    public void SendOSCMessageViaManagement()
    {
        if (OSCContainerManagement.Instance != null)
        {
            if (oSCContainer != null)
            {
                OSCContainerManagement.Instance.SendOSCMessage(oSCContainer);
            }
            else
            {
                Debug.LogError("oscContainer null");
            }
        }
        else
        {
            Debug.LogError("Could not send message to Manager");
        }
    }

    private void OnDestroy()
    {
        if (OSCContainerManagement.Instance != null)
        {
            if (tmpOSCContainer.registeredContainerSetups.Contains(this))
            {
                tmpOSCContainer.registeredContainerSetups.Remove(this);
            }
        }
    }
}