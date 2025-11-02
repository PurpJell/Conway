using NUnit.Framework;
using System;
using UnityEngine;
using UnityEngine.Rendering;

using Object = UnityEngine.Object;

public class RotateStructureTests
{
    private GameManager gameManager;
    private GameObject gameObject;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        gameManager = gameObject.AddComponent<GameManager>();
    }

    [TearDown]
    public void TearDown()
    {
        if (gameObject != null)
            Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void RotateStructure_FromBottomRight_TransitionsToBottomLeft()
    {
        // Arrange
        gameManager.pastingRotatedX = 0;
        gameManager.pastingRotatedY = 0;

        // Act
        gameManager.RotateStructure();

        // Assert
        Assert.AreEqual(1, gameManager.pastingRotatedX, "X should be 1 after rotating from bottom right");
        Assert.AreEqual(0, gameManager.pastingRotatedY, "Y should be 0 after rotating from bottom right");
    }

    [Test]
    public void RotateStructure_FromBottomLeft_TransitionsToTopLeft()
    {
        // Arrange
        gameManager.pastingRotatedX = 1;
        gameManager.pastingRotatedY = 0;

        // Act
        gameManager.RotateStructure();

        // Assert
        Assert.AreEqual(1, gameManager.pastingRotatedX, "X should be 1 after rotating from bottom left");
        Assert.AreEqual(1, gameManager.pastingRotatedY, "Y should be 1 after rotating from bottom left");
    }

    [Test]
    public void RotateStructure_FromTopLeft_TransitionsToTopRight()
    {
        // Arrange
        gameManager.pastingRotatedX = 1;
        gameManager.pastingRotatedY = 1;

        // Act
        gameManager.RotateStructure();

        // Assert
        Assert.AreEqual(0, gameManager.pastingRotatedX, "X should be 0 after rotating from top left");
        Assert.AreEqual(1, gameManager.pastingRotatedY, "Y should be 1 after rotating from top left");
    }

    [Test]
    public void RotateStructure_FromTopRight_TransitionsToBottomRight()
    {
        // Arrange
        gameManager.pastingRotatedX = 0;
        gameManager.pastingRotatedY = 1;

        // Act
        gameManager.RotateStructure();

        // Assert
        Assert.AreEqual(0, gameManager.pastingRotatedX, "X should be 0 after rotating from top right");
        Assert.AreEqual(0, gameManager.pastingRotatedY, "Y should be 0 after rotating from top right");
    }

    [Test]
    public void RotateStructure_FullCycle_ReturnsToInitialState()
    {
        // Arrange
        gameManager.pastingRotatedX = 0;
        gameManager.pastingRotatedY = 0;

        // Act - Rotate 4 times to complete full cycle
        gameManager.RotateStructure(); // 0,0 -> 1,0
        gameManager.RotateStructure(); // 1,0 -> 1,1
        gameManager.RotateStructure(); // 1,1 -> 0,1
        gameManager.RotateStructure(); // 0,1 -> 0,0

        // Assert
        Assert.AreEqual(0, gameManager.pastingRotatedX, "X should return to 0 after full cycle");
        Assert.AreEqual(0, gameManager.pastingRotatedY, "Y should return to 0 after full cycle");
    }

    [Test]
    public void RotateStructure_InvalidState_DoesNothing()
    {
        // Arrange - Set to an invalid state not covered by any if statement
        gameManager.pastingRotatedX = 2;
        gameManager.pastingRotatedY = 2;

        // Act
        gameManager.RotateStructure();

        // Assert - Values should remain unchanged
        Assert.AreEqual(2, gameManager.pastingRotatedX, "X should remain unchanged for invalid state");
        Assert.AreEqual(2, gameManager.pastingRotatedY, "Y should remain unchanged for invalid state");
    }

    [Test]
    public void RotateStructure_NegativeValues_DoesNothing()
    {
        // Arrange
        gameManager.pastingRotatedX = -1;
        gameManager.pastingRotatedY = -1;

        // Act
        gameManager.RotateStructure();

        // Assert
        Assert.AreEqual(-1, gameManager.pastingRotatedX, "X should remain unchanged for negative values");
        Assert.AreEqual(-1, gameManager.pastingRotatedY, "Y should remain unchanged for negative values");
    }

    [Test]
    public void RotateStructure_MultipleRotations_FollowsCorrectSequence()
    {
        // Arrange
        gameManager.pastingRotatedX = 0;
        gameManager.pastingRotatedY = 0;

        // Act & Assert - Test each rotation step
        gameManager.RotateStructure();
        Assert.AreEqual((1, 0), (gameManager.pastingRotatedX, gameManager.pastingRotatedY), "First rotation");

        gameManager.RotateStructure();
        Assert.AreEqual((1, 1), (gameManager.pastingRotatedX, gameManager.pastingRotatedY), "Second rotation");

        gameManager.RotateStructure();
        Assert.AreEqual((0, 1), (gameManager.pastingRotatedX, gameManager.pastingRotatedY), "Third rotation");

        gameManager.RotateStructure();
        Assert.AreEqual((0, 0), (gameManager.pastingRotatedX, gameManager.pastingRotatedY), "Fourth rotation");
    }
}
public class PivotStructureTests
{
    private GameObject gameObject;
    private GameManager gameManager;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        gameManager = gameObject.AddComponent<GameManager>();
    }

    [TearDown]
    public void TearDown()
    {
        if (gameObject != null)
            Object.DestroyImmediate(gameObject);
    }

    [Test]
    public void PivotStructure_FromBottomRight_TransitionsToBottomLeft()
    {
        // Arrange
        gameManager.pastingPivotedX = 0;
        gameManager.pastingPivotedY = 0;

        // Act
        gameManager.PivotStructure();

        // Assert
        Assert.AreEqual(1, gameManager.pastingPivotedX, "X should be 1 after pivoting from bottom right");
        Assert.AreEqual(0, gameManager.pastingPivotedY, "Y should be 0 after pivoting from bottom right");
    }

    [Test]
    public void PivotStructure_FromBottomLeft_TransitionsToTopLeft()
    {
        // Arrange
        gameManager.pastingPivotedX = 1;
        gameManager.pastingPivotedY = 0;

        // Act
        gameManager.PivotStructure();

        // Assert
        Assert.AreEqual(1, gameManager.pastingPivotedX, "X should be 1 after pivoting from bottom left");
        Assert.AreEqual(1, gameManager.pastingPivotedY, "Y should be 1 after pivoting from bottom left");
    }

    [Test]
    public void PivotStructure_FromTopLeft_TransitionsToTopRight()
    {
        // Arrange
        gameManager.pastingPivotedX = 1;
        gameManager.pastingPivotedY = 1;

        // Act
        gameManager.PivotStructure();

        // Assert
        Assert.AreEqual(0, gameManager.pastingPivotedX, "X should be 0 after pivoting from top left");
        Assert.AreEqual(1, gameManager.pastingPivotedY, "Y should be 1 after pivoting from top left");
    }

    [Test]
    public void PivotStructure_FromTopRight_TransitionsToBottomRight()
    {
        // Arrange
        gameManager.pastingPivotedX = 0;
        gameManager.pastingPivotedY = 1;

        // Act
        gameManager.PivotStructure();

        // Assert
        Assert.AreEqual(0, gameManager.pastingPivotedX, "X should be 0 after pivoting from top right");
        Assert.AreEqual(0, gameManager.pastingPivotedY, "Y should be 0 after pivoting from top right");
    }

    [Test]
    public void PivotStructure_FullCycle_ReturnsToInitialState()
    {
        // Arrange
        gameManager.pastingPivotedX = 0;
        gameManager.pastingPivotedY = 0;

        // Act - Pivot 4 times to complete full cycle
        gameManager.PivotStructure(); // 0,0 -> 1,0
        gameManager.PivotStructure(); // 1,0 -> 1,1
        gameManager.PivotStructure(); // 1,1 -> 0,1
        gameManager.PivotStructure(); // 0,1 -> 0,0

        // Assert
        Assert.AreEqual(0, gameManager.pastingPivotedX, "X should return to 0 after full cycle");
        Assert.AreEqual(0, gameManager.pastingPivotedY, "Y should return to 0 after full cycle");
    }

    [Test]
    public void PivotStructure_InvalidState_DoesNothing()
    {
        // Arrange - Set to an invalid state not covered by any if statement
        gameManager.pastingPivotedX = 2;
        gameManager.pastingPivotedY = 2;

        // Act
        gameManager.PivotStructure();

        // Assert - Values should remain unchanged
        Assert.AreEqual(2, gameManager.pastingPivotedX, "X should remain unchanged for invalid state");
        Assert.AreEqual(2, gameManager.pastingPivotedY, "Y should remain unchanged for invalid state");
    }

    [Test]
    public void PivotStructure_NegativeValues_DoesNothing()
    {
        // Arrange
        gameManager.pastingPivotedX = -1;
        gameManager.pastingPivotedY = -1;

        // Act
        gameManager.PivotStructure();

        // Assert
        Assert.AreEqual(-1, gameManager.pastingPivotedX, "X should remain unchanged for negative values");
        Assert.AreEqual(-1, gameManager.pastingPivotedY, "Y should remain unchanged for negative values");
    }

    [Test]
    public void PivotStructure_MixedInvalidValues_DoesNothing()
    {
        // Arrange - X is valid but Y is not
        gameManager.pastingPivotedX = 0;
        gameManager.pastingPivotedY = 5;

        // Act
        gameManager.PivotStructure();

        // Assert
        Assert.AreEqual(0, gameManager.pastingPivotedX, "X should remain unchanged when Y is invalid");
        Assert.AreEqual(5, gameManager.pastingPivotedY, "Y should remain unchanged when invalid");
    }

    [Test]
    public void PivotStructure_MultipleConsecutivePivots_FollowsCorrectSequence()
    {
        // Arrange
        gameManager.pastingPivotedX = 0;
        gameManager.pastingPivotedY = 0;

        // Act & Assert - Test each pivot step
        gameManager.PivotStructure();
        Assert.AreEqual((1, 0), (gameManager.pastingPivotedX, gameManager.pastingPivotedY), "First pivot");

        gameManager.PivotStructure();
        Assert.AreEqual((1, 1), (gameManager.pastingPivotedX, gameManager.pastingPivotedY), "Second pivot");

        gameManager.PivotStructure();
        Assert.AreEqual((0, 1), (gameManager.pastingPivotedX, gameManager.pastingPivotedY), "Third pivot");

        gameManager.PivotStructure();
        Assert.AreEqual((0, 0), (gameManager.pastingPivotedX, gameManager.pastingPivotedY), "Fourth pivot");
    }

    [Test]
    public void PivotStructure_StartFromDifferentPositions_AllTransitionCorrectly()
    {
        // Test starting from position (1,0)
        gameManager.pastingPivotedX = 1;
        gameManager.pastingPivotedY = 0;
        gameManager.PivotStructure();
        Assert.AreEqual((1, 1), (gameManager.pastingPivotedX, gameManager.pastingPivotedY), "From (1,0)");

        // Reset and test starting from position (1,1)
        gameManager.pastingPivotedX = 1;
        gameManager.pastingPivotedY = 1;
        gameManager.PivotStructure();
        Assert.AreEqual((0, 1), (gameManager.pastingPivotedX, gameManager.pastingPivotedY), "From (1,1)");

        // Reset and test starting from position (0,1)
        gameManager.pastingPivotedX = 0;
        gameManager.pastingPivotedY = 1;
        gameManager.PivotStructure();
        Assert.AreEqual((0, 0), (gameManager.pastingPivotedX, gameManager.pastingPivotedY), "From (0,1)");
    }

    [Test]
    public void PivotStructure_PartialInvalidState_DoesNothing()
    {
        // Arrange - X matches a valid pattern but Y doesn't form a valid state
        gameManager.pastingPivotedX = 1;
        gameManager.pastingPivotedY = 2;

        // Act
        gameManager.PivotStructure();

        // Assert
        Assert.AreEqual(1, gameManager.pastingPivotedX, "X should remain unchanged");
        Assert.AreEqual(2, gameManager.pastingPivotedY, "Y should remain unchanged");
    }
}
public class ResetMarkedCellsTests
{
    private GameObject cell1;
    private GameObject cell2;
    private Cell cellScript1;
    private Cell cellScript2;

