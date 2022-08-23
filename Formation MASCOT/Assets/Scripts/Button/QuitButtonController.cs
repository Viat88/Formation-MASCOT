using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonController : MonoBehaviour
{
    /*
        Controller of UI button Quit, always in the top right hand corner of the screen
    */

    public GameObject quitCanvas;                                               // Canvas to show when player clicked on it

    /*
        Called directly on Unity when button is clicked
        It set active quitCanvas (= "are you sure to quit?") and change the game state to Paused
    */
    public void ShowCanvas(){
        quitCanvas.SetActive(true);
        GlobalManager.current.UpdateGameState(GameState.Paused);
    }


    /*
        Called directly on Unity when button "NON" is clicked
        It set inActive quitCanvas and change the game state to Running
    */
    public void HideCanvas(){
        quitCanvas.SetActive(false);
        GlobalManager.current.UpdateGameState(GameState.Running);
    }
}
