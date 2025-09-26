using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;
using TMPro;
using SFB;

public class PictureToStructureButton : MonoBehaviour
{
    public GameManager gameManager;
    public BoardManager boardManager;
    public GameObject optionsPanel;
    public Slider slider;
    public TMP_Text sliderLabel;
    public TMP_Text valueText;

    string desktopPath;

    public string copyingMode = "average";

    Texture2D texture; // texture to be loaded from the image
    
    void Start()
    {
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // get the game manager script
        boardManager = GameObject.Find("Board Manager").GetComponent<BoardManager>(); // get the board manager script
        desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop); // path to the desktop
        optionsPanel = GameObject.Find("UI Canvas").transform.Find("Picture Options Panel").gameObject;
        slider = optionsPanel.transform.Find("Threshold Slider").GetComponent<Slider>();
        sliderLabel = optionsPanel.transform.Find("Slider Label (TMP)").GetComponent<TMP_Text>();
        valueText = optionsPanel.transform.Find("Value Text (TMP)").GetComponent<TMP_Text>();

    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            OnButtonClick();
        }
    }

    public void OnButtonClick()
    {
        optionsPanel.SetActive(!optionsPanel.activeSelf);       
    }

    public void UserGetImage()
    {
        gameManager.StopSimulation();

        // Define filters for PNG and JPEG files
        var extensions = new[] {
            new ExtensionFilter("Image Files", "png", "jpg", "jpeg"),
        };

        // Open file dialog and wait for the user to select an image
        string[] paths = StandaloneFileBrowser.OpenFilePanel("Select Image", desktopPath, extensions, false);
        
        if (paths.Length > 0)
        {
            // Assuming the user selected an image, load and process it
            StartCoroutine(LoadImage(paths[0]));
        }
        
        UnityEngine.EventSystems.EventSystem.current.SetSelectedGameObject(null); // unfocus the button
    }

    // Example coroutine to load an image from a path
    IEnumerator LoadImage(string imagePath)
    {
        using (UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + imagePath))
        {
            yield return uwr.SendWebRequest();

            if (uwr.result != UnityWebRequest.Result.Success)
            {
                //Debug.Log(uwr.error);
            }
            else
            {
                // Get downloaded texture
                Texture2D rawTexture = DownloadHandlerTexture.GetContent(uwr);
                texture = ResizeTexture(rawTexture, boardManager.boardSize, boardManager.boardSize);

                slider.gameObject.SetActive(true);
                sliderLabel.gameObject.SetActive(true);
                valueText.gameObject.SetActive(true);

                EvaluatePicture(); // evaluate the picture's pixel brightness and write the structure to a file
            }
        }
    }

    // Method to resize a texture to new width and height
    Texture2D ResizeTexture(Texture2D texture2D, int targetWidth, int targetHeight)
    {
        int width = texture2D.width; // Get the current width of the texture
        int height = texture2D.height; // Get the current height of the texture

        // Determine new dimensions based on the conditions
        int newWidth = width;
        int newHeight = height;

        if (width > targetWidth)
        {
            newWidth = targetWidth;
        }
        if (height > targetHeight)
        {
            newHeight = targetHeight;
        }

        // Proceed with resizing only if necessary
        if (newWidth != width || newHeight != height)
        {
            RenderTexture renderTexture = RenderTexture.GetTemporary(newWidth, newHeight);
            renderTexture.filterMode = FilterMode.Bilinear;
            RenderTexture.active = renderTexture;
            Graphics.Blit(texture2D, renderTexture);
            Texture2D result = new Texture2D(newWidth, newHeight);
            result.ReadPixels(new Rect(0, 0, newWidth, newHeight), 0, 0);
            result.Apply();

            RenderTexture.active = null;
            RenderTexture.ReleaseTemporary(renderTexture);
            return result;
        }
        else
        {
            // Return original texture if no resizing is needed
            return texture2D;
        }
    }

    public void EvaluatePicture() // evaluate the picture's pixel brightness and write the structure to a file
    {
        valueText.text = slider.value.ToString();
        float threshold = slider.value / 100;

        int width = texture.width;
        int height = texture.height;
        float[,] brightnessMatrix = new float[height, width];

        // store pixel brightness in a matrix
        for (int i = 0; i < texture.height; i++)
        {
            for (int j = 0; j < texture.width; j++)
            {
                Color pixel = texture.GetPixel(j, i);
                float brightness = pixel.grayscale;
                brightnessMatrix[i, j] = brightness;
            }
        }

        CopyPixels(brightnessMatrix, threshold, width, height);
    }

    void CopyPixels(float[,] brightnessMatrix, float threshold, int width, int height)
    {
        List<int[]> coppiedCells = new List<int[]>();
        
        for (int i = height - 1; i >= 0; i--)// iterate through the pixels to write the structure to a file
        {
            int[] row = new int[width];
            for (int j = 0; j < width; j++)
            {
                if (brightnessMatrix[i,j] >= threshold)
                {
                    row[j] = 1;
                }
                else
                {
                    row[j] = 0;
                }
            }
            coppiedCells.Add(row);
        }

        gameManager.coppiedCells = coppiedCells;

        gameManager.ResetPastingSettingsAfterCopying();
    }
}