    private GameManager gameManager;
    private GameObject gameObject;

    [SetUp]
    public void Setup()
    {
        gameObject = new GameObject();
        gameManager = gameObject.AddComponent<GameManager>();
        cell1 = new GameObject("Cell1");
        cell2 = new GameObject("Cell2");
        cellScript1 = cell1.AddComponent<Cell>();
        cellScript2 = cell2.AddComponent<Cell>();
    }

    [TearDown]
    public void TearDown()
    {
        if (gameObject != null)
            Object.DestroyImmediate(gameObject);
        if (cell1 != null)
            Object.DestroyImmediate(cell1);
        if (cell2 != null)
            Object.DestroyImmediate(cell2);
    }

    [Test]
    public void ResetMarkedCells_BothCellsMarked_ResetsBothToNull()
    {
        // Arrange
        gameManager.markedCell1 = cell1;
        gameManager.markedCell2 = cell2;
        gameManager.cell1Script = cellScript1;
        gameManager.cell2Script = cellScript2;

        // Act
        gameManager.ResetMarkedCells();

        // Assert
        Assert.IsNull(gameManager.markedCell1, "markedCell1 should be null after reset");
        Assert.IsNull(gameManager.markedCell2, "markedCell2 should be null after reset");
        Assert.IsNull(gameManager.cell1Script, "cell1Script should be null after reset");
        Assert.IsNull(gameManager.cell2Script, "cell2Script should be null after reset");
    }

