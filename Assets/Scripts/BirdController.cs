using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdController : MonoBehaviour
{
    float speed = 1f;
    public float leftBoundary = -10.0f;
    public float rightBoundary = 10.0f;
    private int direction = 1;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;

    public float spawnPoint;

    // Start is called before the first frame update
    void Start()
    {
        spawnPoint = transform.position.x;
        rb = GetComponent<Rigidbody2D>();
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        MoveLeftAndRight();
    }
    void MoveLeftAndRight()
    {
        float newPosition = transform.position.x + direction * speed * Time.deltaTime;
        if (newPosition > spawnPoint + rightBoundary)
        {
            direction = -1;
        }
        else if (newPosition < spawnPoint + leftBoundary)
        {
            direction = 1;
        }
        Vector2 move = new Vector2(direction * speed, 0);
        rb.velocity = move;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Spike"))
        {
            spriteRenderer.color = Color.red;
            Destroy(gameObject, 0.5f);
        }
    }
}
