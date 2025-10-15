using NUnit.Framework;
using UnityEngine;

public class RotateStructureTests
{
    private TestClass testObject;

    // Mock class containing the method to test
    private class TestClass
    {
        public int pastingRotatedX;
        public int pastingRotatedY;

        public void RotateStructure()
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
    }

    [SetUp]
    public void Setup()
    {
        testObject = new TestClass();
    }

    [Test]
    public void RotateStructure_FromBottomRight_TransitionsToBottomLeft()
    {
        // Arrange
        testObject.pastingRotatedX = 0;
        testObject.pastingRotatedY = 0;

        // Act
        testObject.RotateStructure();

        // Assert
        Assert.AreEqual(1, testObject.pastingRotatedX, "X should be 1 after rotating from bottom right");
        Assert.AreEqual(0, testObject.pastingRotatedY, "Y should be 0 after rotating from bottom right");
    }

    [Test]
    public void RotateStructure_FromBottomLeft_TransitionsToTopLeft()
    {
        // Arrange
        testObject.pastingRotatedX = 1;
        testObject.pastingRotatedY = 0;

        // Act
        testObject.RotateStructure();

        // Assert
        Assert.AreEqual(1, testObject.pastingRotatedX, "X should be 1 after rotating from bottom left");
        Assert.AreEqual(1, testObject.pastingRotatedY, "Y should be 1 after rotating from bottom left");
    }

    [Test]
    public void RotateStructure_FromTopLeft_TransitionsToTopRight()
    {
        // Arrange
        testObject.pastingRotatedX = 1;
        testObject.pastingRotatedY = 1;

        // Act
        testObject.RotateStructure();

        // Assert
        Assert.AreEqual(0, testObject.pastingRotatedX, "X should be 0 after rotating from top left");
        Assert.AreEqual(1, testObject.pastingRotatedY, "Y should be 1 after rotating from top left");
    }

    [Test]
    public void RotateStructure_FromTopRight_TransitionsToBottomRight()
    {
        // Arrange
        testObject.pastingRotatedX = 0;
        testObject.pastingRotatedY = 1;

        // Act
        testObject.RotateStructure();

        // Assert
        Assert.AreEqual(0, testObject.pastingRotatedX, "X should be 0 after rotating from top right");
        Assert.AreEqual(0, testObject.pastingRotatedY, "Y should be 0 after rotating from top right");
    }

    [Test]
    public void RotateStructure_FullCycle_ReturnsToInitialState()
    {
        // Arrange
        testObject.pastingRotatedX = 0;
        testObject.pastingRotatedY = 0;

        // Act - Rotate 4 times to complete full cycle
        testObject.RotateStructure(); // 0,0 -> 1,0
        testObject.RotateStructure(); // 1,0 -> 1,1
        testObject.RotateStructure(); // 1,1 -> 0,1
        testObject.RotateStructure(); // 0,1 -> 0,0

        // Assert
        Assert.AreEqual(0, testObject.pastingRotatedX, "X should return to 0 after full cycle");
        Assert.AreEqual(0, testObject.pastingRotatedY, "Y should return to 0 after full cycle");
    }

    [Test]
    public void RotateStructure_InvalidState_DoesNothing()
    {
        // Arrange - Set to an invalid state not covered by any if statement
        testObject.pastingRotatedX = 2;
        testObject.pastingRotatedY = 2;

        // Act
        testObject.RotateStructure();

        // Assert - Values should remain unchanged
        Assert.AreEqual(2, testObject.pastingRotatedX, "X should remain unchanged for invalid state");
        Assert.AreEqual(2, testObject.pastingRotatedY, "Y should remain unchanged for invalid state");
    }

    [Test]
    public void RotateStructure_NegativeValues_DoesNothing()
    {
        // Arrange
        testObject.pastingRotatedX = -1;
        testObject.pastingRotatedY = -1;

        // Act
        testObject.RotateStructure();

        // Assert
        Assert.AreEqual(-1, testObject.pastingRotatedX, "X should remain unchanged for negative values");
        Assert.AreEqual(-1, testObject.pastingRotatedY, "Y should remain unchanged for negative values");
    }

