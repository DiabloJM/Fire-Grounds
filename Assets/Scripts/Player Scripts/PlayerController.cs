using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    //Public Variables
    [Header("Properties")]
    public int health = 50;
    public int maxHealth = 50;
    public float speed;

    [Header("UI Text")]
    public Text healthText;

    [Header("Joysticks")]
    public Joystick movementJoystick;
    public Joystick aimingJoystick;

    [Header("Bullet Properties")]
    public Transform[] bulletSpawns;
    public GameObject bullet;
    public float bulletSpeed;
    public float bulletRecoil;
    public float bulletDamage = 1.0f;
    public float damageMultiplayer = 1.0f;

    //Private Variables
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D RB;
    private Animator animator;

    private float moveHorizontal;
    private float moveVertical;

    private float aimHorizontal;
    private float aimVertical;

    private float timer;

    private Transform bulletActualSpawn;
    private Vector2 direction;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        RB = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        timer = bulletRecoil;
        bulletActualSpawn = bulletSpawns[2];
        healthText.text = health.ToString();
        direction = new Vector2(1.0f, 0.0f);
    }

    private void Update()
    {
        moveHorizontal = movementJoystick.Horizontal * speed;
        moveVertical = movementJoystick.Vertical * speed;
   
        aimHorizontal = aimingJoystick.Horizontal;
        aimVertical = aimingJoystick.Vertical;

        if (aimVertical < 0.3f && aimVertical > -0.3f)
        {
            animator.SetBool("lookingRorL", true);
            animator.SetBool("lookingUp", false);
            animator.SetBool("lookingDown", false);

            if (aimHorizontal >= 0.0f)
            {
                //Shoot Right
                spriteRenderer.flipX = false;
                bulletActualSpawn = bulletSpawns[2];
                direction = new Vector2(1.0f * bulletSpeed, 0.0f);
            }
            else
            {
                //Shoot Left
                spriteRenderer.flipX = true;
                bulletActualSpawn = bulletSpawns[6];
                direction = new Vector2(-1.0f * bulletSpeed, 0.0f);
            }
        }
        else if (aimVertical >= 0.3f)
        {
            animator.SetBool("lookingUp", true);
            animator.SetBool("lookingDown", false);
            spriteRenderer.flipX = false;

            if (aimHorizontal < 0.3f && aimHorizontal > -0.3f)
            {
                //Shoot Up
                animator.SetBool("lookingRorL", false);
                bulletActualSpawn = bulletSpawns[0];
                direction = new Vector2(0.0f, 1.0f * bulletSpeed);
            }
            else if (aimHorizontal >= 0.3f)
            {
                //Shoot Top Right
                animator.SetBool("lookingRorL", true);
                bulletActualSpawn = bulletSpawns[1];
                direction = new Vector2(1.0f * bulletSpeed, 1.0f * bulletSpeed);
            }
            else
            {
                //Shoot Top Left
                animator.SetBool("lookingRorL", true);
                spriteRenderer.flipX = true;
                bulletActualSpawn = bulletSpawns[7];
                direction = new Vector2(-1.0f * bulletSpeed, 1.0f * bulletSpeed);
            }
        }
        else
        {
            animator.SetBool("lookingUp", false);
            animator.SetBool("lookingDown", true);
            spriteRenderer.flipX = false;

            if (aimHorizontal < 0.3f && aimHorizontal > -0.3f)
            {
                //Shoot Down
                animator.SetBool("lookingRorL", false);
                bulletActualSpawn = bulletSpawns[4];
                direction = new Vector2(0.0f, -1.0f * bulletSpeed);
            }
            else if (aimHorizontal >= 0.3f)
            {
                //Shoot Low Right
                animator.SetBool("lookingRorL", true);
                bulletActualSpawn = bulletSpawns[3];
                direction = new Vector2(1.0f * bulletSpeed, -1.0f * bulletSpeed);
            }
            else
            {
                //Shoot Low Left
                animator.SetBool("lookingRorL", true);
                spriteRenderer.flipX = true;
                bulletActualSpawn = bulletSpawns[5];
                direction = new Vector2(-1.0f * bulletSpeed, -1.0f * bulletSpeed);
            }
        }

        timer -= Time.deltaTime;

        if(timer <= 0.0f)
        {
            GameObject bulletInstatiated = Instantiate(bullet, bulletActualSpawn.position, Quaternion.identity);
            bulletInstatiated.GetComponent<BulletController>().direction = direction;
            bulletInstatiated.GetComponent<BulletController>().damage = bulletDamage * damageMultiplayer;
            timer = bulletRecoil;
        }
        
        Input.ResetInputAxes();
    }

    private void FixedUpdate()
    {
        RB.velocity = new Vector2(moveHorizontal, moveVertical);
    }

    public void TakeDamage(int _damage)
    {
        health -= _damage;

        if(health <= 0)
        {
            FindObjectOfType<GameManager>().GameOver();
        }

        healthText.text = health.ToString();
    }

    public void UpdateHealthText()
    {
        healthText.text = health.ToString();
    }
}
