using System;
using System.Collections.Generic;
using NUnit.Framework;

[TestFixture]
public class GameManagerTests
{
    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        gameManager = new GameManager();
    }

    [Test]
    public void RotateStructure_EmptyStructure_ReturnsEmpty()
    {
        var result = gameManager.RotateStructure(new List<List<Cell>>());
        Assert.IsEmpty(result);
    }

    [Test]
    public void RotateStructure_NullStructure_ReturnsEmpty()
    {
        var result = gameManager.RotateStructure(null);
        Assert.IsEmpty(result);
    }

    [Test]
    public void RotateStructure_2x2Structure_RotatesCorrectly()
    {
        var structure = new List<List<Cell>>
        {
            new List<Cell> { new Cell(0, 0), new Cell(1, 0) },
            new List<Cell> { new Cell(0, 1), new Cell(1, 1) }
        };

        structure[0][0].isAlive = true;
        structure[0][1].isAlive = false;
        structure[1][0].isAlive = false;
        structure[1][1].isAlive = true;

        var result = gameManager.RotateStructure(structure);

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual(2, result[0].Count);
        Assert.IsFalse(result[0][0].isAlive);
        Assert.IsTrue(result[0][1].isAlive);
        Assert.IsTrue(result[1][0].isAlive);
        Assert.IsFalse(result[1][1].isAlive);
    }

    [Test]
    public void PivotStructure_HorizontalFlip_FlipsCorrectly()
    {
        var structure = new List<List<Cell>>
        {
            new List<Cell> { new Cell(0, 0), new Cell(1, 0) }
        };
        structure[0][0].isAlive = true;
        structure[0][1].isAlive = false;

        var result = gameManager.PivotStructure(structure, 0);

        Assert.AreEqual(1, result.Count);
        Assert.AreEqual(2, result[0].Count);
        Assert.IsFalse(result[0][0].isAlive);
        Assert.IsTrue(result[0][1].isAlive);
    }

    [Test]
    public void PivotStructure_VerticalFlip_FlipsCorrectly()
    {
        var structure = new List<List<Cell>>
        {
            new List<Cell> { new Cell(0, 0) },
            new List<Cell> { new Cell(0, 1) }
        };
        structure[0][0].isAlive = true;
        structure[1][0].isAlive = false;

        var result = gameManager.PivotStructure(structure, 1);

        Assert.AreEqual(2, result.Count);
        Assert.IsFalse(result[0][0].isAlive);
        Assert.IsTrue(result[1][0].isAlive);
    }

    [Test]
    public void PivotStructure_InvalidPivotType_ReturnsOriginal()
    {
        var structure = new List<List<Cell>>
        {
            new List<Cell> { new Cell(0, 0) }
        };
        structure[0][0].isAlive = true;

        var result = gameManager.PivotStructure(structure, 99);

        Assert.AreEqual(1, result.Count);
        Assert.IsTrue(result[0][0].isAlive);
    }

    [Test]
    public void ResetMarkedCells_ValidGrid_ResetsAllMarkedFlags()
    {
        var grid = new List<List<Cell>>
        {
            new List<Cell> { new Cell(0, 0), new Cell(1, 0) },
            new List<Cell> { new Cell(0, 1), new Cell(1, 1) }
        };

        grid[0][0].isMarked = true;
        grid[1][1].isMarked = true;

        gameManager.ResetMarkedCells(grid);

        Assert.IsFalse(grid[0][0].isMarked);
        Assert.IsFalse(grid[0][1].isMarked);
        Assert.IsFalse(grid[1][0].isMarked);
        Assert.IsFalse(grid[1][1].isMarked);
    }

    [Test]
    public void ResetMarkedCells_NullGrid_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => gameManager.ResetMarkedCells(null));
    }

    [Test]
    public void UpdateColor_ValidCell_SetsCorrectColors()
    {
        var cell = new Cell(0, 0);

        gameManager.UpdateColor(cell, 0);
        Assert.AreEqual("white", cell.currentColor);

        gameManager.UpdateColor(cell, 1);
        Assert.AreEqual("green", cell.currentColor);

        gameManager.UpdateColor(cell, 2);
        Assert.AreEqual("cyan", cell.currentColor);

        gameManager.UpdateColor(cell, 3);
        Assert.AreEqual("blue", cell.currentColor);

        gameManager.UpdateColor(cell, 4);
        Assert.AreEqual("yellow", cell.currentColor);

        gameManager.UpdateColor(cell, 5);
        Assert.AreEqual("orange", cell.currentColor);

        gameManager.UpdateColor(cell, 6);
        Assert.AreEqual("red", cell.currentColor);

        gameManager.UpdateColor(cell, 7);
        Assert.AreEqual("magenta", cell.currentColor);
    }

    [Test]
    public void UpdateColor_InvalidColorIndex_SetsWhite()
    {
        var cell = new Cell(0, 0);

        gameManager.UpdateColor(cell, -1);
        Assert.AreEqual("white", cell.currentColor);

        gameManager.UpdateColor(cell, 99);
        Assert.AreEqual("white", cell.currentColor);
    }

    [Test]
    public void UpdateColor_NullCell_DoesNotThrow()
    {
        Assert.DoesNotThrow(() => gameManager.UpdateColor(null, 1));
    }
}

