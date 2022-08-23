using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonController : MonoBehaviour
{
    public GameObject quitCanvas;
    public void ShowCanvas(){
        quitCanvas.SetActive(true);
        GlobalManager.current.UpdateGameState(GameState.Paused);
    }

    public void HideCanvas(){
        quitCanvas.SetActive(false);
        GlobalManager.current.UpdateGameState(GameState.Running);
    }
}
