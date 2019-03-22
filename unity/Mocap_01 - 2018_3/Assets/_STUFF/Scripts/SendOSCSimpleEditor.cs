
using UnityEngine;
using UnityEditor;
using System.Collections;
using System.Collections.Generic;
using Mocap;

[CustomEditor(typeof(SendOSCSimple))]
public class SendOSCSimpleEditor : Editor
{
    private SerializedProperty itemImagesProperty;
    private SerializedProperty itemsProperty;
    //protected bool[] Activate = new bool[SendOSCSimple.Activate];

    private const string inventoryPropItemImagesName = "itemImages";
    private const string inventoryPropItemsName = "items";

    private void OnEnable()
    {
        itemImagesProperty = serializedObject.FindProperty(inventoryPropItemImagesName);
        itemsProperty = serializedObject.FindProperty(inventoryPropItemsName);


    }

    public override void OnInspectorGUI()
    {
        SendOSCSimple so = target as SendOSCSimple;
        //so.Activate1 = new Lis
        DrawDefaultInspector();

        SendOSCSimple sendosc = (SendOSCSimple)target;
        if (GUILayout.Button("Get Transforms"))
        {
            if (so.RootObject)
            {
                Transform[] AllTransforms = so.RootObject.GetComponentsInChildren<Transform>();
                so.mocap_transforms.Clear();
                for (int i = 0; i < AllTransforms.Length; i++)
                {
                    string[] excludes = so.ExcludeNames.Split(',');
                    bool keep = true;
                    for (int j = 0; j < excludes.Length; j++)
                    {
                        if (AllTransforms[i].name.Contains(excludes[j].Trim()))
                        {
                            keep = false;
                        }
                    }
                    if (keep)
                    {
                        so.mocap_transforms.Add(AllTransforms[i]);
                    }
                    keep = true;
                    
                    
                }
            }
            else
            {
                Debug.LogWarning("Add Root Object");
                //GUILayout.TextArea("text", GUI.skin.GetStyle("HelpBox"));
            }
        }

    } //OnInspectorGUI
}
