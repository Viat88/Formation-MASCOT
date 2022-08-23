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

   
    public GameObject nextButton;
    private int step = 0;
    private bool isGamePaused;



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

        GlobalManager.OnGameStateChanged += IsGamePaused;
    }

    void Start()
    {
        GlobalManager.current.currentChapter = 0;
    }


    void Update()
    {
       if (!isGamePaused){
            ManageStep();
        }
    }

/////////////////////////// Game Paused /////////////////////////////////

    private void IsGamePaused(GameState state){
        isGamePaused = (state == GameState.Paused);
    }

/////////////////////////// Manage Steps /////////////////////////////////

    private void ManageStep(){

        if (step == 0){
            PlaySpeech(0);
            step += 1;
        }

        if (step==1 && IsPreviousStepFinished()){
            ShowButton(true);
            step += 1;
        }

        if(step == 3){
            ShowButton(false);
            PlaySpeech(1);
            step += 1;
        }
            
        if (step == 4 && IsPreviousStepFinished()){
            step = 0;
            GlobalManager.current.IsCurrentChapterFinished= true;  
        }

    }

/////////////////////////// PLAY /////////////////////////////////

   /*
        Play next speech by calling PlaySupplyClip of SpeechSoundManager
        ENTRY:
            speechIndex: int, the index of the speech
        EXIT:
            Nothing
    */
    private void PlaySpeech(int speechIndex){
        SpeechSoundManager.current.PlayIntroClip(speechIndex);
    }

//////////////////////////// STEP FINISH ////////////////////////////////

    /*
        Return true if the audio source is playing
    */
    private bool IsAudioSourcePlaying(){
        return SpeechSoundManager.current.audioSource.isPlaying;
    }

    /*
        Return true if the previous step is finished
    */
    private bool IsPreviousStepFinished(){
        return !IsAudioSourcePlaying() ;
    }

///////////////////////// BUTTONS ///////////////////////////////////

    /*
        Show or Hide buttons
        Entry: bool b, true = show and false = hide
    */
    private void ShowButton(bool b){
        nextButton.SetActive(b);
    }

//////////////////////////// ENTRY FROM BUTTONS ////////////////////////////////

    /*
        Check the entry from buttons
        Increase step by 1 if next button pressed
    */
    public void CheckEntry(string s){
        if ( s == "NextButton" ){
            step += 1;
        }
        else{
            Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
        }
    }

}
