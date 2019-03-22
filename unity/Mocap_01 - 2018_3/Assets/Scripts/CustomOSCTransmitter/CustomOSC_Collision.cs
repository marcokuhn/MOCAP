using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomOSC_Collision : OSCContainerSetup
{
    private int myID;
    private int otherID;

    private CollisionType collType;
    private float force;
    private float otherForce;
    private string zone = "";
    private string type = "";

    private CustomOSC_Velocity messageVelocity;
    private CustomOSC_Zones messageZones;

    private bool startComplete = false;

    private void Start()
    {
        messageVelocity = gameObject.GetComponent<CustomOSC_Velocity>();
        messageZones = gameObject.GetComponent<CustomOSC_Zones>();
        startComplete = true;
    }


    private void OnTriggerEnter(Collider collEnter)
    {
        if (oSCContainer.values == null)
            return;

        if (collEnter.gameObject.tag == "Tracker" && startComplete)
        {
            collType = collEnter.gameObject.GetComponent<CollisionType>();

            if (collType)
            {
                if (collType.possibleTrackers.Contains(this))
                {
                    type = collType.collisionName;

                    myID = gameObject.GetComponent<TrackerID>().trackerID;
                    otherID = collEnter.gameObject.GetComponent<TrackerID>().trackerID;
                 
                    force = messageVelocity.lastVelocity;
                    otherForce = collEnter.gameObject.GetComponent<CustomOSC_Velocity>().lastVelocity;
                    if (otherForce > force)
                    {
                        force = otherForce;
                    }


                    for (int i = 0; i < messageZones.currentZones.Length; i++)
                    {
                        if (!messageZones.currentZones[i].Equals(""))
                        {
                            zone = messageZones.currentZones[i];
                        }
                    }
                    //if (collType.preferredZone)
                    //{
                    //    for (int i = 0; i < messageZones.currentZones.Length; i++)
                    //    {
                    //        if (!messageZones.currentZones[i].Equals(collType.preferredZone.name))
                    //        {
                    //            zone = collType.preferredZone.name;
                    //        }
                    //    }
                        
                    //}

                    oSCContainer.values[0].intValue = myID;
                    oSCContainer.values[1].intValue = otherID;
                    oSCContainer.values[2].floatValue = force;
                    oSCContainer.values[3].stringValue = type;
                    oSCContainer.values[4].stringValue = zone;

                    SendOSCMessageViaManagement();
                }
            }
        }
        //else
        //{
        //    Debug.Log("A Collision can only happen if the two colliding objects are Trackers.");
        //}
    }
}
