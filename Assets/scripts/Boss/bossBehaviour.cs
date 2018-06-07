using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bossBehaviour : MonoBehaviour {

    public GameObject home_base, player, GFL, GFL_Warning, Laser;
    public int jumpPower, health = 500;
    Vector2 velocity, my_position, player_position, base_position;
    private float horizontalDirection, speed = 2.5f;
    private int focuspoint = 0, focusTimer = 200, fireTimer = 0, reloadTimer = 0, chargeTime = 100;
    private bool isGrounded, hasJumped = true;
    Animator animator;

    public bool isWalking, isAttacking;

    // Use this for initialization
    void Start () {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update () {

        my_position = gameObject.transform.position;
        player_position = player.gameObject.transform.position;
        base_position = home_base.gameObject.transform.position;
        velocity = GetComponent<Rigidbody2D>().velocity;

        
        animator.SetFloat("hSpeed",velocity.x *= -1);
        

        focusTimer -= 1;

        if(focusTimer <= 0)
        {
            chooseFocus();
        }

        if (focuspoint == 0)
        {
            Walk();
        }
        else
        {
            Attack();
        }

    }

    private void FixedUpdate()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(horizontalDirection * speed, GetComponent<Rigidbody2D>().velocity.y);
    }

    void chooseFocus()
    {
        int randomNumber = Random.Range(0, 100);

        if ((isGrounded)&&(velocity.y > -0.01)&&(velocity.y < 0.01))
        {
            if ((randomNumber <= 40)&&!(my_position.x <= -14.76))
            {
                focuspoint = 0;
            }
            else
            {
                focuspoint = 1;
            }

            switch (focuspoint)
            {
                case 0: focusTimer = 250;
                    break;
                case 1: focusTimer = 100;
                    break;
            }
        }

    }

    void Walk()
    {

        isWalking = true;
        isAttacking = false;

        Vector2 lineCastPosOld = new Vector2(transform.position.x - 1f, transform.position.y);

        float myWidth = gameObject.GetComponent<SpriteRenderer>().bounds.extents.x;
        Debug.DrawLine(lineCastPosOld, lineCastPosOld + new Vector2(0, -5));
        isGrounded = Physics2D.Linecast(lineCastPosOld, lineCastPosOld + new Vector2(0, -5), 11);

        if ((!animator.GetCurrentAnimatorStateInfo(0).IsName("shoot"))&& (!animator.GetCurrentAnimatorStateInfo(0).IsName("GFLOn")))
        {
            horizontalDirection = -1;
        }

        if ((!isGrounded)&&(!hasJumped)) { jump(); }
        else if(isGrounded){ hasJumped = false; }

    }

    void Attack()
    {
        horizontalDirection = 0.0f;
        isWalking = false;
        isAttacking = true;

        
        float rayStartPoint;
        if (!GetComponent<SpriteRenderer>().flipX) { rayStartPoint = transform.position.x - 2; }
        else { rayStartPoint = transform.position.x - 2; }
        
        RaycastHit2D hit;
        Debug.DrawRay(new Vector2(rayStartPoint, transform.position.y), new Vector2(-1, 0), Color.red);
        hit = Physics2D.Raycast(new Vector2(rayStartPoint, transform.position.y), new Vector2(-1, 0), 12);

        fireTimer -= 1;

        if(fireTimer <= 0)
        {
            animator.speed = 1;
            GFL.SetActive(false);
            if (hit.collider != null)
            {
                if ((hit.collider.tag == "base") || (hit.collider.tag == "Player"))
                {
                    int randomNumber = Random.Range(0, 100);
                    if ((randomNumber <= 40)&&(reloadTimer == 0))
                    {
                        animator.SetBool("shootingLaser", false);
                        animator.SetBool("shootingGFL", true);
                        reloadTimer = 1;
                    }
                    else
                    {
                        animator.SetBool("shootingGFL", false);
                        animator.SetBool("shootingLaser", true);
                        reloadTimer = 0;
                    }
                }
            }
            fireTimer = 301;
        }

        if(chargeTime < 100)
        {
            chargeTime -= 1;
        }

        if ((chargeTime <= 0)&& (animator.speed == 0.0f))
        {
            GFL.SetActive(true);
            GFL_Warning.SetActive(false);
            chargeTime = 100;
        }

    }

    void jump()
    {
        gameObject.GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpPower);
        hasJumped = true;
    }

    void shootGFL()
    {
        animator.speed = 0.0f;
        GFL_Warning.SetActive(true);
        chargeTime -= 1;
    }

    void shootLaser()
    {
        StartCoroutine("Fire");
    }

    IEnumerator Fire()
    {
        for (int i = 0; i < 3; i++)
        {
            if(i == 0)
            {
                var projectile = Instantiate(Laser, new Vector2(transform.position.x - 6f, transform.position.y + 1.6f), transform.rotation);
            }else if (i == 1)
            {
                var projectile = Instantiate(Laser, new Vector2(transform.position.x - 4.5f, transform.position.y + 1.6f), transform.rotation);
            }
            else if(i == 2)
            {
                var projectile = Instantiate(Laser, new Vector2(transform.position.x - 2.5f, transform.position.y + 1.6f), transform.rotation);
            }
            yield return null;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.collider.tag == "bullet")
        {
            Destroy(collision.gameObject);
            health -= 10;
        }

        if(health <= 0)
        {
            Destroy(gameObject);
        }
    }

}
