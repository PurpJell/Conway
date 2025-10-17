using System;

// Simplified Cell class for coverage analysis
public class Cell
{
    public bool isAlive = false;
    public bool isMarked = false;
    public string currentColor = "white";
    public int x;
    public int y;
    
    public Cell(int x, int y)
    {
        this.x = x;
        this.y = y;
    }

    public void SetAlive(bool alive)
    {
        isAlive = alive;
    }

    public void SetMarked(bool marked)
    {
        isMarked = marked;
    }

    public void SetColor(string color)
    {
        currentColor = color ?? "white";
    }

    public bool IsNeighbor(Cell other)
    {
        if (other == null) return false;
        
        int dx = Math.Abs(this.x - other.x);
        int dy = Math.Abs(this.y - other.y);
        
        return (dx == 1 && dy == 0) || (dx == 0 && dy == 1) || (dx == 1 && dy == 1);
    }
}