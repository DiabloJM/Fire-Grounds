using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    //Public Variables
    public float damage;
    public Vector2 direction = new Vector2(0.0f, 0.0f);

    //Private Variables
    private Rigidbody2D RB;

    private void Awake()
    {
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        RB.velocity = direction;
    }
    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Limit")
        {
            Destroy(gameObject);
        }

        if(other.gameObject.tag == "Enemy")
        {
            other.gameObject.GetComponent<Enemy_Movement>().TakeDamage(damage);
            Destroy(gameObject);
        }
    }
}
