using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RestartButton : MonoBehaviour
{
    public GameObject panelEndGame;
    public void RestartGame()
    {
        panelEndGame.SetActive(false);
        SceneManager.LoadScene(0);
    }
}
