using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class quitGame : MonoBehaviour
{
    public void CloseGame()
    {
        Application.Quit();
        Debug.Log("App Closed!");
    }
}
