using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuitButtonController : MonoBehaviour
{
    public GameObject quitCanvas;
    public void ShowCanvas(){
        quitCanvas.SetActive(true);
        Time.timeScale = 0;
        SpeechSoundManager.current.audioSource.Pause();
    }

    public void HideCanvas(){
        quitCanvas.SetActive(false);
        Time.timeScale = 1;
        SpeechSoundManager.current.audioSource.Play();
    }
}
