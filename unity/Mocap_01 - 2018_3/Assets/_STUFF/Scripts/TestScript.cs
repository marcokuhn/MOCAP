using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestScript : MonoBehaviour {
    public GameObject Test1;
    private GameObject Test2;
    [SerializeField]
    private float Test3;

	// Use this for initialization
	void Start () {
        Debug.Log("test start");
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log("test update" + transform.position.x);
        
    }
}
