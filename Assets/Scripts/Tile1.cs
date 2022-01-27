using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile1 : MonoBehaviour
{
	private static Color selectedColor = new Color(.5f, .5f, .5f, 1.0f);
	private static Tile1 previousSelected = null;

	private SpriteRenderer render;
	private bool isSelected = false;

	private Vector2[] adjacentDirections = new Vector2[] { Vector2.up, Vector2.down, Vector2.left, Vector2.right };
	private bool matchFound = false;

	void Awake()
	{

		render = GetComponent<SpriteRenderer>();
	}

	private void Select()
	{
		isSelected = true;
		render.color = selectedColor;
		previousSelected = gameObject.GetComponent<Tile1>();
	}

	private void Deselect()
	{
		isSelected = false;
		render.color = Color.white;
		previousSelected = null;
	}

	void OnMouseDown()
	{
		// 1
		if (render.sprite == null || BoardManager.instance.IsShifting)
		{
			return;
		}

		/*if (isSelected)
		{ // 2 Is it already selected?
			Deselect();
		}*/
		else
		{
			if (previousSelected == null)
			{ // 3 Is it the first tile selected?
				Select();
				//SwapSprite(previousSelected.render);
				previousSelected.ClearAllMatches();
				//previousSelected.Deselect();
				//ClearAllMatches();
			}
			else
			{
				previousSelected.Deselect(); // 4
			}
		}
	}

	/*public void SwapSprite(SpriteRenderer render1)
	{ // 1
	  //if (render.sprite == render2.sprite)
	  //{ // 2
	  //	return;
	  //}

		//Sprite tempSprite = render2.sprite; // 3
		//render2.sprite = render.sprite; // 4
		//render.sprite = tempSprite; // 5
		//SFXManager.instance.PlaySFX(Clip.Swap); // 6
	}*/

	private List<GameObject> FindMatch(Vector2 castDir)
	{ // 1
		List<GameObject> matchingTiles = new List<GameObject>(); // 2
		RaycastHit2D hit = Physics2D.Raycast(transform.position, castDir); // 3
		while (hit.collider != null)// && hit.collider.GetComponent<SpriteRenderer>().sprite == render.sprite)
		{ // 4
			matchingTiles.Add(hit.collider.gameObject);
			hit = Physics2D.Raycast(hit.collider.transform.position, castDir);
		}
		return matchingTiles; // 5
	}

	private void ClearMatch(Vector2[] paths) // 1
	{
		List<GameObject> matchingTiles = new List<GameObject>(); // 2
		for (int i = 0; i < paths.Length; i++) // 3
		{
			matchingTiles.AddRange(FindMatch(paths[i]));
		}
		/*if (matchingTiles.Count >= 2) // 4
		{
			for (int i = 0; i < matchingTiles.Count; i++) // 5
			{
				matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
			}
			matchFound = true; // 6
		}*/
	}


	public void ClearAllMatches()
	{
		if (render.sprite == null)
			return;

		ClearMatch(new Vector2[2] { Vector2.left, Vector2.right });
		//ClearMatch(new Vector2[2] { Vector2.up, Vector2.down });
		if (matchFound)
		{
			render.sprite = null;
			matchFound = false;
			//StopCoroutine(BoardManager.instance.FindNullTiles());
			//StartCoroutine(BoardManager.instance.FindNullTiles());
		}
	}
}
