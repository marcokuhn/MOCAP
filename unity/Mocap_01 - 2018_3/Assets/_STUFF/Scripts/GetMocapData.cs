using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mocap;

public class GetMocapData : MonoBehaviour
{
    public GameObject go;
    public List<GameObject> gos;
    public SendOSCSimple mocap;
    public float spacingX = 0.4f;
    public float spacingY = 0.4f;
    public float spacingZ = 0f;
    public float scaleX  = 0.2f;
    public float scaleY = 0.2f;
    public float scaleZ = 0.2f;
    private GameObject ins;
    public float dampen = 2.0f;
    public string BodyPart1 = "LeftHand";
     public string BodyPart2 = "RightHand";
    private List<Vector3> SPositions;
    public float totalPower = 5.0f;
    public float totalPowerRotate = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        gos = new List<GameObject>();
        SPositions = new List<Vector3>();
        mocap = GameObject.Find("OSCManager").GetComponent<SendOSCSimple>();

        for(int i = 0; i < 10; i++){
            for(int j = 0; j < 10; j++){
                ins = Instantiate(go, new Vector3(-3.0f + (i*spacingX), 1.0f + j*spacingY,0.0f), Quaternion.identity, gameObject.transform);
                ins.transform.localScale = new Vector3(scaleX, scaleY, scaleZ);
                ins.AddComponent<Rigidbody>();
                var ins_rg = ins.GetComponent<Rigidbody>();
                ins_rg.useGravity = false;
                ins_rg.drag = 0.85f;
                ins_rg.angularDrag = 2.0f;
                ins_rg.constraints = RigidbodyConstraints.FreezePositionZ;
                ins.name = "go_"+i+"_"+j;
                SPositions.Add(ins.transform.position);
                gos.Add(ins);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        var index = 0;
        foreach( GameObject mgo in gos){
            Rigidbody rb = mgo.GetComponent<Rigidbody>();
            if(index < 50){
                mgo.transform.rotation = Quaternion.Lerp(mgo.transform.rotation, mocap.Rotations[BodyPart1], Time.deltaTime * dampen);
            }else{
                mgo.transform.rotation = Quaternion.Lerp(mgo.transform.rotation, mocap.Rotations[BodyPart2], Time.deltaTime * dampen);
            }
            //rb.angularVelocity = mocap.Rotations[BodyPart].eulerAngles * 0.001f * totalPowerRotate;
            rb.AddForce((SPositions[index] - mgo.transform.position).normalized * totalPower * Time.smoothDeltaTime);
            //Debug.Log((SPositions[index] - mgo.transform.position).normalized * totalPower * Time.smoothDeltaTime);
            index++;
        }
    }
}
