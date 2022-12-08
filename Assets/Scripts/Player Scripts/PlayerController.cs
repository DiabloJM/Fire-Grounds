using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Public Variables
    [Header("Properties")]
    public int health;
    public float speed;

    [Header("Bullet Properties")]
    public Transform[] bulletSpawns;
    public GameObject bullet;
    public float bulletSpeed;

    //Private Variables
    private int maxHealth;
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private Rigidbody2D RB;
    [SerializeField] private Animator animator;

    [SerializeField] private float moveHorizontal;
    [SerializeField] private float moveVertical; 
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");

        //Going left or right
        if(moveHorizontal != 0.0f && moveVertical == 0.0f)
        {
            animator.SetBool("lookingRorL", true);
            animator.SetBool("lookingUp", false);
            animator.SetBool("lookingDown", false);

            if (moveHorizontal > 0.0f)
            { //Going right
                spriteRenderer.flipX = false;
            }

            if(moveHorizontal < 0.0f)
            { //Going left
                spriteRenderer.flipX = true;
            }
        }

        //Going Up
        if(moveHorizontal == 0.0f && moveVertical > 0.0f)
        {
            animator.SetBool("lookingRorL", false);
            animator.SetBool("lookingUp", true);
            animator.SetBool("lookingDown", false);
            spriteRenderer.flipX = false;
        }

        //Going Down
        if (moveHorizontal == 0.0f && moveVertical < 0.0f)
        {
            animator.SetBool("lookingRorL", false);
            animator.SetBool("lookingUp", false);
            animator.SetBool("lookingDown", true);
            spriteRenderer.flipX = false;
        }

        //Going right/up
        if (moveHorizontal > 0.0f && moveVertical > 0.0f)
        {
            animator.SetBool("lookingRorL", true);
            animator.SetBool("lookingUp", true);
            animator.SetBool("lookingDown", false);
            spriteRenderer.flipX = false;
        }

        //Going right/down
        if (moveHorizontal > 0.0f && moveVertical < 0.0f)
        {
            animator.SetBool("lookingRorL", true);
            animator.SetBool("lookingUp", false);
            animator.SetBool("lookingDown", true);
            spriteRenderer.flipX = false;
        }

        //Going left/up
        if (moveHorizontal < 0.0f && moveVertical > 0.0f)
        {
            animator.SetBool("lookingRorL", true);
            animator.SetBool("lookingUp", true);
            animator.SetBool("lookingDown", false);
            spriteRenderer.flipX = true;
        }

        //Going left/down
        if (moveHorizontal < 0.0f && moveVertical < 0.0f)
        {
            animator.SetBool("lookingRorL", true);
            animator.SetBool("lookingUp", false);
            animator.SetBool("lookingDown", true);
            spriteRenderer.flipX = true;
        }

        //Not moving
        if(moveHorizontal == 0.0f && moveVertical == 0.0f)
        {
            animator.SetBool("lookingRorL", false);
            animator.SetBool("lookingUp", false);
            animator.SetBool("lookingDown", false);
        }
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(moveHorizontal * speed, moveVertical * speed);
    }

    public void FullHealPlayer()
    {
        health = maxHealth;
    }

    public void HealPlayer(int _heal)
    {
        health += _heal;

        if(health > maxHealth)
        {
            health = maxHealth;
        }
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if(health <= 0)
        {
            //Llamar a la funcion del GameMager para terminar la partida

        }
    }
}
