using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{

    public GameObject gameOver;
    public static GameController inst;

    void Awake()
    {
        inst = this;
    }

    void Update()
    {
        if (gameOver.activeSelf == true)
        {
            if (Input.GetButton("Jump"))
            {
                RestartScene();
            }
        }
    }

    public void GameOver()
    { 
        CheckpointBehaviour.inst.txt.enabled = false;
        gameOver.SetActive(true);
    }
    public void RestartScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