    [Test]
    public void RotateStructure_MultipleRotations_FollowsCorrectSequence()
    {
        // Arrange
        testObject.pastingRotatedX = 0;
        testObject.pastingRotatedY = 0;

        // Act & Assert - Test each rotation step
        testObject.RotateStructure();
        Assert.AreEqual((1, 0), (testObject.pastingRotatedX, testObject.pastingRotatedY), "First rotation");

        testObject.RotateStructure();
        Assert.AreEqual((1, 1), (testObject.pastingRotatedX, testObject.pastingRotatedY), "Second rotation");

        testObject.RotateStructure();
        Assert.AreEqual((0, 1), (testObject.pastingRotatedX, testObject.pastingRotatedY), "Third rotation");

        testObject.RotateStructure();
        Assert.AreEqual((0, 0), (testObject.pastingRotatedX, testObject.pastingRotatedY), "Fourth rotation");
    }
}
public class PivotStructureTests
{
    private TestClass testObject;

    // Mock class containing the method to test
    private class TestClass
    {
        public int pastingPivotedX;
        public int pastingPivotedY;

        public void PivotStructure()
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
    }

    [SetUp]
    public void Setup()
    {
        testObject = new TestClass();
    }

    [Test]
    public void PivotStructure_FromBottomRight_TransitionsToBottomLeft()
    {
        // Arrange
        testObject.pastingPivotedX = 0;
        testObject.pastingPivotedY = 0;

        // Act
        testObject.PivotStructure();

        // Assert
        Assert.AreEqual(1, testObject.pastingPivotedX, "X should be 1 after pivoting from bottom right");
        Assert.AreEqual(0, testObject.pastingPivotedY, "Y should be 0 after pivoting from bottom right");
    }

    [Test]
    public void PivotStructure_FromBottomLeft_TransitionsToTopLeft()
    {
        // Arrange
        testObject.pastingPivotedX = 1;
        testObject.pastingPivotedY = 0;

        // Act
        testObject.PivotStructure();

        // Assert
        Assert.AreEqual(1, testObject.pastingPivotedX, "X should be 1 after pivoting from bottom left");
        Assert.AreEqual(1, testObject.pastingPivotedY, "Y should be 1 after pivoting from bottom left");
    }

    [Test]
    public void PivotStructure_FromTopLeft_TransitionsToTopRight()
    {
        // Arrange
        testObject.pastingPivotedX = 1;
        testObject.pastingPivotedY = 1;

        // Act
        testObject.PivotStructure();

        // Assert
        Assert.AreEqual(0, testObject.pastingPivotedX, "X should be 0 after pivoting from top left");
        Assert.AreEqual(1, testObject.pastingPivotedY, "Y should be 1 after pivoting from top left");
    }

    [Test]
    public void PivotStructure_FromTopRight_TransitionsToBottomRight()
    {
        // Arrange
        testObject.pastingPivotedX = 0;
        testObject.pastingPivotedY = 1;

        // Act
        testObject.PivotStructure();

        // Assert
        Assert.AreEqual(0, testObject.pastingPivotedX, "X should be 0 after pivoting from top right");
        Assert.AreEqual(0, testObject.pastingPivotedY, "Y should be 0 after pivoting from top right");
    }

    [Test]
    public void PivotStructure_FullCycle_ReturnsToInitialState()
    {
        // Arrange
        testObject.pastingPivotedX = 0;
        testObject.pastingPivotedY = 0;

        // Act - Pivot 4 times to complete full cycle
        testObject.PivotStructure(); // 0,0 -> 1,0
        testObject.PivotStructure(); // 1,0 -> 1,1
        testObject.PivotStructure(); // 1,1 -> 0,1
        testObject.PivotStructure(); // 0,1 -> 0,0

        // Assert
        Assert.AreEqual(0, testObject.pastingPivotedX, "X should return to 0 after full cycle");
        Assert.AreEqual(0, testObject.pastingPivotedY, "Y should return to 0 after full cycle");
    }

