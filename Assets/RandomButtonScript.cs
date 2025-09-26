using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RandomButtonScript : MonoBehaviour
{
    GameManager gameManager;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) // selection mode
        {
            OnClick();
        }
    }

    public void OnClick() // switch between drawing and selecting mode
    {
        gameManager.RandomizeCells();
    }
}
