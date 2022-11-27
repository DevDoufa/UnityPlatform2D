using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrampolineBehaviour : MonoBehaviour
{

    private Animator anim;
    public float impulseForce;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("jump");
            col.rigidbody.AddForce(Vector2.up * impulseForce, ForceMode2D.Impulse);
        }
    }
}
