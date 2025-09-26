using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LoadingScreen : MonoBehaviour
{
    public Slider slider; // slider
    public TMP_Text progressText; // progress text

    // Update is called once per frame
    public void UpdateProgress(float progress)
    {
        slider.value = progress; // update the slider value
        progressText.text = (int)progress + "%"; // update the progress text
    }
}