    [Test]
    public void PivotStructure_InvalidState_DoesNothing()
    {
        // Arrange - Set to an invalid state not covered by any if statement
        testObject.pastingPivotedX = 2;
        testObject.pastingPivotedY = 2;

        // Act
        testObject.PivotStructure();

        // Assert - Values should remain unchanged
        Assert.AreEqual(2, testObject.pastingPivotedX, "X should remain unchanged for invalid state");
        Assert.AreEqual(2, testObject.pastingPivotedY, "Y should remain unchanged for invalid state");
    }

    [Test]
    public void PivotStructure_NegativeValues_DoesNothing()
    {
        // Arrange
        testObject.pastingPivotedX = -1;
        testObject.pastingPivotedY = -1;

        // Act
        testObject.PivotStructure();

        // Assert
        Assert.AreEqual(-1, testObject.pastingPivotedX, "X should remain unchanged for negative values");
        Assert.AreEqual(-1, testObject.pastingPivotedY, "Y should remain unchanged for negative values");
    }

    [Test]
    public void PivotStructure_MixedInvalidValues_DoesNothing()
    {
        // Arrange - X is valid but Y is not
        testObject.pastingPivotedX = 0;
        testObject.pastingPivotedY = 5;

        // Act
        testObject.PivotStructure();

        // Assert
        Assert.AreEqual(0, testObject.pastingPivotedX, "X should remain unchanged when Y is invalid");
        Assert.AreEqual(5, testObject.pastingPivotedY, "Y should remain unchanged when invalid");
    }

    [Test]
    public void PivotStructure_MultipleConsecutivePivots_FollowsCorrectSequence()
    {
        // Arrange
        testObject.pastingPivotedX = 0;
        testObject.pastingPivotedY = 0;

        // Act & Assert - Test each pivot step
        testObject.PivotStructure();
        Assert.AreEqual((1, 0), (testObject.pastingPivotedX, testObject.pastingPivotedY), "First pivot");

        testObject.PivotStructure();
        Assert.AreEqual((1, 1), (testObject.pastingPivotedX, testObject.pastingPivotedY), "Second pivot");

        testObject.PivotStructure();
        Assert.AreEqual((0, 1), (testObject.pastingPivotedX, testObject.pastingPivotedY), "Third pivot");

        testObject.PivotStructure();
        Assert.AreEqual((0, 0), (testObject.pastingPivotedX, testObject.pastingPivotedY), "Fourth pivot");
    }

    [Test]
    public void PivotStructure_StartFromDifferentPositions_AllTransitionCorrectly()
    {
        // Test starting from position (1,0)
        testObject.pastingPivotedX = 1;
        testObject.pastingPivotedY = 0;
        testObject.PivotStructure();
        Assert.AreEqual((1, 1), (testObject.pastingPivotedX, testObject.pastingPivotedY), "From (1,0)");

        // Reset and test starting from position (1,1)
        testObject.pastingPivotedX = 1;
        testObject.pastingPivotedY = 1;
        testObject.PivotStructure();
        Assert.AreEqual((0, 1), (testObject.pastingPivotedX, testObject.pastingPivotedY), "From (1,1)");

        // Reset and test starting from position (0,1)
        testObject.pastingPivotedX = 0;
        testObject.pastingPivotedY = 1;
        testObject.PivotStructure();
        Assert.AreEqual((0, 0), (testObject.pastingPivotedX, testObject.pastingPivotedY), "From (0,1)");
    }

    [Test]
    public void PivotStructure_PartialInvalidState_DoesNothing()
    {
        // Arrange - X matches a valid pattern but Y doesn't form a valid state
        testObject.pastingPivotedX = 1;
        testObject.pastingPivotedY = 2;

        // Act
        testObject.PivotStructure();

        // Assert
        Assert.AreEqual(1, testObject.pastingPivotedX, "X should remain unchanged");
        Assert.AreEqual(2, testObject.pastingPivotedY, "Y should remain unchanged");
    }
}
public class ResetMarkedCellsTests
{
    private TestClass testObject;
    private GameObject mockCell1;
    private GameObject mockCell2;
    private MockCellScript mockScript1;
    private MockCellScript mockScript2;

