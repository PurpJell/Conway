using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasteButtonScript : MonoBehaviour
{
    public bool pressed = false;
    GameManager gameManager;
    Button button;
    SelectButtonScript selectButtonScript;
    
    void Start()
    {
        button = GetComponent<Button>();
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
        selectButtonScript = GameObject.Find("Select Button").GetComponent<SelectButtonScript>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V)) // paste cells
        {
            SwitchPastingMode();
        }
    }

    public void SwitchPastingMode() // switch between pasting mode and drawing mode
    {
        if (selectButtonScript.pressed) selectButtonScript.SwitchSelectingMode();

        pressed = !pressed;
        if (pressed)
        {
            button.GetComponent<Image>().color = Color.green;
        }
        else
        {
            button.GetComponent<Image>().color = new Color(161/255f,161/255f,161/255f);
        }

        gameManager.SwitchPastingMode();
    }
}
