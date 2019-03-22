using UnityEngine;
using System.Collections;

public class ReceiveOSC : MonoBehaviour {
    
   	public OSC oscr;


	// Use this for initialization
	void Start () {
	   oscr.SetAddressHandler( "/CubeXYZ" , OnReceive_XYZ );
       oscr.SetAddressHandler("/CubeX", OnReceive_X);
       //oscr.SetAddressHandler("/CubeY", OnReceive_Y);
       //oscr.SetAddressHandler("/CubeZ", OnReceive_Z);
    }
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnReceive_XYZ(OscMessage message){
		float x = message.GetFloat(0);
        float y = message.GetFloat(1);
		float z = message.GetFloat(2);

		transform.position = new Vector3(x,y,z);
	}


    void OnReceive_X(OscMessage message) {
        float x = message.GetFloat(0);
        Debug.Log("x: " + x);
        Vector3 position = transform.position;

        position.x = x;

        transform.position = position;
    }
/*
    void OnReceive_Y(OscMessage message) {
        float y = message.GetFloat(0);

        Vector3 position = transform.position;

        position.y = y;

        transform.position = position;
    }

    void OnReceive_Z(OscMessage message) {
        float z = message.GetFloat(0);

        Vector3 position = transform.position;

        position.z = z;

        transform.position = position;
    }

*/
}