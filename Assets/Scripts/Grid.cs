using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Grid : MonoBehaviour
{
    public List<Sprite> Sprites = new List<Sprite>();
    public GameObject TilePrefab;
    public int GridDimension = 8;
    public int xSize;
    public int ySize;
    public float Distance = 1.0f;
    private GameObject[,] grid;

    public static Grid Instance { get; private set; }

    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        grid = new GameObject[GridDimension, GridDimension];
        InitGrid();
    }

    public void DeleteBlocks(Vector2Int tile1Position) // 1
    {
        // 2
        GameObject tile1 = grid[tile1Position.x, tile1Position.y];
        Debug.Log(tile1Position.x + " " + tile1Position.y);
        SpriteRenderer renderer1 = tile1.GetComponent<SpriteRenderer>();

        // 3
        Sprite temp = renderer1.sprite;

        bool changesOccurs = CheckMatches(tile1Position, tile1, renderer1);

        /*if (changesOccurs)
        {
            do
            {
                FillHoles();
            } while (CheckMatches(tile1Position, tile1, renderer1));
        }*/
    }


    void InitGrid()
    {
        Vector3 positionOffset = transform.position - new Vector3(GridDimension * Distance / 2.0f, GridDimension * Distance / 2.0f, 0); // 1
        for (int row = 0; row < GridDimension; row++)
            for (int column = 0; column < GridDimension; column++) // 2
            {
                GameObject newTile = Instantiate(TilePrefab); // 3
                SpriteRenderer renderer = newTile.GetComponent<SpriteRenderer>(); // 4
                renderer.sprite = Sprites[Random.Range(0, 4)]; // 5
                newTile.transform.parent = transform; // 6
                newTile.transform.position = new Vector3(column * Distance, row * Distance, 0) + positionOffset; // 7

                grid[column, row] = newTile; // 8

                renderer = newTile.GetComponent<SpriteRenderer>();

                Tile tile = newTile.AddComponent<Tile>();

                newTile.transform.parent = transform;
            }
        
    }


    SpriteRenderer GetSpriteRendererAt(int column, int row)
    {
        if (column < 0 || column >= GridDimension
             || row < 0 || row >= GridDimension)
            return null;
        GameObject tile = grid[column, row];
        SpriteRenderer renderer = tile.GetComponent<SpriteRenderer>();
        return renderer;
    }

    bool CheckMatches(Vector2Int tile1Position, GameObject tile1, SpriteRenderer renderer1)
    {
        HashSet<SpriteRenderer> matchedTiles = new HashSet<SpriteRenderer>(); // 1
        for (int row = 0; row < GridDimension; row++)
        {
            for (int column = 0; column < GridDimension; column++) // 2
            {
                SpriteRenderer current = GetSpriteRendererAt(column, row); // 3

                List<SpriteRenderer> horizontalMatches = FindColumnMatchForTile(column, row, current.sprite); // 4
                if (horizontalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(horizontalMatches);
                    matchedTiles.Add(current); // 5
                }

                List<SpriteRenderer> verticalMatches = FindRowMatchForTile(column, row, current.sprite); // 6
                if (verticalMatches.Count >= 2)
                {
                    matchedTiles.UnionWith(verticalMatches);
                    matchedTiles.Add(current);
                }
            }
        }

        foreach (SpriteRenderer renderer in matchedTiles) // 7
        {
            renderer.sprite = null;
        }
        return matchedTiles.Count > 0; // 8
    }

    List<SpriteRenderer> FindColumnMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = col + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextColumn = GetSpriteRendererAt(i, row);
            if (nextColumn.sprite != sprite)
            {
                break;
            }
            result.Add(nextColumn);
        }
        return result;
    }

    List<SpriteRenderer> FindRowMatchForTile(int col, int row, Sprite sprite)
    {
        List<SpriteRenderer> result = new List<SpriteRenderer>();
        for (int i = row + 1; i < GridDimension; i++)
        {
            SpriteRenderer nextRow = GetSpriteRendererAt(col, i);
            if (nextRow.sprite != sprite)
            {
                break;
            }
            result.Add(nextRow);
        }
        return result;
    }

    /*void FillHoles()
    {
        for (int column = 0; column < GridDimension; column++)
        {
            for (int row = 0; row < GridDimension; row++) // 1
            {
                while (GetSpriteRendererAt(column, row).sprite == null) // 2
                {
                    for (int filler = row; filler < GridDimension - 1; filler++) // 3
                    {
                        SpriteRenderer current = GetSpriteRendererAt(column, filler); // 4
                        SpriteRenderer next = GetSpriteRendererAt(column, filler + 1);
                        current.sprite = next.sprite;
                    }
                    //SpriteRenderer last = GetSpriteRendererAt(column, GridDimension - 1);
                    //last.sprite = Sprites[Random.Range(0, Sprites.Count)]; // 5
                }
            }
        }
    }*/
}
