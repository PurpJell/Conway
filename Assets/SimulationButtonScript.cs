using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimulationButtonScript : MonoBehaviour
{
    public bool pressed = false;
    GameManager gameManager;
    Button button;
    
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) // toggle simulation
        {
            OnClick();
        }
    }

    public void OnClick()
    {
        pressed = !pressed;
        if (pressed)
        {
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Stop\n(Space)";
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            button.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Play\n(Space)";
            button.GetComponent<Image>().color = Color.red;
        }

        if(!gameManager.simulationRunning) gameManager.SaveUndoStates();
        gameManager.simulationRunning = !gameManager.simulationRunning;
        gameManager.redoStack.Clear();
    }

}
