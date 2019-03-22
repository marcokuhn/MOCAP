using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class TrackerID : MonoBehaviour
{
    public static bool statechangeInProgress;
    private bool firstStart = true;

    [HideInInspector]
    public GUIStyle backgroundStyle = new GUIStyle();

    [HideInInspector]
    public Color backgroundColor;

    [HideInInspector]
    public Color formerBackgroundColor = Color.gray;

    [HideInInspector]
    public bool showSlider;

    [SerializeField, HideInInspector]
    private int _trackerID;
    public int trackerID
    {
        get { return _trackerID; }
        set
        {
            if (_trackerID == value) return;
            _trackerID = value;
            if (!isDuplicate)
                CheckIDRegister();
        }
    }

    private bool isDuplicate;

    void OnDisable()
    {
        if (!isDuplicate && OSCContainerManagement.Instance != null)
        {
            if (this.gameObject.activeSelf == false || this.enabled == false)
            {
                UnRegisterSelf();
            }
        }
        showSlider = false;
    }

    void OnEnable()
    {
        if (GetComponent<TrackerID>() != null)
        {
            isDuplicate = false;
            foreach (var script in GetComponents<TrackerID>())
            {
                if (script != this && script.enabled)
                {
                    isDuplicate = true;
                }
            }
        }
        if (!isDuplicate)
        {
            CheckIDRegister();
            showSlider = true;
        }
    }

    private void Start()
    {
        statechangeInProgress = false;
        firstStart = true;
    }

    private void SetOSCContainerSetupReference(bool activate)
    {
        if (GetComponent<OSCContainerSetup>() != null)
        {
            if (activate)
            {
                foreach (var script in GetComponents<OSCContainerSetup>())
                {
                    script.trackerIDComponent = this;
                    script.trackerID = _trackerID;
                }
            }
            else
            {
                foreach (var script in GetComponents<OSCContainerSetup>())
                {
                    script.trackerIDComponent = null;
                    script.trackerID = 0;
                }
            }
        }
    }

    private void CheckIDRegister()
    {
        if (OSCContainerManagement.Instance != null)
        {
            if (OSCContainerManagement.Instance.trackerIDRegister.Find(x => x.gameObjectReference == gameObject) != null)
            {
                if (OSCContainerManagement.Instance.trackerIDRegister.Find(x => x.ID == _trackerID && x.gameObjectReference != gameObject) != null)
                {
                    backgroundColor = Color.red;
                    UnRegisterSelf();
                }
                else
                {
                    OSCContainerManagement.Instance.trackerIDRegister.Find(x => x.gameObjectReference == gameObject).ID = _trackerID;
                    backgroundColor = Color.green;
                    SetOSCContainerSetupReference(true);
                }

            }
            else if (OSCContainerManagement.Instance.trackerIDRegister.Find(x => x.ID == _trackerID) != null)
            {
                backgroundColor = Color.red;
                UnRegisterSelf();
            }
            else
            {
                OSCContainerManagement.Instance.trackerIDRegister.Add(new Tracker(gameObject, _trackerID));
                backgroundColor = Color.green;
                SetOSCContainerSetupReference(true);
            }
            OSCContainerManagement.Instance.CheckForIDDistanceContainer();
        }
    }

    private void UnRegisterSelf()
    {
        if (OSCContainerManagement.Instance != null)
        {
            if (OSCContainerManagement.Instance.trackerIDRegister.Find(x => x.gameObjectReference == gameObject) != null)
            {
                OSCContainerManagement.Instance.trackerIDRegister.Remove
                    (OSCContainerManagement.Instance.trackerIDRegister.Find(x => x.gameObjectReference == gameObject));
            }
            OSCContainerManagement.Instance.CheckForIDDistanceContainer();
        }
        SetOSCContainerSetupReference(false);
    }

    private void OnDestroy()
    {
        if (OSCContainerManagement.Instance != null)
        {
            UnRegisterSelf();
        }
    }
}




