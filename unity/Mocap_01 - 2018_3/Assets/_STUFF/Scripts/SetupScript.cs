using UnityEngine;
using System.Collections;

public class SetupScript : MonoBehaviour 
{
    public GameObject obj;
    public Vector3 spawnPoint;

    

    public void SetupStuff()
    {
        Instantiate(obj, spawnPoint, Quaternion.identity);
    }
}