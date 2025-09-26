using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Cell : MonoBehaviour
{
    public SpriteRenderer spriteRenderer; // sprite renderer of the cell
    public bool alive = false; // is the cell alive
    public bool nextAlive = false; // will the cell be alive
    public List<GameObject> neighbors = new List<GameObject>(); // list of neighbors
    public BoardManager boardManager; // board manager script
    public int x; // x position of the cell in the board matrix
    public int y; // y position of the cell in the board matrix
    public int aliveNeighbors = 0; // number of alive neighbors
    public int aliveFor = 0; // how long the cell has been alive (0 - dead, 1 - first frame of being alive...)

    public static class Colors // colors for the cell based on how long it has been alive
    {
        public static readonly Color32 red = new Color32(0xFF, 0x00, 0x36, 0xFF);
        public static readonly Color32 lightRed = new Color32(0xFF, 0x84, 0x83, 0xFF);
        public static readonly Color32 lighterRed = new Color32(0xFF, 0xB3, 0xA7, 0xFF);
        public static readonly Color32 white = new Color32(0xFF, 0xFF, 0xFF, 0xFF);
        public static readonly Color32 cyan = new Color32(0x69, 0xFE, 0xFF, 0xFF);
        public static readonly Color32 darkBlue = new Color32(0x00, 0x6C, 0x98, 0xFF);
        public static readonly Color32 darkerBlue = new Color32(0x00, 0x21, 0x5E, 0xFF);
        public static readonly Color boardColor = new Color(55f / 255f, 55f / 255f, 55f / 255f);
    }

    void Start() 
    {
        boardManager = GameObject.Find("Board Manager").GetComponent<BoardManager>(); // get the board manager script

        spriteRenderer = this.GetComponent<SpriteRenderer>(); // get the sprite renderer of the cell

        x = (int)this.transform.position.x + boardManager.boardSize / 2; // get the x position of the cell in the board matrix
        y = (int)this.transform.position.y + boardManager.boardSize / 2; // get the y position of the cell in the board matrix

        boardManager.cellsSpawned++; // increment the number of cells spawned
    }

    public void HandleLeftClick() // handle being drawn as alive
    {
        if(!alive)
        {
            alive = true;
            aliveFor = 1;
            UpdateState();
        }
    }

    public void HandleRightClick() // handle being drawn as dead
    {
        if (alive)
        {
            alive = false;
            aliveFor = 0;
            UpdateState();
        }
    }

    public void UpdateState() // update the state of the cell
    {
        WillBeAlive(); // check if the cell will be alive

        UpdateColor(); // update the color of the cell
    }

    public void UpdateNeighbors(int difference = 1) // update the neighbors of the cell
    {
        if (alive)
        {
            foreach (GameObject neighbor in neighbors)
            {
                neighbor.GetComponent<Cell>().aliveNeighbors += difference; // adds or removes 1 from the alive neighbors of each neighbor
            }
        }
    }

    public void SwitchState() // switch the state of the cell from alive to nextAlive
    {
        if (alive && nextAlive)
        {
            aliveFor++;
            if(aliveFor > 7){
                aliveFor = 7;
            }
        }
        else if (alive != nextAlive)
        {
            alive = nextAlive;

            if(alive)
            {
                aliveFor = 1;
            }
            else
            {
                aliveFor = 0;
            }
        }
        aliveNeighbors = 0;
        UpdateColor();
    }

    void WillBeAlive() // check if the cell will be alive
    {
        if (alive)
        {
            if (aliveNeighbors < 2 || aliveNeighbors > 3)
            {
                nextAlive = false;
            }
            else
            {
                nextAlive = true;
            }
        }
        else
        {
            if (aliveNeighbors == 3)
            {
                nextAlive = true;
            }
            else
            {
                nextAlive = false;
            }
        }
    }

    public void RandomState() // randomize the state of the cell
    {
        if (Random.value < 0.5f)
        {
            aliveFor = 1;
            alive = true;
        }
        else
        {
            aliveFor = 0;
            alive = false;
        }
        UpdateColor();
    }

    public void GetNeighbors() // get the neighbors of the cell
    {
        for (int i = x - 1; i <= x + 1; i++)
        {
            for (int j = y - 1; j <= y + 1; j++)
            {
                if (i == x && j == y)
                {
                    continue;
                }
                if (i < 0 || i >= boardManager.boardSize || j < 0 || j >= boardManager.boardSize)
                {
                    continue;
                }
                neighbors.Add(boardManager.cells[i][j]);
            }
        }
    }

    public void UpdateColor() // update the color of the cell
    {
        switch(aliveFor){
            case 0:
                spriteRenderer.color = Colors.boardColor;
                break;
            case 1:
                spriteRenderer.color = Colors.red;
                break;
            case 2:
                spriteRenderer.color = Colors.lightRed;
                break;
            case 3:
                spriteRenderer.color = Colors.lighterRed;
                break;
            case 4:
                spriteRenderer.color = Colors.white;
                break;
            case 5:
                spriteRenderer.color = Colors.cyan;
                break;
            case 6:
                spriteRenderer.color = Colors.darkBlue;
                break;
            case 7:
                spriteRenderer.color = Colors.darkerBlue;
                break;
            default:
                spriteRenderer.color = Color.green; // if an error happens, make it green
                break;
        }
    }
}
