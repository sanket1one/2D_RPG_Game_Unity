using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float jumpPower;
    [SerializeField] private float jump;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    [SerializeField] private ParticleSystem Dust;
  
    private float Move;

    private Animator anim;
    private Rigidbody2D rb;
    private BoxCollider2D boxCollider;
    private float wallJumpCooldown;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CreateDust();
        Move = Input.GetAxis("Horizontal");


        if (Move > 0.01f) {
            transform.localScale = new Vector3(-1, 1, 1);
        } else if(Move < -0.01f){
            transform.localScale = Vector3.one;
        }


        anim.SetFloat("speed", Mathf.Abs(Move));
        anim.SetBool("grounded", isGrounded());

        if (wallJumpCooldown > 0.2f)
        {
            

            rb.velocity = new Vector2(speed * Move, rb.velocity.y);

            if (onWall() && !isGrounded())
            {
                rb.gravityScale = 0;
                rb.velocity = Vector2.zero;
            }
            else rb.gravityScale = 1;

            if (Input.GetKey(KeyCode.Space))
                Jump();

        }
        else
            wallJumpCooldown += Time.deltaTime;

    }

    private void Jump() {
        if (isGrounded())
        {
            /*            rb.AddForce(new Vector2(rb.velocity.x, jump));*/
            rb.velocity = new Vector2(rb.velocity.x, jumpPower);
            /*            isJumping = true;*/
            anim.SetBool("jump", true);
        }
        else if (onWall() && !isGrounded()) {
            if (Move == 0)
            {
                rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 3, 0);
                transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x),transform.localScale.y, transform.localScale.y);
            }
            else { 
                rb.velocity = new Vector2(Mathf.Sign(transform.localScale.x) * 3, 6);
            }
            wallJumpCooldown = 0;
        }
    }

    private bool isGrounded() {

        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.1f,groundLayer);
        return raycastHit.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, new Vector2(-Mathf.Sign(transform.localScale.x), 0), 0.1f, wallLayer);
        return raycastHit.collider != null;
    }


    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ground"))
        {
            anim.SetBool("jump", false);
        }

    }

    public bool canAttack() {
        return Move == 0 && isGrounded() && !onWall();
    }


    void CreateDust()
    {
        Dust.Play();
    }
}
