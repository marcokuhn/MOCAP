using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestContainerSystem : MonoBehaviour
{

    [SerializeField]
    protected OSCContainer testContainer;

    public bool changeValueViaScript;


    void OnValidate()
    {
        testContainer = OSCContainerManagement.Instance.containerList.Find(x => x.oscAddress == "/test");
    }

    private void Start()
    {
        testContainer = OSCContainerManagement.Instance.containerList.Find(x => x.oscAddress == "/test");
    }

    private void OnGUI()
    {
        //if (GUI.Button(new Rect(200, 200, 500, 300), "Send test message"))
        //{
        //    if (changeValueViaScript)
        //    {
        //        // be carefull to choose the right valuetype here. wrong valuetypes can also be stored, but will not be transmitted
        //        testContainer.values[0].intValue = 9001;
        //        testContainer.values[1].stringValue = "changed via script";
        //    }
        //    OSCContainerManagement.Instance.SendOSCMessage(testContainer);
        //}
    }


}
