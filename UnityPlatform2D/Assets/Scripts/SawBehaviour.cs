using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SawBehaviour : MonoBehaviour
{

    public enum direction{horizontal, vertical};
    public direction dir;
    public float moveTime;
    public float speed;
    private float timer;
    private int orientation = 1;
    
    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        move();
    }

    void move()
    {
        timer += Time.deltaTime;
        if (timer >= moveTime)
        {
            orientation *= -1;
            timer = 0;
        }
        if (dir == direction.horizontal)
        {
        
            rb.velocity = Vector2.right * orientation * speed;
            //rb.velocity = new Vector2(rb.velocity.x, 0f);
            rb.constraints = RigidbodyConstraints2D.FreezePositionY;
        }
        else
        {
            rb.velocity = Vector2.up * orientation * speed;
            rb.constraints = RigidbodyConstraints2D.FreezePositionX;
        }
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            PlayerBehaviour.inst.Hit();
        }
    }
}
