using UnityEngine;
using static LevelGeneratorUtils;

[System.Serializable]
public class Cell
{
    public int HPosition { get; private set; }
    public int WPosition { get; private set; }

    public bool IsVisited { get; set; }
    public LevelMazeCell MazeCell { get; set; }

    public Vector3 VisualPosition  => MazeCell.transform.position;
    public Vector3 GridPosition => new Vector3(WPosition, 0, HPosition);
    private WallDirection openWalls;

    public Cell(int i, int j, int height, int width)
    {
        this.HPosition = i;
        this.WPosition = j;
        openWalls = WallDirection.None;
    }

    internal void SetAsWin()
    {
        MazeCell.SetAsWin();
    }

    //if we want move in direction (x or y) || if wall opened in that direction || if both wall is opened (W,E or N,S)
    internal bool IsDirectionOpened(Vector3Int moveDirection)
    {
        var dir = FromVector(moveDirection);
        return (openWalls & dir) != 0;
    }

    internal void AddOpenDirection(WallDirection openDirection)
    {
       openWalls |= openDirection;
    }
}