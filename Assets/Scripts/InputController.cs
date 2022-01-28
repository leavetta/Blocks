using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputController : MonoBehaviour
{

    List<GameObject> matchingTiles = new List<GameObject>();
    public bool check = false;
    private const int WIDTH_DEFAULT = 16;
    private const int HEIGHT_DEFAULT = 10;
    private const int COLORS_DEFAULT = 3;

    private void Start()
    {
        BoardManager.Instance.CreateBoard(WIDTH_DEFAULT, HEIGHT_DEFAULT, COLORS_DEFAULT);
        check = BoardManager.Instance.CheckMatches();
        Debug.Log(check);
    }

    void Update()
    {
        Vector2 CurMousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (Input.GetMouseButtonUp(0))
        {
            Debug.Log("*****");
            RaycastHit2D rayHit = Physics2D.Raycast(CurMousePos, Vector2.zero);
            RaycastHit2D rayHitNew = Physics2D.Raycast(CurMousePos, Vector2.zero);
            if (rayHit.transform != null)
            {
                Debug.Log("---");
                //Debug.Log("Selected object: " + rayHit.transform);
                //Debug.Log(CurMousePos + ":" + rayHit.transform.position + ":" + rayHit.collider.transform.position);

                //Vector2[] directions = new Vector2[4] { Vector2.left, Vector2.right, Vector2.up, Vector2.down };

                FindMatchMain(rayHit, CurMousePos, rayHitNew);
                DeleteMatch(rayHit);
                BoardManager.Instance.FallBlocks();
                check = BoardManager.Instance.CheckMatches();
                Debug.Log(check);
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
                //ClearMatchMain(rayHit, allHitsUp[1].transform.position, allHitsUp[1]);
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

        /*if (allHitsUp[1].transform != null
                    && allHitsUp[1].collider.GetComponent<SpriteRenderer>().sprite
                    == rayHit.collider.GetComponent<SpriteRenderer>().sprite)
        {
            if (!matchingTiles.Contains(rayHitNew.collider.gameObject))
            {
                matchingTiles.Add(rayHitNew.collider.gameObject);
                ClearMatchMain(rayHit, allHitsUp[1].transform.position, allHitsUp[1]);
            }
            else
                return;
        }

        if (allHitsDown[1].transform != null
                    && allHitsDown[1].collider.GetComponent<SpriteRenderer>().sprite
                    == rayHit.collider.GetComponent<SpriteRenderer>().sprite)
        {
            if (!matchingTiles.Contains(rayHitNew.collider.gameObject))
            {
                matchingTiles.Add(rayHitNew.collider.gameObject);
                ClearMatchMain(rayHit, allHitsDown[1].transform.position, allHitsDown[1]);
            }
            else
                return;
        }

        if (allHitsLeft[1].transform != null
                    && allHitsLeft[1].collider.GetComponent<SpriteRenderer>().sprite
                    == rayHit.collider.GetComponent<SpriteRenderer>().sprite)
        {
            if (!matchingTiles.Contains(rayHitNew.collider.gameObject))
            {
                matchingTiles.Add(rayHitNew.collider.gameObject);
                ClearMatchMain(rayHit, allHitsLeft[1].transform.position, allHitsLeft[1]);
            }
            else
                return;
        }

        if (allHitsRight[1].transform != null
                    && allHitsRight[1].collider.GetComponent<SpriteRenderer>().sprite
                    == rayHit.collider.GetComponent<SpriteRenderer>().sprite)
        {
            if (!matchingTiles.Contains(rayHitNew.collider.gameObject))
            {
                matchingTiles.Add(rayHitNew.collider.gameObject);
                ClearMatchMain(rayHit, allHitsRight[1].transform.position, allHitsRight[1]);
            }
            else
                return;
        }*/

        //List<GameObject> matchingTiles = new List<GameObject>();
        /*int index = 0;
        for (int dir = 0; dir < directions.Length; dir++)
        {
            RaycastHit2D[] allHits = Physics2D.RaycastAll(CurMousePos, directions[dir]);

            //RaycastHit2D hit = Physics2D.Raycast(CurMousePos, directions[dir]); // 3
            //Debug.Log(rayHit.collider.GetComponent<SpriteRenderer>().sprite);
            for (int i = 1; i < allHits.Length; i++)
            {
                //Debug.Log(allHits[i].collider.transform.position);
                //Debug.Log(rayHit.collider.GetComponent<SpriteRenderer>().sprite);

                if (allHits[i].transform != null 
                    && allHits[i].collider.GetComponent<SpriteRenderer>().sprite
                    == rayHit.collider.GetComponent<SpriteRenderer>().sprite)
                {
                    matchingTiles.Add(allHits[i].collider.gameObject);
                    //allHits[i].collider.gameObject.GetComponent<SpriteRenderer>().sprite = null;
                    //hit = Physics2D.Raycast(hit.collider.transform.position, Vector2.up);
                    //Debug.Log(i);
                }
                else
                {
                    break;
                }
                //Debug.Log("-------------");
            }
        }*/

        //ClearMatchSecondary(directions, rayHit, CurMousePos, matchingTiles, index);

        /*if (matchingTiles.Count >= 2) // 4
        {
            for (int i = 0; i < matchingTiles.Count; i++) // 5
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            rayHit.collider.GetComponent<SpriteRenderer>().sprite = null;
            //matchFound = true; // 6
        }*/

        /*List<GameObject> matchingTiles = new List<GameObject>(); // 2
        for (int i = 0; i < directions.Length; i++) // 3
        {
            matchingTiles.AddRange(FindMatch(directions[i]));
        }
        if (matchingTiles.Count >= 2) // 4
        {
            for (int i = 0; i < matchingTiles.Count; i++) // 5
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            matchFound = true; // 6
        }*/
    }

    private void DeleteMatch(RaycastHit2D rayHit)
    {
        if (matchingTiles.Count > 2) // 4
        {
            for (int i = 0; i < matchingTiles.Count; i++) // 5
            {
                matchingTiles[i].GetComponent<SpriteRenderer>().sprite = null;
            }
            //rayHit.collider.GetComponent<SpriteRenderer>().sprite = null;
            //matchFound = true; // 6
        }
        matchingTiles.Clear();
    }

    private void ClearMatchSecondary(Vector2[] directions, RaycastHit2D rayHit, 
        Vector2 CurMousePos, List<GameObject> matchingTiles, int index)
    {
        List<GameObject> temp = new List<GameObject>();
        for (int dir = 0; dir < directions.Length; dir++)
        {
            RaycastHit2D[] allHits = Physics2D.RaycastAll(matchingTiles[index].transform.position, directions[dir]);

            //RaycastHit2D hit = Physics2D.Raycast(CurMousePos, directions[dir]); // 3
            //Debug.Log(rayHit.collider.GetComponent<SpriteRenderer>().sprite);
            for (int i = 1; i < allHits.Length; i++)
            {
                //Debug.Log(allHits[i].collider.transform.position);
                //Debug.Log(rayHit.collider.GetComponent<SpriteRenderer>().sprite);

                if (allHits[i].transform != null
                    && allHits[i].collider.GetComponent<SpriteRenderer>().sprite
                    == rayHit.collider.GetComponent<SpriteRenderer>().sprite)
                {
                    matchingTiles.Add(allHits[i].collider.gameObject);
                    //allHits[i].collider.gameObject.GetComponent<SpriteRenderer>().sprite = null;
                    /*hit = Physics2D.Raycast(hit.collider.transform.position, Vector2.up);*/
                    //Debug.Log(i);
                }
                else
                {
                    break;
                }
                //Debug.Log("*******");
            }
        }
        

        /*if (matchingTiles.Count - 1 > index)
        {
            index++;
            ClearMatchSecondary(directions, rayHit, CurMousePos, matchingTiles, index);
        }*/
            

    }
}
