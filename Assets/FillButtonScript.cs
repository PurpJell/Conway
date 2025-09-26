using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FillButtonScript : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        gameManager.FillCells();
    }
}
