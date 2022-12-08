using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    //Public Variables
    [Header("Enemy speed")]
    public float speed = 2.5f;

    //Private Variables
    private GameObject player;
    private Rigidbody2D RB;
    private Vector3 direction;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        direction = (player.transform.position - transform.position).normalized;
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(direction.x, direction.y) * speed;
    }
}
