using UnityEngine;

public class FPSCounter : MonoBehaviour
{
    float deltaTime = 0.0f;

    void Update()
    {
        // Calculate deltaTime to get the time it takes to render one frame
        deltaTime += (Time.unscaledDeltaTime - deltaTime) * 0.1f;
    }

    void OnGUI()
    {
        // Calculate FPS
        float msec = deltaTime * 1000.0f;
        float fps = 1.0f / deltaTime;

        // Format the string to display FPS
        string text = string.Format("{0:0.0} ms ({1:0.} fps)", msec, fps);

        // Set style (optional)
        GUIStyle style = new GUIStyle();
        style.fontSize = 15;
        style.normal.textColor = Color.white;

        // Display FPS on the screen
        GUI.Label(new Rect(10, 10, 200, 20), text, style);
    }
}