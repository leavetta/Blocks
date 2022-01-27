using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private static Tile selected; // 1 
    private SpriteRenderer Renderer; // 2

    public Vector2Int Position;

    private void Start() // 3
    {
        Renderer = GetComponent<SpriteRenderer>();
    }

    public void Select() // 4
    {
        Renderer.color = Color.grey;
        selected = gameObject.GetComponent<Tile>();
    }

    public void Unselect() // 5 
    {
        Renderer.color = Color.white;
    }

    private void OnMouseDown() //6
    {
        //if (selected != null)
        //{
        //    selected.Unselect();
        //}

        //selected = this;
        //Select();
        if (selected != null)
        {
            if (selected == this)
                return;
            selected.Unselect();
            selected = null;
        }
        else
        {
            selected = this;
            Select();
            Debug.Log(selected.transform.position.x + " " + selected.transform.position.y);
            Grid.Instance.DeleteBlocks(selected.Position);

        }
    }
}
