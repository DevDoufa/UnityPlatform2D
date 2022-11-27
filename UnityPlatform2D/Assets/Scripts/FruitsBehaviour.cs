using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FruitsBehaviour : MonoBehaviour
{
    private SpriteRenderer sr;
    public GameObject effect;
    public float timerToDestroy;

    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        
    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            sr.enabled = false;
            effect.SetActive(true);
            Destroy(gameObject, timerToDestroy);
            CheckpointBehaviour.inst.AddFruit();
        }
    }
}