    [Test]
    public void ResetMarkedCells_OnlyCell1Marked_ResetsOnlyCell1()
    {
        // Arrange
        gameManager.markedCell1 = cell1;
        gameManager.cell1Script = cellScript1;
        gameManager.markedCell2 = null;
        gameManager.cell2Script = null;

        // Act
        gameManager.ResetMarkedCells();

        // Assert
        Assert.IsNull(gameManager.markedCell1, "markedCell1 should be null after reset");
        Assert.IsNull(gameManager.cell1Script, "cell1Script should be null after reset");
        Assert.IsNull(gameManager.markedCell2, "markedCell2 should remain null");
        Assert.IsNull(gameManager.cell2Script, "cell2Script should remain null");
    }

    [Test]
    public void ResetMarkedCells_OnlyCell2Marked_ResetsOnlyCell2()
    {
        // Arrange
        gameManager.markedCell1 = null;
        gameManager.cell1Script = null;
        gameManager.markedCell2 = cell2;
        gameManager.cell2Script = cellScript2;

        // Act
        gameManager.ResetMarkedCells();

        // Assert
        Assert.IsNull(gameManager.markedCell1, "markedCell1 should remain null");
        Assert.IsNull(gameManager.cell1Script, "cell1Script should remain null");
        Assert.IsNull(gameManager.markedCell2, "markedCell2 should be null after reset");
        Assert.IsNull(gameManager.cell2Script, "cell2Script should be null after reset");
    }

