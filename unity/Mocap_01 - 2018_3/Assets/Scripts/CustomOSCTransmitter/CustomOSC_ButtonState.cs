using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Valve.VR;

public class CustomOSC_ButtonState : OSCContainerSetup
{
    /*
    private SteamVR_TrackedObject trackedObject;
    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObject.index); }
    }

    private float triggerPressed;
    private int touchpadPressed;

    private void Start()
    {
        trackedObject = GetComponent<SteamVR_TrackedObject>();
    }

    private void Update()
    {
        if (GetTriggerInput() | GetTouchpadPressedInput()) // | GetTouchpadTouchInput())
        {
            SendOSCMessageViaManagement();
        }
    }

    private bool GetTriggerInput()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Trigger) ||
           Controller.GetPress(SteamVR_Controller.ButtonMask.Trigger))
        {
            oSCContainer.values[0].floatValue = triggerPressed = Controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x;
            return true;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Trigger))
        {
            oSCContainer.values[0].floatValue = triggerPressed = Controller.GetAxis(EVRButtonId.k_EButton_SteamVR_Trigger).x;
            return true;
        }

        return false;
    }

    private bool GetTouchpadPressedInput()
    {
        if (Controller.GetPressDown(SteamVR_Controller.ButtonMask.Touchpad) ||
           Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            oSCContainer.values[1].intValue = touchpadPressed = 1;
            return true;
        }

        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad))
        {
            oSCContainer.values[1].intValue = touchpadPressed = 0;
            return true;
        }

        return false;
    }

    //private bool GetTouchpadTouchInput()
    //{
    //    if (Controller.GetAxis().x != 0 || Controller.GetAxis().y != 0)
    //    {
    //        oSCContainer.values[2].floatValue = Controller.GetAxis().x;
    //        oSCContainer.values[3].floatValue = Controller.GetAxis().y;
    //        return true;
    //    }

    //    return false;
    //}
    */
}
