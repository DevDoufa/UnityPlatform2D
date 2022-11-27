using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class CheckpointBehaviour : MonoBehaviour
{
    private Animator anim;
    public string nextScene;

    public int fruitsRequirement;

    private int ownedFruits;

    public static CheckpointBehaviour inst;
    public TMP_Text txt;

    void Start()
    {
        anim = GetComponent<Animator>();
        inst = this;
    }

    void Update()
    {
        if (ownedFruits < fruitsRequirement)
        {
            txt.text = "Collect all fruits";
        }
        else{
            txt.text = "Go to the finish line";
        }
            
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            anim.SetTrigger("collision");
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.tag == "Player")
        {
            if (ownedFruits >= fruitsRequirement)
            {
                SceneManager.LoadScene(nextScene);
            }
            
        }
    }

    public void AddFruit()
    {
        ownedFruits++;
    }


}

