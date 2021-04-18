using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{

    public Rigidbody2D rb;
    public float Speed;
    public float JumpForce;
    [Header("Ground Check")]
    bool IsGrounded;
    public Transform GroundCheck;
    public float CheckRaduis;
    public LayerMask GroundLayer;
    [Header("Coin")]
    [HideInInspector] public int Coins;
    public Text CoinsText;
    public GameObject CoinParticle;
    [Header("Animations")]
    public Animator anim;
    void Update()
    {
        Move();
        GroundChecker();
        Jump();
        Attack();
    }

    void Move()
    {
        //Moveing the player on the x axis
        float x = Input.GetAxisRaw("Horizontal");
        float moveBy = x * Speed;
        rb.velocity = new Vector2(moveBy, rb.velocity.y);
        //-1 1 0
        if (x == -1)
        {
            anim.SetBool("IsIdle", false);
            transform.localScale = new Vector3(-1, 1);
        }else if (x == 1)
        {
            transform.localScale = new Vector3(1, 1);
            anim.SetBool("IsIdle", false);
        }
        else if (x == 0)
        {
            anim.SetBool("IsIdle", true);
        }
    }
    void Jump()
    {
        if (Input.GetKey(KeyCode.UpArrow) && IsGrounded == true)
        {
            rb.velocity = new Vector2(rb.velocity.x, JumpForce);
        }
    }
    void GroundChecker()
    {
        Collider2D coll = Physics2D.OverlapCircle(GroundCheck.position, CheckRaduis, GroundLayer);
        if(coll != null)
        {
            IsGrounded = true;
        }
        else
        {
            IsGrounded = false;
        }
        
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Coin"))
        {
            UpdateCoins(1);
            GameObject Particle = Instantiate(CoinParticle, other.transform.position, Quaternion.identity);
            Destroy(Particle, 0.5f);
            Destroy(other.gameObject);
        }
    }

    void UpdateCoins(int amount)
    {
        Coins += amount;
        CoinsText.text = "Coins: " + Coins;
    }
    [Header("Attack")]
    public Transform HitPos;
    public float HitRad;
    public LayerMask EnemyLayermask;
    void Attack()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Collider2D[] AttackInfo = Physics2D.OverlapCircleAll(HitPos.position, HitRad, EnemyLayermask);
            foreach (Collider2D enemy in AttackInfo)
            {
                Destroy(enemy.gameObject);
            }
        }
    }
}