    // Mock cell script class
    private class MockCellScript : MonoBehaviour
    {
        public int id;
    }

    // Mock class containing the method to test
    private class TestClass
    {
        public GameObject markedCell1;
        public GameObject markedCell2;
        public MockCellScript cell1Script;
        public MockCellScript cell2Script;

        public void ResetMarkedCells()
        {
            if (markedCell1 != null)
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
    }

    [SetUp]
    public void Setup()
    {
        testObject = new TestClass();
        mockCell1 = new GameObject("Cell1");
        mockCell2 = new GameObject("Cell2");
        mockScript1 = mockCell1.AddComponent<MockCellScript>();
        mockScript2 = mockCell2.AddComponent<MockCellScript>();
        mockScript1.id = 1;
        mockScript2.id = 2;
    }

    [TearDown]
    public void TearDown()
    {
        if (mockCell1 != null)
            Object.DestroyImmediate(mockCell1);
        if (mockCell2 != null)
            Object.DestroyImmediate(mockCell2);
    }

    [Test]
    public void ResetMarkedCells_BothCellsMarked_ResetsBothToNull()
    {
        // Arrange
        testObject.markedCell1 = mockCell1;
        testObject.markedCell2 = mockCell2;
        testObject.cell1Script = mockScript1;
        testObject.cell2Script = mockScript2;

        // Act
        testObject.ResetMarkedCells();

        // Assert
        Assert.IsNull(testObject.markedCell1, "markedCell1 should be null after reset");
        Assert.IsNull(testObject.markedCell2, "markedCell2 should be null after reset");
        Assert.IsNull(testObject.cell1Script, "cell1Script should be null after reset");
        Assert.IsNull(testObject.cell2Script, "cell2Script should be null after reset");
    }

    [Test]
    public void ResetMarkedCells_OnlyCell1Marked_ResetsOnlyCell1()
    {
        // Arrange
        testObject.markedCell1 = mockCell1;
        testObject.cell1Script = mockScript1;
        testObject.markedCell2 = null;
        testObject.cell2Script = null;

        // Act
        testObject.ResetMarkedCells();

        // Assert
        Assert.IsNull(testObject.markedCell1, "markedCell1 should be null after reset");
        Assert.IsNull(testObject.cell1Script, "cell1Script should be null after reset");
        Assert.IsNull(testObject.markedCell2, "markedCell2 should remain null");
        Assert.IsNull(testObject.cell2Script, "cell2Script should remain null");
    }

    [Test]
    public void ResetMarkedCells_OnlyCell2Marked_ResetsOnlyCell2()
    {
        // Arrange
        testObject.markedCell1 = null;
        testObject.cell1Script = null;
        testObject.markedCell2 = mockCell2;
        testObject.cell2Script = mockScript2;

        // Act
        testObject.ResetMarkedCells();

        // Assert
        Assert.IsNull(testObject.markedCell1, "markedCell1 should remain null");
        Assert.IsNull(testObject.cell1Script, "cell1Script should remain null");
        Assert.IsNull(testObject.markedCell2, "markedCell2 should be null after reset");
        Assert.IsNull(testObject.cell2Script, "cell2Script should be null after reset");
    }

    [Test]
    public void ResetMarkedCells_BothCellsNull_RemainsNull()
    {
        // Arrange
        testObject.markedCell1 = null;
        testObject.markedCell2 = null;
        testObject.cell1Script = null;
        testObject.cell2Script = null;

        // Act
        testObject.ResetMarkedCells();

        // Assert
        Assert.IsNull(testObject.markedCell1, "markedCell1 should remain null");
        Assert.IsNull(testObject.markedCell2, "markedCell2 should remain null");
        Assert.IsNull(testObject.cell1Script, "cell1Script should remain null");
        Assert.IsNull(testObject.cell2Script, "cell2Script should remain null");
    }

