using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectButtonScript : MonoBehaviour
{
    public bool pressed = false;
    GameManager gameManager;
    Button button;
    PasteButtonScript pasteButtonScript;
    
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        pasteButtonScript = GameObject.Find("Paste Button").GetComponent<PasteButtonScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q)) // selection mode
        {
            SwitchSelectingMode();
        }
    }

    public void SwitchSelectingMode() // switch between drawing and selecting mode
    {
        if (pasteButtonScript.pressed) pasteButtonScript.SwitchPastingMode();

        pressed = !pressed;
        if (pressed)
        {
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            button.GetComponent<Image>().color = new Color(161/255f,161/255f,161/255f);
        }

        gameManager.SwitchSelectingMode();
    }
}
