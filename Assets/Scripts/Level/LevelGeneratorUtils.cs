using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LevelGeneratorUtils
{
    public enum WallDirection
    {
        None = 0,
        Up = 1 << 0,
        Down = 1 << 1,
        Left = 1 << 2,
        Right = 1 << 3
    }

    public static WallDirection FromVector(Vector3Int dir)
    {
        if (dir == Vector3Int.forward) return WallDirection.Up;
        if (dir == Vector3Int.back) return WallDirection.Down;
        if (dir == Vector3Int.left) return WallDirection.Left;
        if (dir == Vector3Int.right) return WallDirection.Right;
        return WallDirection.None;
    }

    public static Vector3Int ToVector(WallDirection dir)
    {
        return dir switch
        {
            WallDirection.Up => Vector3Int.forward,
            WallDirection.Down => Vector3Int.back,
            WallDirection.Left => Vector3Int.left,
            WallDirection.Right => Vector3Int.right,
            _ => Vector3Int.zero
        };
    }
}
