using NoctisDev.Pooling.Runtime.Services;
using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class LevelGenerator : MonoBehaviour
{
    [Inject]
    private IPoolService poolService;

    [SerializeField] private LevelMazeCell LevelTilePrefab;
    [SerializeField] private Vector2Int levelDimension;

    private Grid grid;

    public Grid Grid => grid;
    private void Awake()
    {
        CreateGrid();
    }

    private void Start()
    {
        bool needWinCellBuffer = true; 
        GenerateMaze(null, grid.GetCell(grid.Height / 2 + 1, grid.Width / 2 + 1), ref needWinCellBuffer);
    }

    public void CreateGrid()
    {
        grid = new Grid(levelDimension.x, levelDimension.y);

        for (int i = 0; i < grid.Height; i++)
        {
            for (int j = 0; j < grid.Width; j++)
            {
                var mazeCell = poolService.Spawn(LevelTilePrefab);
                mazeCell.transform.SetParent(transform);
                mazeCell.transform.localPosition = new Vector3(j - grid.Width / 2, 0, i - grid.Height / 2);
                grid.GetCell(i, j).MazeCell = mazeCell;

            }
        }

        transform.position = new Vector3(grid.Width/2, 0, grid.Height/2);
    }

    private void GenerateMaze(Cell previousCell,  Cell currentCell, ref bool winCellNeeded)
    {
        currentCell.IsVisited = true;
        ClearWalls(previousCell, currentCell);
        Cell nextCell;
        do
        {
            nextCell = GetNextUnvisitedCell(currentCell);
            if(winCellNeeded && nextCell == null)
            {
                winCellNeeded = false;
                currentCell.SetAsWin();
            }

            if (nextCell != null)
                GenerateMaze(currentCell, nextCell, ref winCellNeeded);
        }
        while (nextCell != null);
    }

    private Cell GetNextUnvisitedCell(Cell currentCell)
    {
        List<Cell> unvisitedCells = new List<Cell>();
        if (grid.TryGetCell(currentCell.WPosition - 1, currentCell.HPosition, out Cell cell))
            if (!cell.IsVisited)
                unvisitedCells.Add(cell);
        if (grid.TryGetCell(currentCell.WPosition + 1, currentCell.HPosition, out cell))
            if (!cell.IsVisited)
                unvisitedCells.Add(cell);
        if (grid.TryGetCell(currentCell.WPosition, currentCell.HPosition - 1, out cell))
            if (!cell.IsVisited)
                unvisitedCells.Add(cell);
        if (grid.TryGetCell(currentCell.WPosition, currentCell.HPosition + 1, out cell))
            if (!cell.IsVisited)
                unvisitedCells.Add(cell);

        if(unvisitedCells.Count > 0)
            return unvisitedCells.GetRandom();
        return null;
    }

    private void ClearWalls(Cell previousCell, Cell currentCell)
    {
        if(previousCell == null)
            return;

        Vector2Int moveDirection = new Vector2Int(currentCell.WPosition - previousCell.WPosition, currentCell.HPosition - previousCell.HPosition);

        if (moveDirection == Vector2Int.up)
        {
            previousCell.MazeCell.WallN.SetInactive();
            previousCell.AddOpenDirection(LevelGeneratorUtils.WallDirection.Up);
            currentCell.MazeCell.WallS.SetInactive();
            currentCell.AddOpenDirection(LevelGeneratorUtils.WallDirection.Down);
        }

        if (moveDirection == Vector2Int.down)
        {
            previousCell.MazeCell.WallS.SetInactive();
            previousCell.AddOpenDirection(LevelGeneratorUtils.WallDirection.Down);
            currentCell.MazeCell.WallN.SetInactive();
            currentCell.AddOpenDirection(LevelGeneratorUtils.WallDirection.Up);
        }

        if (moveDirection == Vector2Int.left)
        {
            previousCell.MazeCell.WallW.SetInactive();
            previousCell.AddOpenDirection(LevelGeneratorUtils.WallDirection.Left);
            currentCell.MazeCell.WallE.SetInactive();
            currentCell.AddOpenDirection(LevelGeneratorUtils.WallDirection.Right);
        }

        if (moveDirection == Vector2Int.right)
        {
            previousCell.MazeCell.WallE.SetInactive();
            previousCell.AddOpenDirection(LevelGeneratorUtils.WallDirection.Right);
            currentCell.MazeCell.WallW.SetInactive();
            currentCell.AddOpenDirection(LevelGeneratorUtils.WallDirection.Left);
        }
    }

    internal Vector2Int GetMazeSize()
    {
        return levelDimension;
    }
}