    [Test]
    public void ResetMarkedCells_BothCellsNull_RemainsNull()
    {
        // Arrange
        gameManager.markedCell1 = null;
        gameManager.markedCell2 = null;
        gameManager.cell1Script = null;
        gameManager.cell2Script = null;

        // Act
        gameManager.ResetMarkedCells();

        // Assert
        Assert.IsNull(gameManager.markedCell1, "markedCell1 should remain null");
        Assert.IsNull(gameManager.markedCell2, "markedCell2 should remain null");
        Assert.IsNull(gameManager.cell1Script, "cell1Script should remain null");
        Assert.IsNull(gameManager.cell2Script, "cell2Script should remain null");
    }

    [Test]
    public void ResetMarkedCells_Cell1MarkedButScriptNull_ResetsCell1Only()
    {
        // Arrange - Cell is marked but script reference is null
        gameManager.markedCell1 = cell1;
        gameManager.cell1Script = null;
        gameManager.markedCell2 = null;
        gameManager.cell2Script = null;

        // Act
        gameManager.ResetMarkedCells();

        // Assert
        Assert.IsNull(gameManager.markedCell1, "markedCell1 should be null after reset");
        Assert.IsNull(gameManager.cell1Script, "cell1Script should remain null");
    }

    [Test]
    public void ResetMarkedCells_Cell2MarkedButScriptNull_ResetsCell2Only()
    {
        // Arrange - Cell is marked but script reference is null
        gameManager.markedCell1 = null;
        gameManager.cell1Script = null;
        gameManager.markedCell2 = cell2;
        gameManager.cell2Script = null;

        // Act
        gameManager.ResetMarkedCells();

        // Assert
        Assert.IsNull(gameManager.markedCell2, "markedCell2 should be null after reset");
        Assert.IsNull(gameManager.cell2Script, "cell2Script should remain null");
    }

