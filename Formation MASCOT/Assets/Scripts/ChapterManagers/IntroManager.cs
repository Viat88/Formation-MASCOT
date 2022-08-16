using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroManager : MonoBehaviour
{
    public static IntroManager current;                 // Unique IntroManager

    /*
        Has to play the Jam's intro speech
        Has to manage the two button "HOMME" and "FEMME" clickable by the player
        Has to play the last sentence
    */

   
    public GameObject introCanvas;

    [HideInInspector]
    public int step = 1;



///////////////////////// START FUNCTIONS ///////////////////////////////////

    void Awake() 
    {
        if (current == null)
        {
            current = this;
        }
        else
        {
            Destroy(obj: this);
        }

    }

    void Start()
    {
        SpeechSoundManager.current.PlayIntroSpeech();
    }


    void Update()
    {
       ManageStep();
    }


////////////////////////////////////////////////////////////
    private void ManageStep(){

        if (step==1 && !SpeechSoundManager.current.audioSource.isPlaying){
            introCanvas.SetActive(true);
            step += 1;
        }

        if(step == 3){
            introCanvas.SetActive(false);
            SpeechSoundManager.current.PlayLastSentenceIntro();
            step += 1;
        }
            
        if (step == 4 && !SpeechSoundManager.current.audioSource.isPlaying){
            step=5;
            GlobalManager.current.IsCurrentChapterFinished= true;  
        }

    }


    public void StepPlusOne(){
        step += 1;
    }
}
