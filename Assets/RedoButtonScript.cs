using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedoButtonScript : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Y))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        gameManager.RedoMove();
    }
}
