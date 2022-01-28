using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public List<Sprite> listOfSprites = new List<Sprite>();
	public GameObject tile;
	private int _xSize, _ySize;

	private GameObject[,] _tiles;

	private List<GameObject> checkMatchingTiles = new List<GameObject>();

	public static BoardManager Instance { get; private set; }

	public bool IsAction { get; set; }

	void Awake()
	{
		Instance = this;
	}

	public void Start()
	{
		Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
	}

	public void CreateBoard(int width, int height, int colors)
	{
		_xSize = width;
		_ySize = height;

		float widthScreenSize = 11.0f;
		float heightScreenSize = 8.0f;
		float compareX, compareY;

		compareX = widthScreenSize / _xSize;
		compareY = heightScreenSize / _ySize;

		if (compareX <= compareY)
        {
			tile.transform.localScale = new Vector3(compareX, compareX, compareX);
		}
        else
        {
			tile.transform.localScale = new Vector3(compareY, compareY, compareY);
		}

		Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
		float xOffset = offset.x;
		float yOffset = offset.y;
		_tiles = new GameObject[_xSize, _ySize];

		List<int> colorsRange = new List<int>();

		for (int i = 0; i < colors; i++)
        {
			int temp = Random.Range(0, listOfSprites.Count);
			while (colorsRange.Contains(temp))
            {
				temp = Random.Range(0, listOfSprites.Count);
				
			}
			colorsRange.Add(temp);
		}

		float startX = transform.position.x;
		float startY = transform.position.y;

		for (int x = 0; x < _xSize; x++)
		{
			for (int y = 0; y < _ySize; y++)
			{
				GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
				_tiles[x, y] = newTile;

				newTile.transform.parent = transform;
				Sprite newSprite = listOfSprites[colorsRange[Random.Range(0, colors)]];
				newTile.GetComponent<SpriteRenderer>().sprite = newSprite;
			}
		}
		IsAction = false; 
	}

	public void FallBlocks()
	{
		for (int x = 0; x < _xSize; x++)
		{
			for (int y = 0; y < _ySize; y++)
			{
				if (_tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
				{
					ShiftTilesDown(x, y);
					break;
				}
			}
		}
	}

	private void ShiftTilesDown(int x, int yStart)
	{
		List<SpriteRenderer> renders = new List<SpriteRenderer>();
		int nullCount = 0;
		
		for (int y = yStart; y < _ySize; y++)
		{
			SpriteRenderer render = _tiles[x, y].GetComponent<SpriteRenderer>();
			if (render.sprite == null)
			{
				nullCount++;
			}
			renders.Add(render);
		}

		for (int y = _ySize - 1; y >= yStart; y--)
		{
			SpriteRenderer render = _tiles[x, y].GetComponent<SpriteRenderer>();
			if (render.sprite == null)
			{
				nullCount--;
			}
			else
				break;
		}

		int j = 0;
		for (int i = 0; i < nullCount; i++)
		{
			while (j < renders.Count - 1)
			{
				if (renders[j].sprite != null)
					j++;
				else
					break;
			}
			for (int k = j; k < renders.Count - 1; k++)
			{
				Sprite temp = renders[k].GetComponent<SpriteRenderer>().sprite;
				renders[k].sprite = renders[k + 1].sprite;
				renders[k + 1].sprite = temp;
			}
			j = 0;
		}
	}

	public void ClearBoardManager()
    {
		for (int x = 0; x < _xSize; x++)
		{
			for (int y = 0; y < _ySize; y++)
			{
				_tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
			}
		}

		Destroy(gameObject);
    }

	public bool CheckMatches()
    {
		for (int i = 0; i < _xSize; i++)
		{
			for (int j = 0; j < _ySize; j++)
			{
				CheckEveryOneTile(i, j, _tiles[i, j]);
				
				if (checkMatchingTiles.Count > 2 && IsAction)
                {
					checkMatchingTiles.Clear();
					return true;
				}
				else
					checkMatchingTiles.Clear();
			}
		}
		
		return false;
	}

	public void CheckEveryOneTile(int x, int y, GameObject comparer)
    {
		if (_tiles[x, y].GetComponent<SpriteRenderer>().sprite != null
			&& _tiles[x, y].GetComponent<SpriteRenderer>().sprite
			== comparer.GetComponent<SpriteRenderer>().sprite)
			if (!checkMatchingTiles.Contains(_tiles[x, y].gameObject))
			{
				checkMatchingTiles.Add(_tiles[x, y].gameObject);
				if (checkMatchingTiles.Count > 2)
                {
					IsAction = true;
					return;
				}
			}
			else
				return;
		else
			return;

		if (x > 0)
			CheckEveryOneTile(x - 1, y, comparer);
		if (y > 0)
			CheckEveryOneTile(x, y - 1, comparer);
		if (x < _xSize - 1)
			CheckEveryOneTile(x + 1, y, comparer);
		if (y < _ySize - 1)
			CheckEveryOneTile(x, y + 1, comparer);

	}
}