    [Test]
    public void ResetMarkedCells_Cell1MarkedButScriptNull_ResetsCell1Only()
    {
        // Arrange - Cell is marked but script reference is null
        testObject.markedCell1 = mockCell1;
        testObject.cell1Script = null;
        testObject.markedCell2 = null;
        testObject.cell2Script = null;

        // Act
        testObject.ResetMarkedCells();

        // Assert
        Assert.IsNull(testObject.markedCell1, "markedCell1 should be null after reset");
        Assert.IsNull(testObject.cell1Script, "cell1Script should remain null");
    }

    [Test]
    public void ResetMarkedCells_Cell2MarkedButScriptNull_ResetsCell2Only()
    {
        // Arrange - Cell is marked but script reference is null
        testObject.markedCell1 = null;
        testObject.cell1Script = null;
        testObject.markedCell2 = mockCell2;
        testObject.cell2Script = null;

        // Act
        testObject.ResetMarkedCells();

        // Assert
        Assert.IsNull(testObject.markedCell2, "markedCell2 should be null after reset");
        Assert.IsNull(testObject.cell2Script, "cell2Script should remain null");
    }

    [Test]
    public void ResetMarkedCells_CalledMultipleTimes_RemainsNull()
    {
        // Arrange
        testObject.markedCell1 = mockCell1;
        testObject.markedCell2 = mockCell2;
        testObject.cell1Script = mockScript1;
        testObject.cell2Script = mockScript2;

        // Act - Call reset multiple times
        testObject.ResetMarkedCells();
        testObject.ResetMarkedCells();
        testObject.ResetMarkedCells();

        // Assert
        Assert.IsNull(testObject.markedCell1, "markedCell1 should remain null after multiple resets");
        Assert.IsNull(testObject.markedCell2, "markedCell2 should remain null after multiple resets");
        Assert.IsNull(testObject.cell1Script, "cell1Script should remain null after multiple resets");
        Assert.IsNull(testObject.cell2Script, "cell2Script should remain null after multiple resets");
    }

    [Test]
    public void ResetMarkedCells_ScriptsSetButCellsNull_ScriptsUnaffected()
    {
        // Arrange - Edge case where scripts are set but cells are null
        testObject.markedCell1 = null;
        testObject.markedCell2 = null;
        testObject.cell1Script = mockScript1;
        testObject.cell2Script = mockScript2;

        // Act
        testObject.ResetMarkedCells();

        // Assert - Scripts should not be reset since cells are null
        Assert.IsNull(testObject.markedCell1, "markedCell1 should remain null");
        Assert.IsNull(testObject.markedCell2, "markedCell2 should remain null");
        Assert.IsNotNull(testObject.cell1Script, "cell1Script should not be reset when cell1 is null");
        Assert.IsNotNull(testObject.cell2Script, "cell2Script should not be reset when cell2 is null");
    }

    [Test]
    public void ResetMarkedCells_MixedState_ResetsOnlyNonNullCells()
    {
        // Arrange - Cell1 marked, Cell2 null initially
        testObject.markedCell1 = mockCell1;
        testObject.cell1Script = mockScript1;
        testObject.markedCell2 = null;
        testObject.cell2Script = null;

        // Act - First reset
        testObject.ResetMarkedCells();

        // Assert after first reset
        Assert.IsNull(testObject.markedCell1, "markedCell1 should be null after first reset");
        Assert.IsNull(testObject.cell1Script, "cell1Script should be null after first reset");

        // Arrange - Now mark Cell2
        testObject.markedCell2 = mockCell2;
        testObject.cell2Script = mockScript2;

        // Act - Second reset
        testObject.ResetMarkedCells();

        // Assert after second reset
        Assert.IsNull(testObject.markedCell2, "markedCell2 should be null after second reset");
        Assert.IsNull(testObject.cell2Script, "cell2Script should be null after second reset");
    }

