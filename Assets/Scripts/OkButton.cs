using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OkButton : MonoBehaviour
{
    public GameObject panel;
    public void OkButtonClick(){
        panel.SetActive(false);
    }
}
