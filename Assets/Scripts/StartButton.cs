using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartButton : MonoBehaviour
{
    public InputField widthInputField;
    public InputField heightInputField;
    public InputField colorsInputField;
    public GameObject panelErrorInput;
    public GameObject panelEndGame;

    public GameObject boardManager;

    private int _widthInt;
    private int _heightInt;
    private int _colorsInt;

    public void OnStartButtonClick()
    {
        if (widthInputField.text == "" 
            || heightInputField.text == "" 
            || colorsInputField.text == "")
        {
            panelEndGame.SetActive(false);
            panelErrorInput.SetActive(true);
        }
        else
        {
            _widthInt = int.Parse(widthInputField.text); //обработка ошибок
            _heightInt = int.Parse(heightInputField.text);
            _colorsInt = int.Parse(colorsInputField.text);

            if ((_widthInt > 50 || _widthInt < 10)
                 || (_heightInt > 50 || _heightInt < 10)
                 || (_colorsInt > 5 || _colorsInt < 2))
            {
                panelEndGame.SetActive(false);
                panelErrorInput.SetActive(true);
            }
            else
            {
                panelEndGame.SetActive(false);
                BoardManager.Instance.ClearBoardManager();
                GameObject newBoardManager = Instantiate(boardManager, new Vector3(-4.33f, -4.12f, 0), boardManager.transform.rotation);
                BoardManager.Instance.CreateBoard(_widthInt, _heightInt, _colorsInt);
            }
        }
        
    }
}