    [Test]
    public void ResetMarkedCells_CalledMultipleTimes_RemainsNull()
    {
        // Arrange
        gameManager.markedCell1 = cell1;
        gameManager.markedCell2 = cell2;
        gameManager.cell1Script = cellScript1;
        gameManager.cell2Script = cellScript2;

        // Act - Call reset multiple times
        gameManager.ResetMarkedCells();
        gameManager.ResetMarkedCells();
        gameManager.ResetMarkedCells();

        // Assert
        Assert.IsNull(gameManager.markedCell1, "markedCell1 should remain null after multiple resets");
        Assert.IsNull(gameManager.markedCell2, "markedCell2 should remain null after multiple resets");
        Assert.IsNull(gameManager.cell1Script, "cell1Script should remain null after multiple resets");
        Assert.IsNull(gameManager.cell2Script, "cell2Script should remain null after multiple resets");
    }

    [Test]
    public void ResetMarkedCells_ScriptsSetButCellsNull_ScriptsUnaffected()
    {
        // Arrange - Edge case where scripts are set but cells are null
        gameManager.markedCell1 = null;
        gameManager.markedCell2 = null;
        gameManager.cell1Script = cellScript1;
        gameManager.cell2Script = cellScript2;

        // Act
        gameManager.ResetMarkedCells();

        // Assert - Scripts should not be reset since cells are null
        Assert.IsNull(gameManager.markedCell1, "markedCell1 should remain null");
        Assert.IsNull(gameManager.markedCell2, "markedCell2 should remain null");
        Assert.IsNotNull(gameManager.cell1Script, "cell1Script should not be reset when cell1 is null");
        Assert.IsNotNull(gameManager.cell2Script, "cell2Script should not be reset when cell2 is null");
    }

    [Test]
    public void ResetMarkedCells_MixedState_ResetsOnlyNonNullCells()
    {
        // Arrange - Cell1 marked, Cell2 null initially
        gameManager.markedCell1 = cell1;
        gameManager.cell1Script = cellScript1;
        gameManager.markedCell2 = null;
        gameManager.cell2Script = null;

        // Act - First reset
        gameManager.ResetMarkedCells();

        // Assert after first reset
        Assert.IsNull(gameManager.markedCell1, "markedCell1 should be null after first reset");
        Assert.IsNull(gameManager.cell1Script, "cell1Script should be null after first reset");

        // Arrange - Now mark Cell2
        gameManager.markedCell2 = cell2;
        gameManager.cell2Script = cellScript2;

        // Act - Second reset
        gameManager.ResetMarkedCells();

        // Assert after second reset
        Assert.IsNull(gameManager.markedCell2, "markedCell2 should be null after second reset");
        Assert.IsNull(gameManager.cell2Script, "cell2Script should be null after second reset");
    }

    [Test]
    public void ResetMarkedCells_BothMarkedWithScripts_AllReferencesCleared()
    {
        // Arrange
        gameManager.markedCell1 = cell1;
        gameManager.markedCell2 = cell2;
        gameManager.cell1Script = cellScript1;
        gameManager.cell2Script = cellScript2;

        // Store references to verify they existed
        var cell1Ref = gameManager.markedCell1;
        var cell2Ref = gameManager.markedCell2;
        var script1Ref = gameManager.cell1Script;
        var script2Ref = gameManager.cell2Script;

        Assert.IsNotNull(cell1Ref, "Setup: cell1 should be assigned");
        Assert.IsNotNull(cell2Ref, "Setup: cell2 should be assigned");
        Assert.IsNotNull(script1Ref, "Setup: script1 should be assigned");
        Assert.IsNotNull(script2Ref, "Setup: script2 should be assigned");

        // Act
        gameManager.ResetMarkedCells();

        // Assert
        Assert.IsNull(gameManager.markedCell1, "markedCell1 reference should be cleared");
        Assert.IsNull(gameManager.markedCell2, "markedCell2 reference should be cleared");
        Assert.IsNull(gameManager.cell1Script, "cell1Script reference should be cleared");
        Assert.IsNull(gameManager.cell2Script, "cell2Script reference should be cleared");
    }
}

