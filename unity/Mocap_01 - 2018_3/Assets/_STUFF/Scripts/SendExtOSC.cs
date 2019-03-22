/* Copyright (c) 2018 ExT (V.Sigalkin) */

using UnityEngine;
using System.Collections.Generic;

public class SendExtOSC : MonoBehaviour
{
    public Transform handLeft;
    public Transform[] mocap_transforms;
    public bool writeToLog;

    [Header("OSC Settings")]
    public extOSC.OSCTransmitter Transmitter;
    private extOSC.OSCMessage message;
    private Vector3[] lastPositions;
    private Vector3[] velocities;
    private Vector3[] lastRotations;
    private Vector3[] velocities_rotation;
    private int index;
    public float totalVelocity;
    public float totalVelocityRotation;
    /*
    private extOSC.OSCMessage message1;
    private extOSC.OSCMessage message2;
    private extOSC.OSCMessage message3;
    private extOSC.OSCMessage message4;
    private extOSC.OSCMessage message5;
    private extOSC.OSCMessage message6;
    */
    Dictionary<string, extOSC.OSCMessage> TestMessages;

    void Start()
    {
        index = 0;
        lastPositions = new Vector3[mocap_transforms.Length];
        velocities = new Vector3[mocap_transforms.Length];
        lastRotations = new Vector3[mocap_transforms.Length];
        velocities_rotation = new Vector3[mocap_transforms.Length];

       TestMessages = new Dictionary<string, extOSC.OSCMessage>();
        for (int i = 0; i < 60; i++)
        {
            TestMessages.Add("testmessage_" + i.ToString(), new extOSC.OSCMessage("address" + i.ToString()));
        }
        /*
        message1 = new extOSC.OSCMessage("");
        message2 = new extOSC.OSCMessage("");
        message3 = new extOSC.OSCMessage("");
        message4 = new extOSC.OSCMessage("");
        message5 = new extOSC.OSCMessage("");
        message6 = new extOSC.OSCMessage("");
        */
    }

    void Update()
    {

        for (int i = 0; i < TestMessages.Count ; i++)
        {
            var msg = TestMessages["testmessage_" + i.ToString()];
            msg.AddValue(extOSC.OSCValue.Float(i));
            msg.AddValue(extOSC.OSCValue.Float(i));
            msg.AddValue(extOSC.OSCValue.Float(i));
            Debug.Log(TestMessages["testmessage_"+i.ToString()].Address);
            Transmitter.Send(msg);
        }

        foreach(Transform tf in mocap_transforms)
        {
            // ALL TRANSFORM OBJECTS
            if (tf) {
                // POSITION
                extOSC.OSCMessage message1;
                message1 = new extOSC.OSCMessage("/mocap/" + tf.name.Replace("Robot_","") + "/position");
                message1.AddValue(extOSC.OSCValue.Float(tf.position.x));
                message1.AddValue(extOSC.OSCValue.Float(tf.position.y));
                message1.AddValue(extOSC.OSCValue.Float(tf.position.z));

                //ROTATION
                extOSC.OSCMessage message2;
                message2 = new extOSC.OSCMessage("/mocap/" + tf.name.Replace("Robot_", "") + "/rotation");
                message2.AddValue(extOSC.OSCValue.Float(WrapAngle(tf.localEulerAngles.x)) );
                message2.AddValue(extOSC.OSCValue.Float(WrapAngle(tf.localEulerAngles.y)) );
                message2.AddValue(extOSC.OSCValue.Float(WrapAngle(tf.localEulerAngles.z)) );

                // VELOCITY
                extOSC.OSCMessage message3;
                velocities[index] = (tf.position - lastPositions[index]) / Time.deltaTime;
                message3 = new extOSC.OSCMessage("/mocap/" + tf.name.Replace("Robot_", "") + "/velocity");
                message3.AddValue(extOSC.OSCValue.Float(velocities[index].magnitude));

                // VELOCITY ROTATION
                /*
                extOSC.OSCMessage message4;
                velocities_rotation[index] = (tf.localEulerAngles - lastRotations[index]) / Time.deltaTime;
                message4 = new extOSC.OSCMessage("/mocap/" + tf.name.Replace("Robot_", "") + "/velocity_rotation");
                message4.AddValue(extOSC.OSCValue.Float(velocities_rotation[index].magnitude));
                */
                // send
                Transmitter.Send(message1);
                Transmitter.Send(message2);
                Transmitter.Send(message3);
                //Transmitter.Send(message4);

                // debug
                /*
                if (tf.name == "Robot_RightHand")
                {
                    if (writeToLog) { 
                        Debug.Log(tf.name + " - velocity magnitude: " + velocities[index].magnitude);
                        Debug.Log("pos: " + tf.position);
                        Debug.Log("rot local x: " + WrapAngle(tf.localEulerAngles.x));
                        Debug.Log("rot local y: " + WrapAngle(tf.localEulerAngles.y));
                        Debug.Log("rot local z: " + WrapAngle(tf.localEulerAngles.z));
                    }
                }
                */
                // store current values for next frame
                lastPositions[index] = tf.position;
               // lastRotations[index] = tf.localEulerAngles;

                //
                index++;

                
            }
            
        }

        // VELOCITY ALL
        // sum up all velocities into 1 value
        
        totalVelocity = 0;
        for(int i = 0; i < velocities.Length; i++)
        {
            totalVelocity += velocities[i].magnitude;
        }
        extOSC.OSCMessage message5;
        message5 = new extOSC.OSCMessage("/mocap/VelocityAll");
        message5.AddValue(extOSC.OSCValue.Float(totalVelocity));
        Transmitter.Send(message5);
        

        /*
        // VELOCITY ROTATION ALL
        // sum up all rotation velocities into 1 value
        totalVelocityRotation = 0;
        for (int i = 0; i < velocities_rotation.Length; i++)
        {
            totalVelocityRotation += velocities_rotation[i].magnitude;
        }
        message6.Address = "/mocap/VelocityRotationAll";
        message6.AddValue(extOSC.OSCValue.Float(totalVelocityRotation));
        Transmitter.Send(message6);
        */
        //
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
