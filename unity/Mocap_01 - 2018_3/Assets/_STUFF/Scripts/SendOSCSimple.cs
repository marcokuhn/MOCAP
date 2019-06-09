using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.Events;

namespace Mocap
{
  public class SendOSCSimple : MonoBehaviour
  {
    public OSC oscReference;
    public GUILayout Button;
    [Header("Transform Objects")]

    public GameObject RootObject;
    [TextArea(3, 10)]
    public string ExcludeNames;
    public string tmp;
    public List<Transform> mocap_transforms;

    private Vector3[] lastPositions;
    private float[] velocities;
    private Vector3[] lastRotations;
    private Vector3[] velocities_rotation;
    private int index;
    [Header("Debug")]
    public float totalVelocity;
    public float totalVelocityRotation;

    private OscMessage messageNames;
    private OscMessage messagePositions;
    private OscMessage messageRotations;
    private OscMessage messageVelocities;

    public Dictionary<string, Vector3> Positions;
    public Dictionary<string, Quaternion> Rotations;
    public Dictionary<string, Vector3> VelocitiesDic;
 
    private string tfName;

    void Start()
    {
      lastPositions = new Vector3[mocap_transforms.Count];
      velocities = new float[mocap_transforms.Count];
      lastRotations = new Vector3[mocap_transforms.Count];
      velocities_rotation = new Vector3[mocap_transforms.Count];

      Positions = new Dictionary<string, Vector3>();
      Rotations = new Dictionary<string, Quaternion>();

      messageNames = new OscMessage();
      messagePositions = new OscMessage();
      messageRotations = new OscMessage();
      messageVelocities = new OscMessage();

      messageNames.address = "/mocap/names";
      messagePositions.address = "/mocap/positions";
      messageRotations.address = "/mocap/rotations";
      messageVelocities.address = "/mocap/velocities";
    }

    void Update()
    {
      messageNames.values.Clear();
      messagePositions.values.Clear();
      messageRotations.values.Clear();
      messageVelocities.values.Clear();

      foreach (Transform tf in mocap_transforms)
      {
        // ALL TRANSFORM OBJECTS
        if (tf)
        {
          tfName = tf.name.Replace("Robot_", "");

          //NAMES
          messageNames.values.Add(tfName);

          // POSITION
          Positions[tfName] = tf.position;
          messagePositions.values.Add(tf.position.x);
          messagePositions.values.Add(tf.position.y);
          messagePositions.values.Add(tf.position.z);

          //ROTATION
          Rotations[tfName] = tf.rotation;
          messageRotations.values.Add(WrapAngle(tf.localEulerAngles.x));
          messageRotations.values.Add(WrapAngle(tf.localEulerAngles.y));
          messageRotations.values.Add(WrapAngle(tf.localEulerAngles.z));

          // VELOCITY
          velocities[index] = (tf.position - lastPositions[index]).magnitude / Time.deltaTime;
          messageVelocities.values.Add(velocities[index]);

          //----------------------------------------------------
          lastPositions[index] = tf.position;
          index++;


        } // if transform
      } //foreach

      // VELOCITY TOTAL
      totalVelocity = 0;
      for (int i = 0; i < velocities.Length; i++)
      {
        totalVelocity += velocities[i];
      }

      oscReference.Send(messageNames);
      oscReference.Send(messagePositions);
      oscReference.Send(messageRotations);
      oscReference.Send(messageVelocities);


      /*
        // VELOCITY ROTATION ALL
        // sum up all rotation velocities into 1 value
        totalVelocityRotation = 0;
        for (int i = 0; i < velocities_rotation.Length; i++)
        {
            totalVelocityRotation += velocities_rotation[i].magnitude;
        }
      */

      index = 0;
    } // Update

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


  } // SendOSCSimple
} // Mocap

