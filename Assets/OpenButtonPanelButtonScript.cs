using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenButtonPanelButtonScript : MonoBehaviour
{
    public GameObject buttonPanel;
    public void MovePanel()
    {
        Vector3 newPosition = buttonPanel.transform.position;
        newPosition.x += 300;
        buttonPanel.transform.position = newPosition;
    }
}
