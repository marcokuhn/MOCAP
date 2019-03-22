using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionType : MonoBehaviour
{
    public string collisionName;
    public GameObject preferredZone;

    public List<CustomOSC_Collision> possibleTrackers = new List<CustomOSC_Collision>();
}