    [Test]
    public void ResetMarkedCells_BothMarkedWithScripts_AllReferencesCleared()
    {
        // Arrange
        testObject.markedCell1 = mockCell1;
        testObject.markedCell2 = mockCell2;
        testObject.cell1Script = mockScript1;
        testObject.cell2Script = mockScript2;

        // Store references to verify they existed
        var cell1Ref = testObject.markedCell1;
        var cell2Ref = testObject.markedCell2;
        var script1Ref = testObject.cell1Script;
        var script2Ref = testObject.cell2Script;

        Assert.IsNotNull(cell1Ref, "Setup: cell1 should be assigned");
        Assert.IsNotNull(cell2Ref, "Setup: cell2 should be assigned");
        Assert.IsNotNull(script1Ref, "Setup: script1 should be assigned");
        Assert.IsNotNull(script2Ref, "Setup: script2 should be assigned");

        // Act
        testObject.ResetMarkedCells();

        // Assert
        Assert.IsNull(testObject.markedCell1, "markedCell1 reference should be cleared");
        Assert.IsNull(testObject.markedCell2, "markedCell2 reference should be cleared");
        Assert.IsNull(testObject.cell1Script, "cell1Script reference should be cleared");
        Assert.IsNull(testObject.cell2Script, "cell2Script reference should be cleared");
    }
}
public class UpdateColorTests
{
    private TestClass testObject;
    private GameObject mockGameObject;

    // Mock Colors class
    public static class Colors
    {
        public static Color boardColor = new Color(0.1f, 0.1f, 0.1f);
        public static Color red = new Color(1f, 0f, 0f);
        public static Color lightRed = new Color(1f, 0.3f, 0.3f);
        public static Color lighterRed = new Color(1f, 0.5f, 0.5f);
        public static Color white = Color.white;
        public static Color cyan = Color.cyan;
        public static Color darkBlue = new Color(0f, 0f, 0.5f);
        public static Color darkerBlue = new Color(0f, 0f, 0.3f);
    }

    // Mock class containing the method to test
    private class TestClass : MonoBehaviour
    {
        public int aliveFor;
        public SpriteRenderer spriteRenderer;

        public void UpdateColor()
        {
            switch (aliveFor)
            {
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
                    spriteRenderer.color = Color.green;
                    break;
            }
        }
    }

    [SetUp]
    public void Setup()
    {
        mockGameObject = new GameObject("TestCell");
        testObject = mockGameObject.AddComponent<TestClass>();
        testObject.spriteRenderer = mockGameObject.AddComponent<SpriteRenderer>();
    }

    [TearDown]
    public void TearDown()
    {
        if (mockGameObject != null)
            Object.DestroyImmediate(mockGameObject);
    }

    [Test]
    public void UpdateColor_AliveFor0_SetsBoardColor()
    {
        // Arrange
        testObject.aliveFor = 0;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.boardColor, testObject.spriteRenderer.color,
            "Color should be boardColor when aliveFor is 0");
    }

