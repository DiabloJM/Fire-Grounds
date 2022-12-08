using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Public Variables
    public Vector2 bulletDirection;

    //Private Variables
    private Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RB.velocity = bulletDirection;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Limit")
        {
            Destroy(this);
        }

        if(other.tag == "Enemy")
        {
            //Damage enemy function
            Destroy(this);
        }
    }
}
