using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mocap;

public class VfxControl : MonoBehaviour
{
    public GameObject OSCManager;
    
    // Start is called before the first frame update
    void Start()
    {
        SendOSCSimple sendosc = OSCManager.GetComponent<SendOSCSimple>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
