using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

public class CustomGrid
{
    private int width;
    public int Width
    {
        get => width;
    }
    private int height;
    public int Height
    {
        get => height;
    }
    private Vector3 cellSize;
    private Tile[,] gridArr;

    public CustomGrid(int width, int height, Vector3 cellSize)
    {
        gridArr = new Tile[width, height];
        this.width = width;
        this.height = height;
        this.cellSize = cellSize;

        BuildGrid();
    }

    private void BuildGrid()
    {
        for (int x = 0; x < width; x++)
        {
            for (int z = 0; z < height; z++)
            {
                gridArr[x, z] = new Tile(x, z);
                gridArr[x, z].walkable = Random.Range(1, 5) != 1;
                CreateWorldText(gridArr[x, z].ToString(), null, GetWorldPosition(x, z) + new Vector3(cellSize.x, 0, cellSize.y) / 2, 8, gridArr[x, z].walkable ? Color.green : Color.red, TextAnchor.MiddleCenter);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x, z + 1), gridArr[x, z].walkable ? Color.green : Color.red, 1000f);
                Debug.DrawLine(GetWorldPosition(x, z), GetWorldPosition(x + 1, z), gridArr[x, z].walkable ? Color.green : Color.red, 1000f);
            }
        }
    }

    private Vector3 GetWorldPosition(int x, int z)
    {
        return new Vector3(x, 0, z);
    }

    private void GetXZ(Vector3 worldPosition, out int x, out int z)
    {
        x = (int)(worldPosition.x / cellSize.x);
        z = (int)(worldPosition.z / cellSize.y);
    }

    public Tile GetTile(int x, int z)
    {
        return gridArr[x, z];
    }
    
    public int GetMaxSize()
    {
        return Height * Width;
    }

    public List<Tile> GetNeighbourTiles(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0) { continue;}
                int checkX = tile.x + x;
                int checkZ = tile.y + z;

                if (checkX >= 0 && checkX < width && checkZ >= 0 && checkZ < height)
                {
                    neighbours.Add(GetTile(checkX, checkZ));
                }
            }
        }

        return neighbours;
    }
    
    public List<Tile> GetMovableNeighbourTiles(Tile tile)
    {
        List<Tile> neighbours = new List<Tile>();
        for (int x = -1; x <= 1; x++)
        {
            for (int z = -1; z <= 1; z++)
            {
                if (x == 0 && z == 0) { continue;}
                
                int checkX = tile.x + x;
                int checkZ = tile.y + z;
                
                if (checkX >= 0 && checkX < width && checkZ >= 0 && checkZ < height)
                {
                    //Check for cutting corners
                    if (x != 0 && z != 0)
                    {
                        if (!GetTile(checkX, tile.y).walkable && !GetTile(tile.x, checkZ).walkable)
                        {
                            continue;
                        }
                    }
                    neighbours.Add(GetTile(checkX, checkZ));
                }
            }
        }

        return neighbours;
    }

    public int GetDistance(Tile a, Tile b)
    {
        int distanceX = Mathf.Abs(a.x - b.x);
        int distanceY = Mathf.Abs(a.y - b.y);

        return distanceX > distanceY
            ? 14 * distanceY + 10 * (distanceX - distanceY)
            : 14 * distanceX + 10 * (distanceY - distanceX);
    }


    private TextMesh CreateWorldText(string text, Transform parent,  Vector3 localPosition = default(Vector3), int fontSize = 20, Color color = default,
        TextAnchor textAnchor = TextAnchor.MiddleCenter, TextAlignment textAlignment = TextAlignment.Center, int sortingOrder = 1)
    {
        GameObject gameObject = new GameObject("World_Text", typeof(TextMesh));
        Transform transform = gameObject.transform;
        transform.SetParent(parent, false);
        transform.localPosition = localPosition;
        transform.Rotate(90f, 0f, 90f);
        TextMesh textMesh = gameObject.GetComponent<TextMesh>();
        textMesh.anchor = textAnchor;
        textMesh.alignment = textAlignment;
        textMesh.text = text;
        textMesh.fontSize = fontSize;
        textMesh.color = color;
        textMesh.GetComponent<MeshRenderer>().sortingOrder = sortingOrder;
        return textMesh;
    }
}