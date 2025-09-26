using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HelpButtonScript : MonoBehaviour
{
    GameManager gameManager;
    public GameObject helpPanel;
    public GameObject helpPanelCloseButton;
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.H)) // selection mode
        {
            SwitchHelpPanel();
        }
    }

    public void SwitchHelpPanel()
    {
        if(helpPanel.activeSelf)
        {
            helpPanelCloseButton.GetComponent<Button>().onClick.Invoke();
        }
        else
        {
            GetComponent<Button>().onClick.Invoke();
        }
        
    }
}
