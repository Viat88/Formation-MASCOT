using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputManager : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        CheckEntry();
    }


    private void CheckEntry(){
        if (Input.GetKeyDown("right")){
            Pass();
        }
    }


    private void Pass() {
        SpeechSoundManager.current.StopSound();
        VideoManager.current.StopVideo();

    }
}
