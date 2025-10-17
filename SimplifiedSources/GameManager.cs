using System;
using System.Collections.Generic;

// Simplified GameManager for coverage analysis - core logic only
public class GameManager
{
    public enum GameState { Stopped, Playing, Paused }
    public GameState currentState = GameState.Stopped;
    
    // Core game logic methods from original GameManager
    public List<List<Cell>> RotateStructure(List<List<Cell>> structure)
    {
        if (structure == null || structure.Count == 0)
            return new List<List<Cell>>();

        int rows = structure.Count;
        int cols = structure[0].Count;

        List<List<Cell>> rotatedStructure = new List<List<Cell>>();

        for (int i = 0; i < cols; i++)
        {
            List<Cell> row = new List<Cell>();
            for (int j = rows - 1; j >= 0; j--)
            {
                row.Add(structure[j][i]);
            }
            rotatedStructure.Add(row);
        }

        return rotatedStructure;
    }

    public List<List<Cell>> PivotStructure(List<List<Cell>> structure, int pivotType)
    {
        if (structure == null || structure.Count == 0)
            return new List<List<Cell>>();

        List<List<Cell>> pivotedStructure = new List<List<Cell>>();

        switch (pivotType)
        {
            case 0: // Horizontal flip
                for (int i = 0; i < structure.Count; i++)
                {
                    List<Cell> row = new List<Cell>(structure[i]);
                    row.Reverse();
                    pivotedStructure.Add(row);
                }
                break;
            case 1: // Vertical flip
                for (int i = structure.Count - 1; i >= 0; i--)
                {
                    pivotedStructure.Add(new List<Cell>(structure[i]));
                }
                break;
            default:
                return new List<List<Cell>>(structure);
        }

        return pivotedStructure;
    }

    public void ResetMarkedCells(List<List<Cell>> grid)
    {
        if (grid == null) return;

        foreach (var row in grid)
        {
            if (row != null)
            {
                foreach (var cell in row)
                {
                    if (cell != null)
                    {
                        cell.isMarked = false;
                    }
                }
            }
        }
    }

    public void UpdateColor(Cell cell, int colorIndex)
    {
        if (cell == null) return;

        switch (colorIndex)
        {
            case 0:
                cell.currentColor = "white";
                break;
            case 1:
                cell.currentColor = "green";
                break;
            case 2:
                cell.currentColor = "cyan";
                break;
            case 3:
                cell.currentColor = "blue";
                break;
            case 4:
                cell.currentColor = "yellow";
                break;
            case 5:
                cell.currentColor = "orange";
                break;
            case 6:
                cell.currentColor = "red";
                break;
            case 7:
                cell.currentColor = "magenta";
                break;
            default:
                cell.currentColor = "white";
                break;
        }
    }
}