    [Test]
    public void UpdateColor_AliveFor1_SetsRed()
    {
        // Arrange
        testObject.aliveFor = 1;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.red, testObject.spriteRenderer.color,
            "Color should be red when aliveFor is 1");
    }

    [Test]
    public void UpdateColor_AliveFor2_SetsLightRed()
    {
        // Arrange
        testObject.aliveFor = 2;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.lightRed, testObject.spriteRenderer.color,
            "Color should be lightRed when aliveFor is 2");
    }

    [Test]
    public void UpdateColor_AliveFor3_SetsLighterRed()
    {
        // Arrange
        testObject.aliveFor = 3;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.lighterRed, testObject.spriteRenderer.color,
            "Color should be lighterRed when aliveFor is 3");
    }

    [Test]
    public void UpdateColor_AliveFor4_SetsWhite()
    {
        // Arrange
        testObject.aliveFor = 4;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.white, testObject.spriteRenderer.color,
            "Color should be white when aliveFor is 4");
    }

    [Test]
    public void UpdateColor_AliveFor5_SetsCyan()
    {
        // Arrange
        testObject.aliveFor = 5;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.cyan, testObject.spriteRenderer.color,
            "Color should be cyan when aliveFor is 5");
    }

    [Test]
    public void UpdateColor_AliveFor6_SetsDarkBlue()
    {
        // Arrange
        testObject.aliveFor = 6;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.darkBlue, testObject.spriteRenderer.color,
            "Color should be darkBlue when aliveFor is 6");
    }

    [Test]
    public void UpdateColor_AliveFor7_SetsDarkerBlue()
    {
        // Arrange
        testObject.aliveFor = 7;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Colors.darkerBlue, testObject.spriteRenderer.color,
            "Color should be darkerBlue when aliveFor is 7");
    }

    [Test]
    public void UpdateColor_AliveFor8_SetsGreen()
    {
        // Arrange
        testObject.aliveFor = 8;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, testObject.spriteRenderer.color,
            "Color should be green (error color) when aliveFor is 8");
    }

    [Test]
    public void UpdateColor_NegativeValue_SetsGreen()
    {
        // Arrange
        testObject.aliveFor = -1;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, testObject.spriteRenderer.color,
            "Color should be green (error color) when aliveFor is negative");
    }

    [Test]
    public void UpdateColor_LargePositiveValue_SetsGreen()
    {
        // Arrange
        testObject.aliveFor = 100;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, testObject.spriteRenderer.color,
            "Color should be green (error color) when aliveFor is large positive");
    }

    [Test]
    public void UpdateColor_LargeNegativeValue_SetsGreen()
    {
        // Arrange
        testObject.aliveFor = -999;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, testObject.spriteRenderer.color,
            "Color should be green (error color) when aliveFor is large negative");
    }

    [Test]
    public void UpdateColor_SequentialCalls_UpdatesColorCorrectly()
    {
        // Test transitioning through multiple states
        testObject.aliveFor = 0;
        testObject.UpdateColor();
        Assert.AreEqual(Colors.boardColor, testObject.spriteRenderer.color, "First state");

        testObject.aliveFor = 1;
        testObject.UpdateColor();
        Assert.AreEqual(Colors.red, testObject.spriteRenderer.color, "Second state");

        testObject.aliveFor = 5;
        testObject.UpdateColor();
        Assert.AreEqual(Colors.cyan, testObject.spriteRenderer.color, "Third state");

        testObject.aliveFor = 7;
        testObject.UpdateColor();
        Assert.AreEqual(Colors.darkerBlue, testObject.spriteRenderer.color, "Fourth state");
    }

    [Test]
    public void UpdateColor_AllValidCases_NoExceptions()
    {
        // Test all valid cases don't throw exceptions
        for (int i = 0; i <= 7; i++)
        {
            testObject.aliveFor = i;
            Assert.DoesNotThrow(() => testObject.UpdateColor(),
                $"UpdateColor should not throw exception for aliveFor = {i}");
        }
    }

    [Test]
    public void UpdateColor_BoundaryCase_JustBelowValid_SetsGreen()
    {
        // Arrange
        testObject.aliveFor = -1;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, testObject.spriteRenderer.color,
            "Color should be green for value just below valid range");
    }

    [Test]
    public void UpdateColor_BoundaryCase_JustAboveValid_SetsGreen()
    {
        // Arrange
        testObject.aliveFor = 8;

        // Act
        testObject.UpdateColor();

        // Assert
        Assert.AreEqual(Color.green, testObject.spriteRenderer.color,
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
            testObject.aliveFor = i;
            testObject.UpdateColor();
            Assert.AreEqual(expectedColors[i], testObject.spriteRenderer.color,
                $"Color mismatch at aliveFor = {i}");
        }
    }

    [Test]
    public void UpdateColor_RepeatedCalls_SameValue_MaintainsColor()
    {
        // Arrange
        testObject.aliveFor = 3;

        // Act - Call multiple times with same value
        testObject.UpdateColor();
        Color firstColor = testObject.spriteRenderer.color;
        testObject.UpdateColor();
        Color secondColor = testObject.spriteRenderer.color;
        testObject.UpdateColor();
        Color thirdColor = testObject.spriteRenderer.color;

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
            testObject.aliveFor = value;
            testObject.UpdateColor();
            Assert.AreEqual(Color.green, testObject.spriteRenderer.color,
                $"Color should be green for invalid aliveFor value: {value}");
        }
    }
}