[TestFixture]
public class BoardManagerTests
{
    private BoardManager boardManager;

    [SetUp]
    public void Setup()
    {
        boardManager = new BoardManager();
    }

    [Test]
    public void InitializeBoard_ValidDimensions_CreatesCorrectGrid()
    {
        boardManager.InitializeBoard(5, 3);

        Assert.AreEqual(5, boardManager.boardWidth);
        Assert.AreEqual(3, boardManager.boardHeight);
        Assert.AreEqual(3, boardManager.grid.Count);
        Assert.AreEqual(5, boardManager.grid[0].Count);
    }

    [Test]
    public void GetCellAt_ValidCoordinates_ReturnsCell()
    {
        boardManager.InitializeBoard(3, 3);
        var cell = boardManager.GetCellAt(1, 1);

        Assert.IsNotNull(cell);
        Assert.AreEqual(1, cell.x);
        Assert.AreEqual(1, cell.y);
    }

    [Test]
    public void GetCellAt_InvalidCoordinates_ReturnsNull()
    {
        boardManager.InitializeBoard(3, 3);

        Assert.IsNull(boardManager.GetCellAt(-1, 0));
        Assert.IsNull(boardManager.GetCellAt(0, -1));
        Assert.IsNull(boardManager.GetCellAt(3, 0));
        Assert.IsNull(boardManager.GetCellAt(0, 3));
    }

    [Test]
    public void CountLiveNeighbors_CenterCell_CountsCorrectly()
    {
        boardManager.InitializeBoard(3, 3);
        
        // Set up a pattern with live neighbors
        boardManager.GetCellAt(0, 0).SetAlive(true);
        boardManager.GetCellAt(1, 0).SetAlive(true);
        boardManager.GetCellAt(2, 0).SetAlive(true);

        int count = boardManager.CountLiveNeighbors(1, 1);
        Assert.AreEqual(3, count);
    }

    [Test]
    public void UpdateBoard_ConwaysRules_AppliesCorrectly()
    {
        boardManager.InitializeBoard(3, 3);
        
        // Set up a classic "block" pattern (stable)
        boardManager.GetCellAt(0, 0).SetAlive(true);
        boardManager.GetCellAt(1, 0).SetAlive(true);
        boardManager.GetCellAt(0, 1).SetAlive(true);
        boardManager.GetCellAt(1, 1).SetAlive(true);

        boardManager.UpdateBoard();

        // Block should remain stable
        Assert.IsTrue(boardManager.GetCellAt(0, 0).isAlive);
        Assert.IsTrue(boardManager.GetCellAt(1, 0).isAlive);
        Assert.IsTrue(boardManager.GetCellAt(0, 1).isAlive);
        Assert.IsTrue(boardManager.GetCellAt(1, 1).isAlive);
    }

    [Test]
    public void ClearBoard_AllCellsLive_SetsAllDead()
    {
        boardManager.InitializeBoard(2, 2);
        
        // Set all cells alive
        foreach (var row in boardManager.grid)
        {
            foreach (var cell in row)
            {
                cell.SetAlive(true);
            }
        }

        boardManager.ClearBoard();

        // All cells should be dead
        foreach (var row in boardManager.grid)
        {
            foreach (var cell in row)
            {
                Assert.IsFalse(cell.isAlive);
            }
        }
    }
}

[TestFixture]
public class CellTests
{
    [Test]
    public void Cell_Constructor_SetsCoordinates()
    {
        var cell = new Cell(5, 7);
        Assert.AreEqual(5, cell.x);
        Assert.AreEqual(7, cell.y);
    }

    [Test]
    public void SetAlive_ChangesState()
    {
        var cell = new Cell(0, 0);
        
        cell.SetAlive(true);
        Assert.IsTrue(cell.isAlive);
        
        cell.SetAlive(false);
        Assert.IsFalse(cell.isAlive);
    }

    [Test]
    public void SetColor_ValidColor_SetsColor()
    {
        var cell = new Cell(0, 0);
        
        cell.SetColor("red");
        Assert.AreEqual("red", cell.currentColor);
    }

    [Test]
    public void SetColor_NullColor_SetsWhite()
    {
        var cell = new Cell(0, 0);
        
        cell.SetColor(null);
        Assert.AreEqual("white", cell.currentColor);
    }

    [Test]
    public void IsNeighbor_AdjacentCells_ReturnsTrue()
    {
        var cell1 = new Cell(1, 1);
        var cell2 = new Cell(1, 2); // Vertically adjacent
        var cell3 = new Cell(2, 1); // Horizontally adjacent
        var cell4 = new Cell(2, 2); // Diagonally adjacent

        Assert.IsTrue(cell1.IsNeighbor(cell2));
        Assert.IsTrue(cell1.IsNeighbor(cell3));
        Assert.IsTrue(cell1.IsNeighbor(cell4));
    }

    [Test]
    public void IsNeighbor_NonAdjacentCells_ReturnsFalse()
    {
        var cell1 = new Cell(1, 1);
        var cell2 = new Cell(3, 3); // Not adjacent

        Assert.IsFalse(cell1.IsNeighbor(cell2));
    }

    [Test]
    public void IsNeighbor_NullCell_ReturnsFalse()
    {
        var cell = new Cell(1, 1);
        Assert.IsFalse(cell.IsNeighbor(null));
    }
}