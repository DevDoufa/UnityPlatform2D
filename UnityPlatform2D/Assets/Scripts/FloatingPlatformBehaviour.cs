using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingPlatformBehaviour : MonoBehaviour
{
    public float fallTime;
    public float respawnTime;
    private FixedJoint2D joint;
    private Animator anim;
    private Vector2 initPosition;

    void Start()
    {
        joint = GetComponent<FixedJoint2D>();
        anim = GetComponent<Animator>();
        initPosition = new Vector2(transform.position.x, transform.position.y);
    }
    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            Invoke("Fall",fallTime);
            StartCoroutine("WaitAndRespawn");
        }
    }

    void Fall()
    {
        joint.enabled = false;
        anim.SetBool("fall", true);
    }

    IEnumerator WaitAndRespawn()
    {
        yield return new WaitForSeconds(respawnTime);
        anim.SetBool("fall", false);
        transform.position = initPosition;
        joint.enabled = true;
    }

}
