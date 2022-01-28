using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
	public static BoardManager instance;
	public List<Sprite> characters = new List<Sprite>();
	public GameObject tile;
	public int xSize, ySize;

	private GameObject[,] tiles;

	public List<GameObject> matchingTiles = new List<GameObject>();
	public List<GameObject> checkMatchingTiles = new List<GameObject>();
	public bool check = false;

	public static BoardManager Instance { get; private set; }

	public bool IsShifting { get; set; }

	public bool IsAction { get; set; }

	void Awake()
	{
		Instance = this;
	}

	public void Start()
	{
		//instance = GetComponent<BoardManager>();

		Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
		Debug.Log(offset.x + " " + offset.y);
		//CreateBoard();
		//check = CheckMatches();
		//Debug.Log(check);
	}

	public void CreateBoard(int width, int height, int colors)
	{
		xSize = width;
		ySize = height;
		//instance = GetComponent<BoardManager>();

		float widthScreenSize = 11.0f;
		float heightScreenSize = 8.0f;
		float compareX = 0.7f, compareY = 0.7f;

		compareX = widthScreenSize / xSize;
		compareY = heightScreenSize / ySize;

		if (compareX <= compareY)
        {
			tile.transform.localScale = new Vector3(compareX, compareX, compareX);
		}
        else
        {
			tile.transform.localScale = new Vector3(compareY, compareY, compareY);
		}
		/*if (xSize >= 10 && xSize  < 20)
        {
			tile.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
		}
		else if (xSize >= 20 && xSize < 30)
		{
			tile.transform.localScale = new Vector3(0.4f, 0.4f, 0.4f);
		} 
		else if(xSize >= 30 && xSize < 40)
        {
			tile.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f);
		}
		else if(xSize >= 40 && xSize <= 50)
		{
			if(ySize >= 10 && ySize <= 25)
				tile.transform.localScale = new Vector3(0.25f, 0.25f, 0.25f);
			if()
		}*/

		/*if (ySize > 10 && xSize < 20)
        {
			tile.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
		}*/

		Vector2 offset = tile.GetComponent<SpriteRenderer>().bounds.size;
		float xOffset = offset.x;
		float yOffset = offset.y;
		tiles = new GameObject[xSize, ySize];
		List<int> colorsRange = new List<int>();
		for (int i = 0; i < colors; i++)
        {
			int temp = Random.Range(0, characters.Count);
			while (colorsRange.Contains(temp))
            {
				temp = Random.Range(0, characters.Count);
				
			}
			colorsRange.Add(temp);
		}

		float startX = transform.position.x;
		float startY = transform.position.y;

		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				GameObject newTile = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
				tiles[x, y] = newTile;

				newTile.transform.parent = transform; // 1
				Sprite newSprite = characters[colorsRange[Random.Range(0, colors)]]; // 2
				newTile.GetComponent<SpriteRenderer>().sprite = newSprite; // 3
			}
		}
		IsAction = false; 
	}

	public void FallBlocks()
	{
		//GameObject temp = Instantiate(tile, new Vector3(startX + (xOffset * x), startY + (yOffset * y), 0), tile.transform.rotation);
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				if (tiles[x, y].GetComponent<SpriteRenderer>().sprite == null)
				{
					//Debug.Log(y);
					ShiftTilesDown(x, y);
					break;
				}
			}
		}
	}

	private void ShiftTilesDown(int x, int yStart, float shiftDelay = .03f)
	{
		bool isFall = true;
		List<SpriteRenderer> renders = new List<SpriteRenderer>();
		int nullCount = 0;
		Debug.Log("x =" + x + "\nyStart = " + yStart + "\nySize =" + ySize);
		for (int y = yStart; y < ySize; y++)
		{  // 1
			SpriteRenderer render = tiles[x, y].GetComponent<SpriteRenderer>();
			if (render.sprite == null)
			{ // 2
				nullCount++;
			}
			renders.Add(render);
		}

		for (int y = ySize - 1; y >= yStart; y--)
		{
			SpriteRenderer render = tiles[x, y].GetComponent<SpriteRenderer>();
			if (render.sprite == null)
			{ // 2
				nullCount--;
			}
			else
				break;
		}

		//Debug.Log("nullCount = " + nullCount + "\nrenders.Count = " + renders.Count);
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
				/*if(renders[0].sprite != null)
                {
					renders.RemoveAt(0);
					continue;
				}*/
				Sprite temp = renders[k].GetComponent<SpriteRenderer>().sprite;
				/*Debug.Log("Count " + (i + 1) + "\ny " + (k + yStart) + "\ntempSprite" + temp + "\n"
					+ "renerKSprite" + renders[k].sprite + "\n" +
					"renderK1Sprite" + renders[k + 1].sprite);*/
				renders[k].sprite = renders[k + 1].sprite;
				renders[k + 1].sprite = temp;
			}
			j = 0;
		}
		IsShifting = false;
	}

	public void ClearBoardManager()
    {
		//tiles = new GameObject[0, 0];
		for (int x = 0; x < xSize; x++)
		{
			for (int y = 0; y < ySize; y++)
			{
				tiles[x, y].GetComponent<SpriteRenderer>().sprite = null;
			}
		}
		//characters.Clear();
		Destroy(gameObject);
    }

	public bool CheckMatches()
    {
		for (int i = 0; i < xSize; i++)
		{
			for (int j = 0; j < ySize; j++)
			{
				Debug.Log("x = " + i + "\ny = " + j);
				CheckEveryOneTile(i, j, tiles[i, j]);
				/*foreach(var check in checkMatchingTiles)
                {
					Debug.Log(check.GetComponent<SpriteRenderer>().sprite);
                }*/
				Debug.Log("=========================");
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
		if (tiles[x, y].GetComponent<SpriteRenderer>().sprite != null
			&& tiles[x, y].GetComponent<SpriteRenderer>().sprite
			== comparer.GetComponent<SpriteRenderer>().sprite)
			if (!checkMatchingTiles.Contains(tiles[x, y].gameObject))
			{
				checkMatchingTiles.Add(tiles[x, y].gameObject);
				if (checkMatchingTiles.Count > 2)
                {
					IsAction = true;
					return;
				}
				//ClearMatchMain(rayHit, allHitsUp[1].transform.position, allHitsUp[1]);
			}
			else
				return;
		else
			return;

		if (x > 0)
			CheckEveryOneTile(x - 1, y, comparer);
		if (y > 0)
			CheckEveryOneTile(x, y - 1, comparer);
		if (x < xSize - 1)
			CheckEveryOneTile(x + 1, y, comparer);
		if (y < ySize - 1)
			CheckEveryOneTile(x, y + 1, comparer);

	}
}
