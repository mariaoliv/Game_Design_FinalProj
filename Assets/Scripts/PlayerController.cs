using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5.0f;
    
    private bool isMoving;
    private Rigidbody2D rb;
    private Tilemap tilemap;
    private Animator animator;
    private Vector2 input;

    private bool isOpen;

    public AudioSource audioSource;
    
    public PeacockCooldownBar cooldownBar;
    [SerializeField]private float openCoolDown = 5; //once peacock's feathers have been closed, you have to wait 5(?) seconds before being able to open them again
    [SerializeField]private float openTimer; //feathers can only stay open for 3(?) seconds, initally was set to 0

    public HealthBar healthBar;
    [SerializeField] private int maxHealth = 100;
    [SerializeField] private int health = 100;

    //public PeacockCooldownBar openFeathersTimeLeft;
    //[SerializeField] private float maxOpenTimeLeft = 10f;

    [SerializeField] private float bulletSpeed = 5f;
    [SerializeField] private Transform bulletParent;

    [SerializeField] TextMeshProUGUI instructions_text;

    float TESTtimer = 0;

    public GameObject bulletPrefab;

    [SerializeField] Overworld world;

    Vector2 curPos;

    public QuestManager qm;
    

    private void Start()
    {
        instructions_text.text = "";
        animator = GetComponent<Animator>();
        healthBar.SetMaxHealth(maxHealth);
        healthBar.SetHealth(health);
        cooldownBar.SetMaxValue(openCoolDown);
        //openFeathersTimeLeft.SetMaxValue(maxOpenTimeLeft);

        Debug.Log("Complete lvl 1 QS: " + qm.GetQuestState("CompletePlatformerLevel1Quest"));
        
    }

    

    private void Update()
    {
        

        curPos = new Vector2(transform.position.x, transform.position.y);

        // FIRES


        GameObject[] fires = GameObject.FindGameObjectsWithTag("Fire");

        if (fires.Length > 0 && qm.GetQuestState("PutOutFiresQuest") == QuestState.IN_PROGRESS) 
        {
            GameObject nearestFire = findNearestFire(fires);

            float dist = Vector2.Distance(this.transform.position, nearestFire.transform.position);

            if (dist <= 2.5f)
            {
                instructions_text.text = "Press the X key to put out the fire";
                if (Input.GetKeyDown(KeyCode.X))
                {
                    //Destroy(nearestFire, 0.3f);
                    ExtinguishFire(nearestFire);
                }
            }
            else
            {
                instructions_text.text = "";
            }
        }
        else
        {
            instructions_text.text = "";

            if (fires.Length == 0 && qm.GetQuestState("PutOutFiresQuest") == QuestState.IN_PROGRESS)
            {
                GameEventsManager.instance.playerEvents.FiresClear(); 
                // reworking fires quest so it is completed once all fires have been put out instead of having to put out a certain number of fires
            }
        }

        //END OF FIRES

        // PLAYER EVENTS GAIN XP

        // respawn
        if (health <= 0)
        {
            health = 100;
            SceneManager.LoadScene("Overworld");
        }

        if (!isOpen)
        {
            openCoolDown += Time.deltaTime;
        } 
        else
        {
            openTimer += Time.deltaTime;
            
            if (openTimer >= 10)
            {
                isOpen = false;
                animator.SetBool("openFeathers", false);
                openTimer = 0;
                openCoolDown = 0;
            }
        }

        animator.SetBool("isMoving", false);

        if (Input.GetKeyDown(KeyCode.Space) && !isOpen && openCoolDown >= 5)
        {
            isOpen = true;
            animator.SetBool("openFeathers", true);
        } 

        cooldownBar.SetCooldownValue(openCoolDown);

        input.x = Input.GetAxis("Horizontal");
        input.y = Input.GetAxis("Vertical");
        
        if(input == Vector2.zero)
        {
            animator.SetFloat("Horizontal", input.x);
            animator.SetFloat("Vertical", input.y);
        }
        if (input.x < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }
        if (input.x > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        }
        transform.Translate(input * Time.deltaTime * moveSpeed);

        

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            GameObject bullet = Instantiate(bulletPrefab, bulletParent.transform.position, Quaternion.identity);

            Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 shootDirection = (mousePosition - (Vector2)transform.position).normalized;

            Rigidbody2D bulletRB = bullet.GetComponent<Rigidbody2D>();
            bulletRB.velocity = bulletSpeed * shootDirection; //bulletParent.transform.position; 
        }

        healthBar.SetHealth(health);

    }

    IEnumerator Move(Vector2 targetPos)
    {
        isMoving = true;

        //animator.SetBool("isMoving", true);

        while((targetPos - curPos).sqrMagnitude > Mathf.Epsilon)
        {
            transform.position = Vector2.MoveTowards(curPos, targetPos, moveSpeed * Time.deltaTime);
            yield return null;
        }
        curPos = targetPos;
        isMoving = false;

        //animator.SetBool("isMoving", false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Enemy" || collision.gameObject.tag == "Egg Guard")
        {
            //Debug.Log("ouch");
            if (!isOpen)
            {
                health -= 10; // we can adjust this later
                audioSource.Play();

                GameEventsManager.instance.playerEvents.ExperienceGained(PublicVars.xp);
            }
            else
            {
                world.GainXP(10);
                Destroy(collision.gameObject, 0.5f);
            }
        }
    }

    GameObject findNearestFire(GameObject[] fires)
    {
        float minDist = Vector2.Distance(this.transform.position, fires[0].transform.position);
        GameObject nearestFire = fires[0];
        foreach (GameObject fire in fires)
        {
            float dist = Vector2.Distance(this.transform.position, fire.transform.position);
            if (dist < minDist) {
                minDist = dist;
                nearestFire = fire;
            }
        }
        return nearestFire;
    }

    
    void ExtinguishFire(GameObject fire)
    {
        GameEventsManager.instance.miscEvents.FirePutOut();


        //Fire f = fire.gameObject.GetComponent<Fire>();
        //f.putOut();


        Debug.Log("extinguishing fire");

        Destroy(fire, 0.3f); // instead of destroying, set as inactive
    } 
}
