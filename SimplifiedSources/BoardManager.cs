using System;
using System.Collections.Generic;

// Simplified BoardManager for coverage analysis - core logic only  
public class BoardManager
{
    public List<List<Cell>> grid;
    public int boardWidth = 10;
    public int boardHeight = 10;

    public void InitializeBoard(int width, int height)
    {
        boardWidth = width;
        boardHeight = height;
        grid = new List<List<Cell>>();

        for (int y = 0; y < height; y++)
        {
            List<Cell> row = new List<Cell>();
            for (int x = 0; x < width; x++)
            {
                row.Add(new Cell(x, y));
            }
            grid.Add(row);
        }
    }

    public Cell GetCellAt(int x, int y)
    {
        if (x < 0 || x >= boardWidth || y < 0 || y >= boardHeight)
            return null;

        return grid[y][x];
    }

    public int CountLiveNeighbors(int x, int y)
    {
        int count = 0;
        
        for (int dx = -1; dx <= 1; dx++)
        {
            for (int dy = -1; dy <= 1; dy++)
            {
                if (dx == 0 && dy == 0) continue;
                
                int nx = x + dx;
                int ny = y + dy;
                
                Cell neighbor = GetCellAt(nx, ny);
                if (neighbor != null && neighbor.isAlive)
                {
                    count++;
                }
            }
        }
        
        return count;
    }

    public void UpdateBoard()
    {
        if (grid == null) return;

        List<List<bool>> nextState = new List<List<bool>>();
        
        for (int y = 0; y < boardHeight; y++)
        {
            List<bool> row = new List<bool>();
            for (int x = 0; x < boardWidth; x++)
            {
                int liveNeighbors = CountLiveNeighbors(x, y);
                Cell currentCell = GetCellAt(x, y);
                
                bool willLive = false;
                if (currentCell != null)
                {
                    if (currentCell.isAlive)
                    {
                        willLive = (liveNeighbors == 2 || liveNeighbors == 3);
                    }
                    else
                    {
                        willLive = (liveNeighbors == 3);
                    }
                }
                
                row.Add(willLive);
            }
            nextState.Add(row);
        }

        // Apply the next state
        for (int y = 0; y < boardHeight; y++)
        {
            for (int x = 0; x < boardWidth; x++)
            {
                Cell cell = GetCellAt(x, y);
                if (cell != null)
                {
                    cell.SetAlive(nextState[y][x]);
                }
            }
        }
    }

    public void ClearBoard()
    {
        if (grid == null) return;

        foreach (var row in grid)
        {
            foreach (var cell in row)
            {
                cell?.SetAlive(false);
            }
        }
    }
}