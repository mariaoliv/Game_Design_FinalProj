using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalsaController : MonoBehaviour
{
    public GameWin gameWin;


    private bool isPowerActive;

    private Rigidbody2D rb;

    private float Move;

    private bool facingRight = true;
    public float moveSpeed = 7f;
    private Vector2 currentVelocity = Vector2.zero;
    public float movementSmoothing = .05f;
    public LayerMask groundLayer;

    private float jumpForce = 7;

    private bool isGrounded;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private HingeJoint2D vineJoint;
    private HingeJoint2D vineJoint2;

    private bool isHoldingVine = false;

    public float swingForce = 8f;

    private float tension = 0f;
    private float maxTension = 12f;
    private float tensionIncrement = 2f;

    private float originalVineLength;
    private SpriteRenderer vineSprite;
    private SpriteRenderer vineSprite2;

    private bool isDead = false;

    public float powerRadius = 2f;

    public GameObject powerRadiusVisual;


    public static int respawnCount = 0;
    private static Vector3 respawnPoint;

    private void Start()
    {
        powerRadiusVisual.SetActive(false);
        if (respawnCount != 0)
        {
            transform.position = respawnPoint;
        }
        
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator.SetBool("isGrabbing", false);
        animator.SetBool("isPulling", false);
        animator.SetBool("isDead", false);
    }

    private void Update()
    {
        //Move = Input.GetAxisRaw("Horizontal");
        //Vector2 velocity = new Vector2(Move * speed, rb.velocity.y);
        //rb.velocity = velocity;

        powerRadiusVisual.transform.position = transform.position;

        if (!isHoldingVine)
        {
            float x = Input.GetAxisRaw("Horizontal");

            animator.SetBool("isWalking", x != 0);
            Vector2 targetVelocity = new Vector2(x * moveSpeed, rb.velocity.y);

            rb.velocity = Vector2.SmoothDamp(rb.velocity, targetVelocity, ref currentVelocity, movementSmoothing);
            //player looking direction
            if (x > 0 && !facingRight || x < 0 && facingRight)
            {
                facingRight = !facingRight;
                Vector3 flipScale = transform.localScale;
                flipScale.x *= -1;
                transform.localScale = flipScale;
            }
        }

        //better ground check
        isGrounded = Physics2D.Raycast(transform.position, Vector2.down, 0.5f, groundLayer);
        animator.SetBool("inAir", !isGrounded);

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        //if (velocity.x != 0 && isGrounded)
        //{
            //animator.SetBool("isMoving", true);

            //if (Move > 0 && !spriteRenderer.flipX)
            //{
                //spriteRenderer.flipX = true;
            //}
            //else if (Move < 0 && spriteRenderer.flipX)
            //{
                //spriteRenderer.flipX = false;
            //}
        //}
        //else
        //{
            //animator.SetBool("isMoving", false);
        //}
        
        if (isHoldingVine)
        {
            HandleSwing();
        }

        if (isHoldingVine && !Input.GetMouseButton(0) || Input.GetKey(KeyCode.J))
        {
            animator.SetBool("isPulling", false);
            ReleaseVines();
        }

        if (isHoldingVine && vineJoint != null && vineJoint2 != null && Input.GetKey(KeyCode.S))
        {
            animator.SetBool("isPulling", true);
            IncreaseTension();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            
            StartCoroutine(ShowAndHideCircle());
        }
        if(transform.position.y < -10){
            StartCoroutine(Die());
        }
        
    }

    IEnumerator ShowAndHideCircle()
    {
        powerRadiusVisual.SetActive(true);
        EngagePower();
        yield return new WaitForSeconds(0.1f);
        powerRadiusVisual.SetActive(false);
    }

    private void EngagePower()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, powerRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy") || collider.CompareTag("Eco"))
            {
                rb.velocity = new Vector2(rb.velocity.x, 0);
                rb.AddForce(Vector2.up * (1.2f * jumpForce), ForceMode2D.Impulse);
                Destroy(collider.gameObject);
                if (collider.CompareTag("Eco"))
                {
                    gameWin.ecoBotsKilled++;
                }
            }
        }

    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, powerRadius);
    }


    private void IncreaseTension()
    {
        if (tension < maxTension)
        {
            tension += tensionIncrement;
        }
    }

    private void ReleaseTension()
    {
        if (tension > 0)
        {
            rb.AddForce(Vector2.up * tension, ForceMode2D.Impulse);
            tension = 0;
        }
    }

    private void HandleSwing()
    {
        animator.SetBool("isGrabbing", true);
        float horizontalInput = Input.GetAxis("Horizontal");
        Vector2 force = new Vector2(horizontalInput * swingForce, 0);
        rb.AddForce(force);
    }

    private void ReleaseVines()
    {
        bool released = false;

        if (vineJoint != null)
        {
            Destroy(vineJoint);
            vineJoint = null;
            released = true;
        }

        if (vineJoint2 != null)
        {
            Destroy(vineJoint2);
            vineJoint2 = null;
            released = true;
        }

        if (released)
        {
            rb.gravityScale = 1;
            isHoldingVine = false;

            animator.SetBool("isGrabbing", false);

            ReleaseTension();
        }

    }

    private void HoldVine(Collider2D collision, ref HingeJoint2D vineJointRef)
    {
        vineJointRef = CreateVineJoint(collision);
        vineSprite = collision.gameObject.GetComponent<SpriteRenderer>();
        if (vineJointRef == vineJoint)
        {
            vineSprite = collision.gameObject.GetComponent<SpriteRenderer>();
        }
        else
        {
            vineSprite2 = collision.gameObject.GetComponent<SpriteRenderer>();
        }

    }

    private HingeJoint2D CreateVineJoint(Collider2D collision)
    {
        var vineJoint = gameObject.AddComponent<HingeJoint2D>();
        vineJoint.connectedBody = collision.attachedRigidbody;
        rb.gravityScale = 0;
        isHoldingVine = true;
        return vineJoint;
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Spike") && !isDead)
        {
            StartCoroutine(Die());
        }

        if (collision.gameObject.CompareTag("Enemy") && !isPowerActive)
        {
            StartCoroutine(Die());
        }


    }

    private IEnumerator Die()
    {
        isDead = true;
        rb.velocity = Vector2.zero;
        spriteRenderer.color = Color.red;

        rb.constraints = RigidbodyConstraints2D.FreezeAll;
        animator.SetBool("isDead", true);

        yield return new WaitForSeconds(1f);

        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        //respawnCount++;
        //transform.position = respawnPoint;

    }



    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Vine") && Input.GetMouseButton(0) || Input.GetKey(KeyCode.J))
        {
            if (vineJoint == null)
            {
                HoldVine(collision, ref vineJoint);
            }
            else if (vineJoint2 == null)
            {
                HoldVine(collision, ref vineJoint2);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Vine") && Input.GetMouseButton(0) || Input.GetKey(KeyCode.J))
        {
            if (vineJoint == null)
            {
                HoldVine(collision, ref vineJoint);
            }
            else if (vineJoint2 == null && vineJoint != null)
            {
                HoldVine(collision, ref vineJoint2);
            }
        }
    }

    public void SetRespawnPoint(Vector3 newRespawnPoint)
    {
        respawnPoint = newRespawnPoint;
        respawnCount++;
    }
}
