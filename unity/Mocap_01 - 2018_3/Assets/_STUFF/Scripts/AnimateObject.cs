using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateObject : MonoBehaviour
{

    public Vector3 lastPosition;
    public Vector3 velocity;
    public Vector3 _newPosition;
    public float speed = 1.0f;
    public float range = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {


        _newPosition = transform.position;
        _newPosition.x += Mathf.Sin(Time.time * range) * Time.deltaTime * speed;
        transform.position = _newPosition;

        velocity = (gameObject.transform.position - lastPosition) / Time.deltaTime;


        Debug.Log(gameObject.name + " - vel: " + velocity);
        Debug.Log("pos: " + gameObject.transform.position);
        Debug.Log("lastPos: " + lastPosition);
        Debug.Log("vel calc: " + (gameObject.transform.position - lastPosition));
        Debug.Log("vel calc2: " + ((gameObject.transform.position - lastPosition) / 0.01f));
        Debug.Log("-------------------" + Time.deltaTime);

        lastPosition = gameObject.transform.position;
    }
}
