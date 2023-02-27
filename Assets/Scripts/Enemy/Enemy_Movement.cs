using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Movement : MonoBehaviour
{
    //Public Variables
    [Header("Properties")]
    public float health;
    public float speed = 2.5f;
    public int damage = 10;
    public int deathValue = 3;

    //Private Variables
    private GameObject player;
    private Rigidbody2D RB;
    private SpriteRenderer sprite;
    private Vector3 direction;
    private bool canAttack = true;
    private float attackRecoil = 2.0f;
    private float timer;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        RB = GetComponent<Rigidbody2D>();
        sprite = GetComponent<SpriteRenderer>();
        timer = attackRecoil;
    }

    private void Update()
    {
        direction = (player.transform.position - transform.position).normalized;

        if(direction.x < 0.0f)
        {
            sprite.flipX = true;
        }
        else
        {
            sprite.flipX = false;
        }

        if(canAttack == false)
        {
            timer -= Time.deltaTime;

            if(timer <= 0.0f)
            {
                canAttack = true;
                timer = attackRecoil;
            }
        }
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(direction.x, direction.y) * speed;
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if(other.gameObject.tag == "Player" && canAttack)
        {
            canAttack = false;
            other.gameObject.GetComponent<PlayerController>().TakeDamage(damage);
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;

        if(health <= 0.0f)
        {
            FindObjectOfType<GameManager>().KillEnemy(deathValue);
            Destroy(gameObject);
        }
    }
}
