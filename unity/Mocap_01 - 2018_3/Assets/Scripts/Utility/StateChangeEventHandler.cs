using UnityEngine;
using UnityEditor;

[InitializeOnLoadAttribute]
public static class StateChangeEventHandler {

    static StateChangeEventHandler()
    {
        EditorApplication.playModeStateChanged += LogPlayModeState;       
    }

    static void LogPlayModeState(PlayModeStateChange state)
    {
        switch (state)
        {
            case PlayModeStateChange.EnteredEditMode:
                TrackerID.statechangeInProgress = false;
                OSCContainerSetup.statechangeInProgress = false;
                break;
            case PlayModeStateChange.ExitingEditMode:
                TrackerID.statechangeInProgress = true;
                OSCContainerSetup.statechangeInProgress = true;
                break;
            case PlayModeStateChange.EnteredPlayMode:
                TrackerID.statechangeInProgress = false;
                OSCContainerSetup.statechangeInProgress = false;
                break;
            case PlayModeStateChange.ExitingPlayMode:
                TrackerID.statechangeInProgress = true;
                OSCContainerSetup.statechangeInProgress = true;
                break;
            default:
                break;
        }
    }
}
