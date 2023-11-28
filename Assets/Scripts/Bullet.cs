using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    Rigidbody2D rb;
    public float speed = 5f;
    

    private void OnTriggerEnter2D(Collider2D collision)
    {
        /*
        if (collision.tag == "Enemy")
        {
            Destroy(collision.gameObject);
        } */
        Destroy(this.gameObject, 2);
    }
    
}
