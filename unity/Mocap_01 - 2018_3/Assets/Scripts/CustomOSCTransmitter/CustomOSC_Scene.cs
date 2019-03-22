using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CustomOSC_Scene : OSCContainerSetup
{
    public string sceneName;

    public bool sendContinuously = false;
    private bool stopSending = false;

	void Start ()
    {
        //Scene scene = SceneManager.GetActiveScene();
        //sceneName = scene.name;

        //if (oSCContainer.values == null)
        //    return;

        //oSCContainer.values[0].stringValue = sceneName;
        //SendOSCMessageViaManagement();
    }

    void Update()
    {
        if (sendContinuously)
        {
            Scene scene = SceneManager.GetActiveScene();
            sceneName = scene.name;

            if (oSCContainer.values == null)
                return;

            oSCContainer.values[0].stringValue = sceneName;
            SendOSCMessageViaManagement();
        }
        else
        {
            if (!stopSending)
            {
                Scene scene = SceneManager.GetActiveScene();
                sceneName = scene.name;

                if (oSCContainer.values == null)
                    return;

                oSCContainer.values[0].stringValue = sceneName;
                SendOSCMessageViaManagement();

                stopSending = true;
            }
        }
    }
}