public class UpdateColorTests
{
    public static class Colors // colors for the cell based on how long it has been alive
    {
        public static readonly Color red = new Color(255.0f / 255.0f, 0.0f, 54.0f / 255.0f, 255.0f / 255.0f);
        public static readonly Color lightRed = new Color(255.0f / 255.0f, 132.0f / 255.0f, 131.0f / 255.0f, 255.0f / 255.0f);
        public static readonly Color lighterRed = new Color(255.0f / 255.0f, 179.0f / 255.0f, 167.0f / 255.0f, 255.0f / 255.0f);
        public static readonly Color white = new Color(255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        public static readonly Color cyan = new Color(105.0f / 255.0f, 254.0f / 255.0f, 255.0f / 255.0f, 255.0f / 255.0f);
        public static readonly Color darkBlue = new Color(0.0f / 255.0f, 108.0f / 255.0f, 152.0f / 255.0f, 255.0f / 255.0f);
        public static readonly Color darkerBlue = new Color(0.0f / 255.0f, 33.0f / 255.0f, 94.0f / 255.0f, 255.0f / 255.0f);
        public static readonly Color boardColor = new Color(55f / 255f, 55f / 255f, 55f / 255f, 255.0f / 255.0f);
    }

    private GameObject cell;
    private Cell cellScript;
    private SpriteRenderer spriteRenderer;

    [SetUp]
    public void Setup()
    {
        cell = new GameObject("Cell");
        cellScript = cell.AddComponent<Cell>();
        spriteRenderer = cell.AddComponent<SpriteRenderer>();
        cellScript.spriteRenderer = spriteRenderer;
    }

    [TearDown]
    public void TearDown()
    {
        if (cell != null)
            Object.DestroyImmediate(cell);
    }

    [Test]
    public void UpdateColor_AliveFor0_SetsBoardColor()
    {
        // Arrange
        cellScript.aliveFor = 0;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.boardColor, cellScript.spriteRenderer.color,
            "Color should be boardColor when aliveFor is 0");
    }

