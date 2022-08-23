using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndManager : MonoBehaviour
{

    public static EndManager current;
    private int step = 0;
    public GameObject canva;
    public List<Button> buttonsList;
    private int chapterChoosen;
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
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 1 && IsPreviousStepFinished()){
            PlaySpeech(0);
            step += 1;
        }

        if (step == 2 && IsPreviousStepFinished()){
            EnableButtons(true);
            ShowButtons(true);
            step += 1;
        }        


        if (step == 4){
            GlobalManager.current.chapterList[chapterChoosen].SetActive(true);
            step = 0;
            GlobalManager.current.currentChapter = chapterChoosen;
            gameObject.SetActive(false);
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
        SpeechSoundManager.current.PlayEndClip(speechIndex);
    }

//////////////////////////// STEP FINISH ////////////////////////////////

    /*
        Return true if the audio source is playing
    */
    private bool IsAudioSourcePlaying(){
        return SpeechSoundManager.current.audioSource.isPlaying;
    }

    /*
        Return true if Jam is moving
    */
    private bool IsJamMoving(){
        return !MoveJam.current.HasFinished();
    }


    /*
        Return true if the previous step is finished
    */
    private bool IsPreviousStepFinished(){
        return !IsAudioSourcePlaying() && !IsJamMoving();
    }

///////////////////////// BUTTONS ///////////////////////////////////

    /*
        Show or Hide buttons
        Entry: bool b, true = show and false = hide
    */
    private void ShowButtons(bool b){
        canva.SetActive(b);
    }

    /*
        Make buttons interactables or not 
        Entry: bool b, true = interactables and false = not interactables
    */
    private void EnableButtons(bool b){

        foreach(Button button in buttonsList){
            button.interactable = b; 
        }
    }

//////////////////////////// ENTRY FROM BUTTONS ////////////////////////////////

    /*
        Check the entry from buttons
        Call SetStep if the name of the button is known and an error warning otherwise
    */
    public void CheckEntry(string s){
        if (s == "Appro" || s == "Sécurité" || s == "Prémontage" || s == "Montage" || s == "FinMontage" || s == "PièceDéfectueuse"){
            SetStep(s);
        }
        else{
            Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
        }
    }

    /*
        Get the index of the chapter choosen depending on which button has been pressed
    */
    private void SetStep( string s){

        ShowButtons(false);

        if (s == "Appro"){
            chapterChoosen = 2;
        }

        if (s == "Sécurité"){
            chapterChoosen = 3;
        }

        if (s == "Prémontage"){
            chapterChoosen = 4;
        }

        if (s == "Montage"){
            chapterChoosen = 5;
        }

        if (s == "FinMontage"){
            chapterChoosen = 6;
        }

        if (s == "PièceDéfectueuse"){
            chapterChoosen = 7;
        }

        step = 4;
    }
}
