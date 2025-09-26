using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{
    private Camera cameraComponent;
    public BoardManager boardManager;
    float cameraMinSize = 4.0f;
    float cameraMaxSize = 60.0f;
    float zoom = 1.0f;
    float zoomMin = 1.0f;
    float zoomMax = 10.0f;

    // Start is called before the first frame update
    void Start()
    {
        cameraComponent = this.GetComponent<Camera>();
        boardManager = GameObject.Find("Board Manager").GetComponent<BoardManager>();
    }

    public void AdjustCameraSize()
    {
        cameraMaxSize *= boardManager.boardSize / 100.0f;
        zoomMax *= boardManager.boardSize / 100.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(0, 0.1f * zoom, 0);
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(0, -0.1f * zoom, 0);
        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(-0.1f * zoom, 0, 0);
        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(0.1f * zoom, 0, 0);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (cameraComponent.orthographicSize < cameraMaxSize && cameraComponent.orthographicSize > cameraMinSize)
        {
            cameraComponent.orthographicSize -= scroll * 20 * boardManager.boardSize / 100.0f;
            zoom = (cameraComponent.orthographicSize - cameraMinSize) / (cameraMaxSize - cameraMinSize) * (zoomMax - zoomMin) + zoomMin;
        }
        else if (cameraComponent.orthographicSize >= cameraMaxSize)
        {
            cameraComponent.orthographicSize = cameraMaxSize - 0.01f;
        }
        else if (cameraComponent.orthographicSize <= cameraMinSize)
        {
            cameraComponent.orthographicSize = cameraMinSize + 0.01f;
        }
    }
}