    [Test]
    public void UpdateColor_AliveFor1_SetsRed()
    {
        // Arrange
        cellScript.aliveFor = 1;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.red, cellScript.spriteRenderer.color,
            "Color should be red when aliveFor is 1");
    }

    [Test]
    public void UpdateColor_AliveFor2_SetsLightRed()
    {
        // Arrange
        cellScript.aliveFor = 2;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.lightRed, cellScript.spriteRenderer.color,
            "Color should be lightRed when aliveFor is 2");
    }

    [Test]
    public void UpdateColor_AliveFor3_SetsLighterRed()
    {
        // Arrange
        cellScript.aliveFor = 3;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.lighterRed, cellScript.spriteRenderer.color,
            "Color should be lighterRed when aliveFor is 3");
    }

    [Test]
    public void UpdateColor_AliveFor4_SetsWhite()
    {
        // Arrange
        cellScript.aliveFor = 4;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.white, cellScript.spriteRenderer.color,
            "Color should be white when aliveFor is 4");
    }

    [Test]
    public void UpdateColor_AliveFor5_SetsCyan()
    {
        // Arrange
        cellScript.aliveFor = 5;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.cyan, cellScript.spriteRenderer.color,
            "Color should be cyan when aliveFor is 5");
    }

    [Test]
    public void UpdateColor_AliveFor6_SetsDarkBlue()
    {
        // Arrange
        cellScript.aliveFor = 6;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.darkBlue, cellScript.spriteRenderer.color,
            "Color should be darkBlue when aliveFor is 6");
    }

    [Test]
    public void UpdateColor_AliveFor7_SetsDarkerBlue()
    {
        // Arrange
        cellScript.aliveFor = 7;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.darkerBlue, cellScript.spriteRenderer.color,
            "Color should be darkerBlue when aliveFor is 7");
    }

    [Test]
    public void UpdateColor_AliveFor8_SetsGreen()
    {
        // Arrange
        cellScript.aliveFor = 8;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, cellScript.spriteRenderer.color,
            "Color should be green (error color) when aliveFor is 8");
    }

    [Test]
    public void UpdateColor_NegativeValue_SetsGreen()
    {
        // Arrange
        cellScript.aliveFor = -1;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, cellScript.spriteRenderer.color,
            "Color should be green (error color) when aliveFor is negative");
    }

    [Test]
    public void UpdateColor_LargePositiveValue_SetsGreen()
    {
        // Arrange
        cellScript.aliveFor = 100;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, cellScript.spriteRenderer.color,
            "Color should be green (error color) when aliveFor is large positive");
    }

    [Test]
    public void UpdateColor_LargeNegativeValue_SetsGreen()
    {
        // Arrange
        cellScript.aliveFor = -999;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, cellScript.spriteRenderer.color,
            "Color should be green (error color) when aliveFor is large negative");
    }

    [Test]
    public void UpdateColor_SequentialCalls_UpdatesColorCorrectly()
    {
        // Test transitioning through multiple states
        cellScript.aliveFor = 0;
        cellScript.UpdateColor();
        Assert.AreEqual(Colors.boardColor, cellScript.spriteRenderer.color, "First state");

        cellScript.aliveFor = 1;
        cellScript.UpdateColor();
        Assert.AreEqual(Colors.red, cellScript.spriteRenderer.color, "Second state");

        cellScript.aliveFor = 5;
        cellScript.UpdateColor();
        Assert.AreEqual(Colors.cyan, cellScript.spriteRenderer.color, "Third state");

        cellScript.aliveFor = 7;
        cellScript.UpdateColor();
        Assert.AreEqual(Colors.darkerBlue, cellScript.spriteRenderer.color, "Fourth state");
    }

    [Test]
    public void UpdateColor_AllValidCases_NoExceptions()
    {
        // Test all valid cases don't throw exceptions
        for (int i = 0; i <= 7; i++)
        {
            cellScript.aliveFor = i;
            Assert.DoesNotThrow(() => cellScript.UpdateColor(),
                $"UpdateColor should not throw exception for aliveFor = {i}");
        }
    }

    [Test]
    public void UpdateColor_BoundaryCase_JustBelowValid_SetsGreen()
    {
        // Arrange
        cellScript.aliveFor = -1;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, cellScript.spriteRenderer.color,
            "Color should be green for value just below valid range");
    }

    [Test]
    public void UpdateColor_BoundaryCase_JustAboveValid_SetsGreen()
    {
        // Arrange
        cellScript.aliveFor = 8;

        // Act
        cellScript.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, cellScript.spriteRenderer.color,
            "Color should be green for value just above valid range");
    }

    [Test]
    public void UpdateColor_ColorProgression_FollowsExpectedSequence()
    {
        // Test the complete color progression from 0 to 7
        Color[] expectedColors = new Color[]
        {
            Colors.boardColor,   // 0
            Colors.red,          // 1
            Colors.lightRed,     // 2
            Colors.lighterRed,   // 3
            Colors.white,        // 4
            Colors.cyan,         // 5
            Colors.darkBlue,     // 6
            Colors.darkerBlue    // 7
        };

        for (int i = 0; i < expectedColors.Length; i++)
        {
            cellScript.aliveFor = i;
            cellScript.UpdateColor();
            Assert.AreEqual(expectedColors[i], cellScript.spriteRenderer.color,
                $"Color mismatch at aliveFor = {i}");
        }
    }

    [Test]
    public void UpdateColor_RepeatedCalls_SameValue_MaintainsColor()
    {
        // Arrange
        cellScript.aliveFor = 3;

        // Act - Call multiple times with same value
        cellScript.UpdateColor();
        Color firstColor = cellScript.spriteRenderer.color;
        cellScript.UpdateColor();
        Color secondColor = cellScript.spriteRenderer.color;
        cellScript.UpdateColor();
        Color thirdColor = cellScript.spriteRenderer.color;

        // Assert
        Assert.AreEqual(Colors.lighterRed, firstColor, "First call should set lighterRed");
        Assert.AreEqual(firstColor, secondColor, "Second call should maintain same color");
        Assert.AreEqual(secondColor, thirdColor, "Third call should maintain same color");
    }

    [Test]
    public void UpdateColor_DefaultCase_MultipleInvalidValues_AlwaysSetsGreen()
    {
        // Test various invalid values all result in green
        int[] invalidValues = { -5, -1, 8, 10, 50, 999, int.MinValue, int.MaxValue };

        foreach (int value in invalidValues)
        {
            cellScript.aliveFor = value;
            cellScript.UpdateColor();
            Assert.AreEqual(Color.green, cellScript.spriteRenderer.color,
                $"Color should be green for invalid aliveFor value: {value}");
        }
    }
}