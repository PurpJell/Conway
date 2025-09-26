using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UndoButtonScript : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        gameManager.UndoMove();
    }
}
