using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    BoardManager boardManager; // reference to the board manager script

    bool pencilOnBoard = false; // if true, enables drawing behind UI elements when drawing was started on the board

    public GameObject markedCell1; // the first cell selected in selection mode
    public Cell cell1Script; // script of the first cell selected in selection mode
    public GameObject markedCell2; // the second cell selected in selection mode
    public Cell cell2Script; // script of the second cell selected in selection mode

    public List<int[]> coppiedCells = new List<int[]>(); // stores the copied cells

    public int pastingPivotedX = 0; // 0 if the structure is not pivoted, 1 if it is pivoted
    public int pastingRotatedX = 0; // 0 if the structure is not rotated, 1 if it is rotated

    public int pastingPivotedY = 0; // 0 if the structure is not pivoted, 1 if it is pivoted
    public int pastingRotatedY = 0; // 0 if the structure is not rotated, 1 if it is rotated

    public bool pastingTransposed = false; // if true, the structure will be pasted transposed

    public bool pastingInverted = false; // if true, dead cells will be pasted as alive and vice versa

    public bool pastingIgnoreEmpty = true; // if true, empty cells will not be pasted

    public string gameState = "drawing"; // drawing, selecting, pasting
    public bool simulationRunning = false; // if true, the simulation is running

    public GameObject simulationButton; // reference to the simulation button
    public GameObject helpPanelCloseButton; // reference to the close button of the help panel
    public GameObject helpButton; // reference to the help button

    private List<int[,]> undoStack = new List<int[,]>(); // stack of previous states
    public List<int[,]> redoStack = new List<int[,]>(); // stack of states that were undone

    // Start is called before the first frame update
    void Start()
    {
        boardManager = GameObject.Find("Board Manager").GetComponent<BoardManager>(); // get the board manager script
        helpButton = GameObject.Find("UI Canvas").transform.Find("Button Panel").transform.Find("Help Button").gameObject; // get the help button
        helpPanelCloseButton = GameObject.Find("UI Canvas").transform.Find("Help Panel").transform.Find("Close Button").gameObject; // get the close button of the help panel
    }

    // Update is called once per frame
    void Update()
    {
        HandleInput();

        if (simulationRunning) // update cells every frame or when pressing N
        {
            NextFrame();
        }
    }

    void HandleInput() 
    {
        if (Input.GetKeyDown(KeyCode.H) || (Input.GetKeyDown(KeyCode.Escape) && helpPanelCloseButton.activeSelf)) 
        {
            SwitchHelpPanel();
        }


        if (gameState == "selecting") 
        {
            if (Input.GetMouseButtonUp(0)) 
            {
                pencilOnBoard = false; 
            }
            
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D clickedCollider = Physics2D.OverlapPoint(mousePosition);
                if (clickedCollider != null && clickedCollider.CompareTag("Cell"))
                {
                    pencilOnBoard = true;
                    markedCell1 = clickedCollider.gameObject;
                    cell1Script = markedCell1.GetComponent<Cell>();
                    cell1Script.spriteRenderer.color = Color.yellow;
                }
            }
            if (Input.GetMouseButton(0) && pencilOnBoard) 
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D clickedCollider = Physics2D.OverlapPoint(mousePosition);
                if (clickedCollider != null && clickedCollider.CompareTag("Cell"))
                {
                    markedCell2 = clickedCollider.gameObject;
                    cell2Script = markedCell2.GetComponent<Cell>();
                    cell2Script.spriteRenderer.color = Color.yellow;
                }
            }

            HighlightSelectedCells(); 
        }
        else if (gameState == "pasting") 
        {
            if (Input.GetMouseButtonDown(0) && !EventSystem.current.IsPointerOverGameObject()) 
            {
                PasteCells();
            }
            else if (Input.GetMouseButtonDown(1) && !EventSystem.current.IsPointerOverGameObject())
            {
                PivotStructure();
            }
            else if (Input.GetKeyDown(KeyCode.E)) 
            {
                RotateStructure();
            }
            else if (Input.GetKeyDown(KeyCode.X)) 
            {
                pastingIgnoreEmpty = !pastingIgnoreEmpty;
            }
            else if (Input.GetKeyDown(KeyCode.I)) 
            {
                pastingInverted = !pastingInverted;
            }
            else if (Input.GetKeyDown(KeyCode.T)) 
            {
                pastingTransposed = !pastingTransposed;
            }

            PasteCells(true); 
        }
        else 
        {

            if ((Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)) && !EventSystem.current.IsPointerOverGameObject()) 
            {
                if(!simulationRunning) 
                {
                    SaveUndoStates();
                    redoStack.Clear();
                }
                pencilOnBoard = true;
            }
            else if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) 
            {
                pencilOnBoard = false;
            }

            if(Input.GetMouseButton(0) && pencilOnBoard) 
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D clickedCollider = Physics2D.OverlapPoint(mousePosition);
                if (clickedCollider != null)
                {
                    clickedCollider.GetComponent<Cell>().HandleLeftClick();
                }
            }            
            else if(Input.GetMouseButton(1) && pencilOnBoard) 
            {
                Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                Collider2D clickedCollider = Physics2D.OverlapPoint(mousePosition);
                if (clickedCollider != null)
                {
                    clickedCollider.GetComponent<Cell>().HandleRightClick();
                }
            }
        }
    }
    
    public void NextFrame()
    {
        if(!simulationRunning) 
        {
            SaveUndoStates(); // save the current state to the undo stack
            redoStack.Clear(); // clear the redo stack
        }
        boardManager.UpdateStates(); // update the states of the cells
        boardManager.SwitchStates(); // switch the states of the cells
        if (markedCell1 != null && markedCell2 != null) HighlightSelectedCells(); // highlight the selected cells
        if (gameState == "pasting") PasteCells(true); // preview mode
    }

    public void CopyCells() // copy the selected cells
    {
        int left;
        int right;
        int top;
        int bottom;

        if (markedCell1 == null || markedCell2 == null) // copy the whole board
        {
            left = 0;
            right = boardManager.boardSize - 1;
            top = boardManager.boardSize - 1;
            bottom = 0;
        }
        else // copy the selected cells
        {
            left = Mathf.Min(cell1Script.x, cell2Script.x);
            right = Mathf.Max(cell1Script.x, cell2Script.x);
            top = Mathf.Max(cell1Script.y, cell2Script.y);
            bottom = Mathf.Min(cell1Script.y, cell2Script.y);
        }

        int columns = right - left + 1;
        int rows = top - bottom + 1;

        coppiedCells.Clear();
        
        for(int i = top; i >= bottom; i--) // copy the cells
        {
            int[] row = new int[columns];
            for(int j = left; j <= right; j++)
            {
                Cell cellScript = boardManager.cells[j][i].GetComponent<Cell>();
                row[j - left] = cellScript.aliveFor;
            }
            coppiedCells.Add(row);
        }
        ResetPastingSettingsAfterCopying(); // reset the pasting settings
    }

    public void RandomizeCells() // randomize the selected or all cells
    {
        if(!simulationRunning) 
        {
            SaveUndoStates(); // save the current state to the undo stack
            redoStack.Clear(); // clear the redo stack
        }
        if (markedCell1 == null || markedCell2 == null) // randomize the whole board
        {
            boardManager.RandomizeBoard(0, 0, boardManager.boardSize - 1, boardManager.boardSize - 1);
            return;
        }

        int left = Mathf.Min(cell1Script.x, cell2Script.x);
        int right = Mathf.Max(cell1Script.x, cell2Script.x);
        int top = Mathf.Max(cell1Script.y, cell2Script.y);
        int bottom = Mathf.Min(cell1Script.y, cell2Script.y);

        boardManager.RandomizeBoard(left, bottom, right, top); // randomize the selected cells
    }

    public void ClearCells() // clear the selected or all cells
    {
        if(!simulationRunning) 
        {
            SaveUndoStates(); // save the current state to the undo stack
            redoStack.Clear(); // clear the redo stack
        }
        pencilOnBoard = false;
        if (markedCell1 == null || markedCell2 == null) // clear the whole board
        {
            boardManager.ClearBoard(0, 0, boardManager.boardSize - 1, boardManager.boardSize - 1);
            return;
        }

        int left = Mathf.Min(cell1Script.x, cell2Script.x);
        int right = Mathf.Max(cell1Script.x, cell2Script.x);
        int top = Mathf.Max(cell1Script.y, cell2Script.y);
        int bottom = Mathf.Min(cell1Script.y, cell2Script.y);

        boardManager.ClearBoard(left, bottom, right, top); // clear the selected cells
        ResetMarkedCells();
    }

    public void FillCells() // fill the selected or all cells
    {
        if(!simulationRunning) 
        {
            SaveUndoStates(); // save the current state to the undo stack
            redoStack.Clear(); // clear the redo stack
        }
        pencilOnBoard = false;
        if (markedCell1 == null || markedCell2 == null) // fill the whole board
        {
            boardManager.FillBoard(0, 0, boardManager.boardSize - 1, boardManager.boardSize - 1);
            return;
        }

        int left = Mathf.Min(cell1Script.x, cell2Script.x);
        int right = Mathf.Max(cell1Script.x, cell2Script.x);
        int top = Mathf.Max(cell1Script.y, cell2Script.y);
        int bottom = Mathf.Min(cell1Script.y, cell2Script.y);

        boardManager.FillBoard(left, bottom, right, top); // fill the selected cells
        ResetMarkedCells();
    }

    void ResetMarkedCells() // reset the selected cells
    {
        if(markedCell1 != null) 
        {
            markedCell1 = null;
            cell1Script = null;
        }
        if (markedCell2 != null)
        {
            markedCell2 = null;
            cell2Script = null;
        }
    }

    void PivotStructure() // pivot the structure to be pasted around the mouse position
    {
        if (pastingPivotedX == 0 && pastingPivotedY == 0) // bottom right -> bottom left
        {
            pastingPivotedX = 1;
            pastingPivotedY = 0;
        }
        else if (pastingPivotedX == 1 && pastingPivotedY == 0) // bottom left -> top left
        {
            pastingPivotedX = 1;
            pastingPivotedY = 1;
        }
        else if (pastingPivotedX == 1 && pastingPivotedY == 1) // top left -> top right
        {
            pastingPivotedX = 0;
            pastingPivotedY = 1;
        }
        else if (pastingPivotedX == 0 && pastingPivotedY == 1) // top right -> bottom right
        {
            pastingPivotedX = 0;
            pastingPivotedY = 0;
        }
    }

    void RotateStructure() // rotate the structure to be pasted
    {
        if (pastingRotatedX == 0 && pastingRotatedY == 0) // bottom right -> bottom left
        {
            pastingRotatedX = 1;
            pastingRotatedY = 0;
        }
        else if (pastingRotatedX == 1 && pastingRotatedY == 0) // bottom left -> top left
        {
            pastingRotatedX = 1;
            pastingRotatedY = 1;
        }
        else if (pastingRotatedX == 1 && pastingRotatedY == 1) // top left -> top right
        {
            pastingRotatedX = 0;
            pastingRotatedY = 1;
        }
        else if (pastingRotatedX == 0 && pastingRotatedY == 1) // top right -> bottom right
        {
            pastingRotatedX = 0;
            pastingRotatedY = 0;
        }
    }

    void PasteCells(bool previewMode = false) 
    {
        if(!simulationRunning && !previewMode) 
        {
            SaveUndoStates();
            redoStack.Clear(); 
        }
        if (previewMode) boardManager.UpdateBoardColor(); 

        int rows = coppiedCells.Count;
        int columns = coppiedCells[0].Length;
        int left;
        int top;

        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D clickedCollider = Physics2D.OverlapPoint(mousePosition);
        if (clickedCollider != null)
        {
            Cell cellScript = clickedCollider.GetComponent<Cell>();
            left = cellScript.x - pastingPivotedX * columns + pastingPivotedX + pastingRotatedY * columns - pastingRotatedY;
            top = cellScript.y + pastingPivotedY * rows - pastingPivotedY - pastingRotatedX * rows + pastingRotatedX;
        }
        else return;

        Cell thisCellScript;

        for (int i = 0; i < rows; i++) 
        {
            for (int j = 0; j < columns; j++)
            {
                if (!pastingTransposed) 
                {
                    if((left + j * ((0 - pastingRotatedY) == 0 ? 1 : -1)) >= boardManager.boardSize || (top - i * ((0 - pastingRotatedX) == 0 ? 1 : -1)) >= boardManager.boardSize || (left + j * ((0 - pastingRotatedY) == 0 ? 1 : -1)) < 0 || (top - i * ((0 - pastingRotatedX) == 0 ? 1 : -1)) < 0) continue;
                    thisCellScript = boardManager.cells[left + j * ((0 - pastingRotatedY) == 0 ? 1 : -1)][top - i * ((0 - pastingRotatedX) == 0 ? 1 : -1)].GetComponent<Cell>();
                }
                else 
                {
                    if((left + i * ((0 - pastingRotatedX) == 0 ? 1 : -1)) >= boardManager.boardSize || (top + j * ((0 - pastingRotatedY) == 0 ? 1 : -1)) >= boardManager.boardSize || (left + i * ((0 - pastingRotatedX) == 0 ? 1 : -1)) < 0 || (top + j * ((0 - pastingRotatedY) == 0 ? 1 : -1)) < 0) continue;
                    thisCellScript = boardManager.cells[left + i * ((0 - pastingRotatedX) == 0 ? 1 : -1)][top + j * ((0 - pastingRotatedY) == 0 ? 1 : -1)].GetComponent<Cell>();
                }
                if (coppiedCells[i][j] > 0)
                {
                    if(previewMode) 
                    {
                        if (!pastingInverted) thisCellScript.spriteRenderer.color = Color.green;
                        else if (!pastingIgnoreEmpty) thisCellScript.spriteRenderer.color = Color.yellow;
                    }
                    else 
                    {
                        if (!pastingInverted) 
                        {
                            thisCellScript.alive = true;
                            thisCellScript.aliveFor = coppiedCells[i][j];
                        }
                        else if (!pastingIgnoreEmpty) 
                        {
                            thisCellScript.alive = false;
                            thisCellScript.aliveFor = 0;
                        }
                    }
                }
                else
                {
                    if (previewMode) 
                    {
                        if(pastingInverted) thisCellScript.spriteRenderer.color = Color.green;
                        else if (!pastingIgnoreEmpty) thisCellScript.spriteRenderer.color = Color.yellow;
                    }
                    else 
                    {
                        if(pastingInverted) 
                        {
                            thisCellScript.alive = true;
                            thisCellScript.aliveFor = 1;
                        }
                        else if (!pastingIgnoreEmpty) 
                        {
                            thisCellScript.alive = false;
                            thisCellScript.aliveFor = 0;
                        }
                    }
                }
            }
        }
        if (!previewMode) boardManager.UpdateBoardColor(); 
    }

    public void HighlightSelectedCells() // draw a rectangle around the selected cells (inclusive)
    {
        if (markedCell1 != null && markedCell2 != null) // if cells are selected
        {
            boardManager.UpdateBoardColor(); // reset the board colors before highlighting the selected cells

            int left = Mathf.Min(cell1Script.x, cell2Script.x);
            int right = Mathf.Max(cell1Script.x, cell2Script.x);
            int top = Mathf.Max(cell1Script.y, cell2Script.y);
            int bottom = Mathf.Min(cell1Script.y, cell2Script.y);

            for (int i = left; i <= right; i++) // highlight the selected cells
            {
                for (int j = bottom; j <= top; j++)
                {
                    if ( ((i == left || i == right) && j <= top && j >= bottom) || ((j == top || j == bottom) && i <= right && i >= left) )
                    {
                        boardManager.cells[i][j].GetComponent<Cell>().spriteRenderer.color = Color.yellow;
                    }
                }

            }
        }
    }

    // game state switching functions

    public void SwitchPastingMode() // switch between drawing and pasting mode
    {
        if (gameState != "pasting") // enable pasting mode
        {
            gameState = "pasting";
        }
        else // quit pasting mode
        {
            gameState = "drawing";
        }

        ResetMarkedCells();

        simulationRunning = false;
        pencilOnBoard = false;
        boardManager.UpdateBoardColor();
        //print(gameState);
    }

    public void SwitchSelectingMode() // switch between drawing and selecting mode
    {
        simulationRunning = false;
        pencilOnBoard = false;
        if (gameState != "selecting") // enable selection mode
        {
            gameState = "selecting";
        }
        else // quit selection mode
        {
            ResetMarkedCells();
            gameState = "drawing";
        }
        boardManager.UpdateBoardColor();
        //print(gameState);
    }

    public void StopSimulation() // switch simulation off (for Help Button)
    {
        simulationRunning = false;
        simulationButton.GetComponentInChildren<TMPro.TextMeshProUGUI>().text = "Play\n(Space)";
        simulationButton.GetComponent<Image>().color = Color.red;
        simulationButton.GetComponent<SimulationButtonScript>().pressed = false;
    }

    public void SwitchHelpPanel() // open/close the help panel
    {
        if(helpPanelCloseButton.activeSelf) // close the help panel
        {
            helpPanelCloseButton.GetComponent<Button>().onClick.Invoke();
        }
        else // open the help panel
        {
            helpButton.GetComponent<Button>().onClick.Invoke();
        }
        
    }

    public void ResetPastingSettingsAfterCopying() // reset the pasting settings after copying
    {
        pastingPivotedX = 0;
        pastingRotatedX = 0;
        pastingPivotedY = 0;
        pastingRotatedY = 0;
        pastingTransposed = false;
    }

    public void SaveUndoStates() // save the current states to the undo stack
    {
        int[,] states = new int[boardManager.boardSize, boardManager.boardSize];
        for (int i = 0; i < boardManager.boardSize; i++)
        {
            for (int j = 0; j < boardManager.boardSize; j++)
            {
                states[i, j] = boardManager.cells[i][j].GetComponent<Cell>().aliveFor;
            }
        }
        undoStack.Add(states);
    }

    public void SaveRedoStates() // save the current states to the redo stack after undoing
    {
        int[,] states = new int[boardManager.boardSize, boardManager.boardSize];
        for (int i = 0; i < boardManager.boardSize; i++)
        {
            for (int j = 0; j < boardManager.boardSize; j++)
            {
                states[i, j] = boardManager.cells[i][j].GetComponent<Cell>().aliveFor;
            }
        }
        redoStack.Add(states);
    }

    public void UndoMove() // undo the last move
    {
        if (undoStack.Count > 0)
        {
            if(!simulationRunning) SaveRedoStates();
            StopSimulation();
            int[,] states = undoStack[undoStack.Count - 1];
            undoStack.RemoveAt(undoStack.Count - 1);
            for (int i = 0; i < boardManager.boardSize; i++)
            {
                for (int j = 0; j < boardManager.boardSize; j++)
                {
                    if (states[i,j] > 0) boardManager.cells[i][j].GetComponent<Cell>().alive = true;
                    else boardManager.cells[i][j].GetComponent<Cell>().alive = false;
                    boardManager.cells[i][j].GetComponent<Cell>().aliveFor = states[i, j];
                }
            }
            boardManager.UpdateBoardColor();
        }
    }

    public void RedoMove() // redo the last move
    {
        if (redoStack.Count > 0)
        {
            SaveUndoStates();
            int[,] states = redoStack[redoStack.Count - 1];
            redoStack.RemoveAt(redoStack.Count - 1);
            for (int i = 0; i < boardManager.boardSize; i++)
            {
                for (int j = 0; j < boardManager.boardSize; j++)
                {
                    if (states[i, j] > 0) boardManager.cells[i][j].GetComponent<Cell>().alive = true;
                    else boardManager.cells[i][j].GetComponent<Cell>().alive = false;
                    boardManager.cells[i][j].GetComponent<Cell>().aliveFor = states[i, j];
                }
            }
            boardManager.UpdateBoardColor();
        }
    }
}
