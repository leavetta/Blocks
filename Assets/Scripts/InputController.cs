using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{
    List<GameObject> matchingTiles = new List<GameObject>();

    private bool _checkMoves = false;
    private const int WIDTH_DEFAULT = 16;
    private const int HEIGHT_DEFAULT = 10;
    private const int COLORS_DEFAULT = 3;

    public GameObject panelEndGame;

    private void Start()
    {
        BoardManager.Instance.CreateBoard(WIDTH_DEFAULT, HEIGHT_DEFAULT, COLORS_DEFAULT);
        _checkMoves = BoardManager.Instance.CheckMatches();
    }

    void Update()
    {
        Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            RaycastHit2D rayHit = Physics2D.Raycast(CurMousePos, Vector2.zero);
            RaycastHit2D rayHitNew = Physics2D.Raycast(CurMousePos, Vector2.zero);
            if (rayHit.transform != null)
            {
                FindMatchMain(rayHit, CurMousePos, rayHitNew);
                DeleteMatch(rayHit);

                BoardManager.Instance.FallBlocks();
                _checkMoves = BoardManager.Instance.CheckMatches();
                if (!_checkMoves)
                {
                    panelEndGame.SetActive(true);
                }
            }
        }
    }

    private void FindMatchMain(RaycastHit2D rayHit, Vector2 CurMousePos, RaycastHit2D rayHitNew) // 1
    {
        RaycastHit2D[] allHitsUp = Physics2D.RaycastAll(CurMousePos, Vector2.up);
        RaycastHit2D[] allHitsDown = Physics2D.RaycastAll(CurMousePos, Vector2.down);
        RaycastHit2D[] allHitsLeft = Physics2D.RaycastAll(CurMousePos, Vector2.left);
        RaycastHit2D[] allHitsRight = Physics2D.RaycastAll(CurMousePos, Vector2.right);

        if (rayHitNew.collider.GetComponent<SpriteRenderer>().sprite != null
                    && rayHitNew.collider.GetComponent<SpriteRenderer>().sprite
                    == rayHit.collider.GetComponent<SpriteRenderer>().sprite)
        {
            if (!matchingTiles.Contains(rayHitNew.collider.gameObject))
            {
                matchingTiles.Add(rayHitNew.collider.gameObject);
            }
            else
                return;
        }
        else
            return;
        if (allHitsUp.Length > 1 && allHitsUp[1].transform != null)
            FindMatchMain(rayHit, allHitsUp[1].transform.position, allHitsUp[1]);
        if (allHitsDown.Length > 1 && allHitsDown[1].transform != null)
            FindMatchMain(rayHit, allHitsDown[1].transform.position, allHitsDown[1]);
        if (allHitsLeft.Length > 1 && allHitsLeft[1].transform != null)
            FindMatchMain(rayHit, allHitsLeft[1].transform.position, allHitsLeft[1]);
        if (allHitsRight.Length > 1 && allHitsRight[1].transform != null)
            FindMatchMain(rayHit, allHitsRight[1].transform.position, allHitsRight[1]);
    }

    private void DeleteMatch(RaycastHit2D rayHit)
    {
        if (matchingTiles.Count > 2)
        {
            for (int i = 0; i < matchingTiles.Count; i++)
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
        }
        matchingTiles.Clear();
    }

}
