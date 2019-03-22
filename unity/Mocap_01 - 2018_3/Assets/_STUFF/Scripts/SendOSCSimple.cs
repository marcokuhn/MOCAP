using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace Mocap {
    public class SendOSCSimple : MonoBehaviour
{
    public OSC oscReference;
    public GUILayout Button;
    [Header("Transform Objects")]

    public GameObject RootObject;
    public string ExcludeNames;
    public List<Transform> mocap_transforms;

    private Vector3[] lastPositions;
    private Vector3[] velocities;
    private Vector3[] lastRotations;
    private Vector3[] velocities_rotation;
    private int index;
    [Header("Debug")]
    public float totalVelocity;
    public float totalVelocityRotation;

    private OscMessage message1;
    private OscMessage message2;
    private OscMessage message3;
    private OscMessage message4;
    private OscMessage message5;
    private OscMessage message6;
    private OscMessage[] OscMessages;

    //
    public Dictionary<string, Vector3> Positions;
    public Dictionary<string, Quaternion> Rotations;
    public Dictionary<string, Vector3> Velocities;

    string tfName;

    void Start()
    {
        Debug.Log("mocap start");
        lastPositions = new Vector3[mocap_transforms.Count];
        velocities = new Vector3[mocap_transforms.Count];
        lastRotations = new Vector3[mocap_transforms.Count];
        velocities_rotation = new Vector3[mocap_transforms.Count];

        Positions = new Dictionary<string, Vector3>();
        Rotations = new Dictionary<string, Quaternion>();
        Velocities = new Dictionary<string, Vector3>();

        message1 = new OscMessage();
        string tfName = "";
    }

    void Update()
    {
        foreach (Transform tf in mocap_transforms)
        {
            // ALL TRANSFORM OBJECTS
            if (tf)
            {
                tfName = tf.name.Replace("Robot_", "");

                // POSITION
                Positions[tfName] = tf.position;
                //TODO GC 200ms
                message1.address = "/mocap/" + tfName + "/position";

                message1.values.Clear();
                message1.values.Add(tf.position.x);
                message1.values.Add(tf.position.y);
                message1.values.Add(tf.position.z);

                oscReference.Send(message1);
               
                //ROTATION
                Rotations[tfName] = tf.rotation;
                message1.address = ("/mocap/" + tfName + "/rotation");
                message1.values.Clear();
                message1.values.Add(WrapAngle(tf.localEulerAngles.x));
                message1.values.Add(WrapAngle(tf.localEulerAngles.y));
                message1.values.Add(WrapAngle(tf.localEulerAngles.z));
                oscReference.Send(message1);
                
                // VELOCITY
                velocities[index] = (tf.position - lastPositions[index]) / Time.deltaTime;
                Velocities[tfName] = velocities[index];
                message1.address = "/mocap/" + tfName + "/velocity";
                message1.values.Clear();
                message1.values.Add(velocities[index].magnitude);
                oscReference.Send(message1);
/*
                // VELOCITY ROTATION
                velocities_rotation[index] = (tf.localEulerAngles - lastRotations[index]) / Time.deltaTime;
                message1.address = "/mocap/" + tfName + "/velocity_rotation";
                message1.values.Clear();
                message1.values.Add(velocities_rotation[index].magnitude);
                oscReference.Send(message1);
                // store current values for next frame
                lastPositions[index] = tf.position;
                lastRotations[index] = tf.localEulerAngles;
   */
                //
                index++;
            }

        }
/*
        // VELOCITY ALL
        // sum up all velocities into 1 value

        totalVelocity = 0;
        for (int i = 0; i < velocities.Length; i++)
        {
            totalVelocity += velocities[i].magnitude;
        }

        message1.address = "/mocap/VelocityAll";
        message1.values.Clear();
        message1.values.Add(totalVelocity);
        oscReference.Send(message1);

        // VELOCITY ROTATION ALL
        // sum up all rotation velocities into 1 value
        totalVelocityRotation = 0;
        for (int i = 0; i < velocities_rotation.Length; i++)
        {
            totalVelocityRotation += velocities_rotation[i].magnitude;
        }

        message1.address = "/mocap/VelocityRotationAll";
        message1.values.Clear();
        message1.values.Add(totalVelocityRotation);
        oscReference.Send(message1);
 */
        index = 0;
    }

    private static float UnwrapAngle(float angle)
    {
        if (angle >= 0)
            return angle;
        angle = -angle % 360;
        return 360 - angle;
    }
    private static float WrapAngle(float angle)
    {
        angle %= 360;
        if (angle > 180)
            return angle - 360;
        return angle;
    }

}
}

