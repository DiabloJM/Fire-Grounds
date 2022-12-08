using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //Public Variables
    [Header("Gameobject to Follow")]
    public Transform target;

    [Header("Limits")]
    public float TopLimit;
    public float DownLimit;
    public float RightLimit;
    public float LeftLimit;

    private void Update()
    {
        this.transform.position = new Vector3(target.position.x, target.position.y, this.transform.position.z);

        if(this.transform.position.y > TopLimit)
        {
            this.transform.position = new Vector3(this.transform.position.x, TopLimit, this.transform.position.z);
        }

        if (this.transform.position.y < DownLimit)
        {
            this.transform.position = new Vector3(this.transform.position.x, DownLimit, this.transform.position.z);
        }

        if (this.transform.position.x > RightLimit)
        {
            this.transform.position = new Vector3(RightLimit, this.transform.position.y, this.transform.position.z);
        }

        if (this.transform.position.x < LeftLimit)
        {
            this.transform.position = new Vector3(LeftLimit, this.transform.position.y, this.transform.position.z);
        }
    }
}
