using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SubmitBoardSizeButton : MonoBehaviour
{
    private Button submitButton; // Reference to the Button component

    void Start()
    {
        submitButton = GetComponent<Button>(); // Get the Button component
    }

    void Update()
    {
        // Example of triggering the onClick event
        if (Input.GetKeyDown(KeyCode.Return) || (Input.GetKeyDown(KeyCode.KeypadEnter))) // Replace with your condition
        {
            TriggerButtonClick();
        }
    }

    public void TriggerButtonClick()
    {
        if (submitButton != null)
        {
            submitButton.onClick.Invoke(); // Trigger the onClick event
        }
    }
}
