using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SalsaController : MonoBehaviour
{


    private bool isPowerActive;

    private Rigidbody2D rb;

    private float Move;

    private float speed = 3;

    private float jumpForce = 4;

    private bool isGrounded;

    private Animator animator;

    private SpriteRenderer spriteRenderer;

    private HingeJoint2D vineJoint;
    private HingeJoint2D vineJoint2;

    private bool isHoldingVine = false;

    private float swingForce = 1f;

    private float tension = 0f;
    private float maxTension = 8f;
    private float tensionIncrement = 0.2f;

    private float originalVineLength;
    private SpriteRenderer vineSprite;
    private SpriteRenderer vineSprite2;

    private bool isDead = false;

    public float powerRadius = 2f;

    public GameObject powerRadiusVisual;

    private GameObject activePowerRadiusVisual;

    public static int respawnCount = 0;
    private static Vector3 respawnPoint;

    private void Start()
    {

        Debug.Log(respawnCount);
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
        Move = Input.GetAxisRaw("Horizontal");
        Vector2 velocity = new Vector2(Move * speed, rb.velocity.y);
        rb.velocity = velocity;
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            animator.SetBool("inAir", true);
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        if (velocity.x != 0 && isGrounded)
        {
            animator.SetBool("isMoving", true);

            if (Move > 0 && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
            }
            else if (Move < 0 && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool("isMoving", false);
        }

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
            isPowerActive = true;
            EngagePower();
            ActivatePower();
        }
        if(transform.position.y < -10){
            StartCoroutine(Die());
        }
        else
        {
            isPowerActive = false;
            DeactivatePower();
        }
        
    }

    private void ActivatePower()
    {
        if (activePowerRadiusVisual == null)
        {
            activePowerRadiusVisual = Instantiate(powerRadiusVisual, transform.position, Quaternion.identity);
            activePowerRadiusVisual.transform.localScale = new Vector3(powerRadius * 2, powerRadius * 2, 1);
        }
    }

    private void DeactivatePower()
    {
        if (activePowerRadiusVisual != null)
        {
            Destroy(activePowerRadiusVisual);
            activePowerRadiusVisual = null; 
        }
    }

    private void EngagePower()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, powerRadius);

        foreach (var collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                Rigidbody2D enemyRb = collider.GetComponent<Rigidbody2D>();
                if (enemyRb != null)
                {
                    enemyRb.gravityScale = 50;
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
        if (collision.gameObject.name == "Floor" || collision.gameObject.CompareTag("Floor"))
        {
            animator.SetBool("inAir", false);
            isGrounded = true;
        }

        if (collision.gameObject.CompareTag("Spike") && !isDead)
        {
            StartCoroutine(Die());
        }

        if (collision.gameObject.CompareTag("Enemy") && !isPowerActive)
        {
            StartCoroutine(Die());
        }

        if (collision.gameObject.CompareTag("Banana"))
        {
            SceneManager.LoadScene("Level2");
        }


        if (collision.gameObject.CompareTag("Crown"))
        {
            SceneManager.LoadScene("WinScreen");
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

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.name == "Floor" || collision.gameObject.CompareTag("Floor"))
        {
            animator.SetBool("inAir", true);
            isGrounded = false;
        }
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
