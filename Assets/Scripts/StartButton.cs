using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public InputField widthInputField;
    public InputField heightInputField;
    public InputField colorsInputField;

    //public BoardManager boardManager;
    public GameObject boardManager;

    private int _widthInt;
    private int _heightInt;
    private int _colorsInt;

    /*void Start()
    {
        boardManager = Resources.Load<BoardManager>("BoardManager");
    }*/

    public void OnStartButtonClick()
    {

        _widthInt = int.Parse(widthInputField.text); //��������� ������
        _heightInt = int.Parse(heightInputField.text);
        _colorsInt = int.Parse(colorsInputField.text);
        
       /*if ((widthInt > 50 || widthInt < 10) 
            && (heightInt > 50 || heightInt < 10)
            && (colorsInt > 5 || colorsInt < 2))
        {

        }
        else
        {

        }*/

        BoardManager.Instance.ClearBoardManager();
        GameObject newBoardManager = Instantiate(boardManager, new Vector3(-3.71f, -3.97f, 0), boardManager.transform.rotation);
        //newBoardManager.transform.parent = transform;
        BoardManager.Instance.CreateBoard(_widthInt, _heightInt, _colorsInt);
    }
}