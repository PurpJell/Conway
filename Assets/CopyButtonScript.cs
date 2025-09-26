using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CopyButtonScript : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.C)) // copy cells
        {
            gameManager.CopyCells();
        }
    }

    public void CopyCells()
    {
        gameManager.CopyCells();
    }
}
