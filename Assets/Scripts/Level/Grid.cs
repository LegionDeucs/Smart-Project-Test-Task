using UnityEngine;

[System.Serializable]
public class Grid
{
    private int height;
    private int width;

    private Cell[,] cells;

    public int Height => height;
    public int Width => width;

    public Grid(int width, int height)
    {
        this.height = height;
        this.width = width;
        cells = new Cell[height, width];

        for (int i = 0; i < height; i++)
        {
            for (int j = 0; j < width; j++)
            {
                cells[i, j] = new Cell(i, j, height, width);
            }
        }
    }

    public Cell GetCell(int h, int w)
    {
        return cells[h, w];
    }

    public bool TryGetCell(int w, int h, out Cell cell)
    {
        if (w >= 0 && h >= 0 && w < width && h < height)
        {
            cell = GetCell(h, w);
            return true;
        }
        cell = null;
        return false;
    }

    public Cell GetCell(Vector3 position)
    {
        if (position.x < 0 || position.z < 0 || position.x >= width || position.z >= height)
            return null;

        return cells[Mathf.RoundToInt(position.z), Mathf.RoundToInt(position.x)];
    }

    public Cell GetNextCell(Cell fromCell, Vector3Int direction)
    {
        (int, int) index = cells.IndexOf(fromCell);

        int x = index.Item2;
        int y = index.Item1;

        TryGetCell(x + direction.x, y + direction.z, out Cell cell);
        return cell;
    }
}
