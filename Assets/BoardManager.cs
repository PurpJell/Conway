using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class BoardManager : MonoBehaviour
{
    public GameObject cellPrefab; // cell prefab
    public TMP_InputField boardSizeInputField; // Change the type from GameObject
    public List<List<GameObject>> cells = new List<List<GameObject>>(); // list of all cells
    private List<GameObject> row = new List<GameObject>(); // list of cells in a row
    public int boardSize = 100; // size of the board
    public int cellsSpawned = 0; // number of cells spawned
    public GameObject loadingScreen; // loading screen
    private LoadingScreen loadingScreenScript; // loading screen script

    public GameManager gameManager; // game manager script 

    // Start is called before the first frame update
    void Start()
    {
        boardSizeInputField = GameObject.Find("UI Canvas").transform.Find("Board Size Input Field").transform.GetComponent<TMP_InputField>(); // get the input field for the board size
        loadingScreenScript = loadingScreen.GetComponent<LoadingScreen>(); // get the loading screen script
    }

    public void SetBoardSize()
    {
        if (int.TryParse(boardSizeInputField.text, out int size))
        {
            // Input is in the correct format
            if(size < 10)
            {
                size = 10;
            }
            else if(size > 200)
            {
                size = 200;
            }
        }
        else
        {
            // Input is not in the correct format, default to 100
            size = 100;
        }
        boardSize = size;
        gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>(); // get the game manager script
        StartCoroutine(SpawnCells());
    }

    IEnumerator SpawnCells()
    {
        for (int i = 0; i < boardSize; i++) // spawn cells
        {
            List<GameObject> row = new List<GameObject>();
            for (int j = 0; j < boardSize; j++)
            {
                GameObject cell = Instantiate(cellPrefab, new Vector3(i - boardSize / 2, j - boardSize / 2, 0), Quaternion.identity);
                row.Add(cell);
            }
            cells.Add(row);
            updateLoadingScreen(1.0f*i/boardSize*100);
            yield return null; // Wait for the next frame
        }

        loadingScreen.SetActive(false); // hide the loading screen
    }

    void updateLoadingScreen(float progress)
    {
        loadingScreenScript.UpdateProgress(progress);
    }

    void Update()
    {
        if (cellsSpawned == boardSize * boardSize) // get neighbors after all cells have been spawned
        {
            cellsSpawned = 0;
            foreach (List<GameObject> row in cells)
            {
                foreach (GameObject cell in row)
                {
                    cell.GetComponent<Cell>().GetNeighbors();
                }
            }
        }
    }

    public void ClearBoard(int left, int top, int right, int bottom) // clear the board
    {
        for (int i = left; i <= right; i++)
        {
            for (int j = top; j <= bottom; j++)
            {
                cells[i][j].GetComponent<Cell>().alive = false;
                cells[i][j].GetComponent<Cell>().aliveFor = 0;
            }
        }
        UpdateBoardColor();
    }

    public void FillBoard(int left, int top, int right, int bottom) // fill the board
    {
        for (int i = left; i <= right; i++)
        {
            for (int j = top; j <= bottom; j++)
            {
                cells[i][j].GetComponent<Cell>().alive = true;
                cells[i][j].GetComponent<Cell>().aliveFor = 1;
            }
        }
        UpdateBoardColor();
    }

    public void RandomizeBoard(int left, int top, int right, int bottom) // randomize the board
    {
        for (int i = left; i <= right; i++)
        {
            for (int j = top; j <= bottom; j++)
            {
                cells[i][j].GetComponent<Cell>().RandomState();
            }
        }
        UpdateBoardColor();
    }

    public void UpdateBoardColor() // update the color of all cells
    {
        foreach (List<GameObject> row in cells)
        {
            foreach (GameObject cell in row)
            {
                cell.GetComponent<Cell>().UpdateColor();
            }
        }
    }

    public void UpdateStates() // calculate the next state of all cells
    {
        foreach (List<GameObject> row in cells)
        {
            foreach (GameObject cell in row)
            {
                cell.GetComponent<Cell>().UpdateNeighbors();
            }
        }

        foreach (List<GameObject> row in cells)
        {
            foreach (GameObject cell in row)
            {
                cell.GetComponent<Cell>().UpdateState();
            }
        }
    }

    public void SwitchStates() // switch the state of all cells when going to the next frame
    {
        foreach (List<GameObject> row in cells)
        {
            foreach (GameObject cell in row)
            {
                cell.GetComponent<Cell>().SwitchState();
            }
        }
    }
}
