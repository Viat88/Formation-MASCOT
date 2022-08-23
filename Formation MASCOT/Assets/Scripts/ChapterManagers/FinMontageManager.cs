using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinMontageManager : MonoBehaviour
{
    public static FinMontageManager current;

    /*
        0: MOVE Jam to start
        1: PLAY speech Fin01 + PLAY video Fin01
        2: PLAY speech Fin02 + SHOW picture Fin01
        3: PLAY speech Fin03 + MOVE arrow
        4: HIDE screen + HIDE picture Fin01 + MOVE Jam mirror
        5: PLAY speech Fin04 + PLAY video Fin02
        6: PLAY speech Fin05 + HIDE screen
        7: MOVE Jam to middle
        8: PLAY speech Fin06
        9: SHOW buttons
        11: (SUIVANT PRESSED) TELL GlobalManager chapter finished
    */

    private int step = 0;
    public GameObject endButtons;
    public GameObject arrow;
    public List<Transform> arrowPositionList;
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
        GlobalManager.current.currentChapter = 6;
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

        if (step == 0 && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 1 && IsPreviousStepFinished()){
            PlaySpeech(0);
            PlayVideo(0);
            step += 1;
        }    

        if (step == 2 && IsPreviousStepFinished()){
            PlaySpeech(1);
            PlayPhoto(0);
            MoveArrow(arrowPositionList[0].position);
            step += 1;
        }    

        if (step == 3 && IsPreviousStepFinished()){
            PlaySpeech(2);
            MoveArrow(arrowPositionList[1].position);
            step += 1;
        }    

        if (step == 4 && IsPreviousStepFinished()){
            HideTheScreen(true);
            PhotoManager.current.ShowPhoto(false);
            MoveJam.current.MoveJamToMirror();
            step += 1;
        }

        if (step == 5 && IsPreviousStepFinished()){
            PlaySpeech(3);
            PlayVideo(1);
            step += 1;
        }  

        if (step == 6 && IsPreviousStepFinished()){
            PlaySpeech(4);
            HideTheScreen(true);
            step += 1;
        }     

        if (step == 7 && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToMiddle();
            step += 1;
        }    

        if (step == 8 && IsPreviousStepFinished()){
            PlaySpeech(5);
            step += 1;
        }   

        if (step == 9 && IsPreviousStepFinished()){
            ShowEndButtons(true);
            step += 1;
        }   

        if (step == 11 && IsPreviousStepFinished()){
            step = 0;
            GlobalManager.current.IsCurrentChapterFinished = true;
        }   
    }

/////////////////////////// PLAY /////////////////////////////////

    /*
        Play next video by telling to GlobalManager the new Index
        ENTRY:
            videoIndex: int, the index among videos of Presentation 
        EXIT:
            Nothing
    */
    private void PlayVideo(int videoIndex){
        GlobalManager.current.VideoIndex = GetList(videoIndex);
    }




    /*
        Play next photo by telling to GlobalManager the new Index
        ENTRY:
            photoIndex: int, the index among photos of Presentation 
        EXIT:
            Nothing
    */
    private void PlayPhoto(int photoIndex){
        GlobalManager.current.PhotoIndex = GetList(photoIndex);
    }


    /*
        Play next speech by calling PlaySupplyClip of SpeechSoundManager
        ENTRY:
            speechIndex: int, the index of the speech
        EXIT:
            Nothing
    */
    private void PlaySpeech(int speechIndex){
        SpeechSoundManager.current.PlayFinMontageClip(speechIndex);
    }

//////////////////////////// INDEX ////////////////////////////////

    /*
        Create a list of two int: (n1, n2) with:
         -n1 the chapter index
         -n2 the step index inside the chapter

        Input: 
         int n, corresponding to n2
        Output: 
         List<int> newIndex; the list of two int
    */
    private List<int> GetList(int n){

        List<int> newIndex = new List<int>();
        newIndex.Add(6);
        newIndex.Add(n);

        return newIndex;
    }

//////////////////////////// STEP FINISH ////////////////////////////////

    /*
        Return true if the audio source is playing
    */
    private bool IsAudioSourcePlaying(){
        return SpeechSoundManager.current.audioSource.isPlaying;
    }

    /*
        Return true if the video source is playing
    */
    private bool IsVideoPlayerPlaying(){
        return VideoManager.current.IsVideoPlayerPlaying();
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
        return !IsAudioSourcePlaying() && !IsJamMoving() && !IsVideoPlayerPlaying();
    }

///////////////////////////// HIDE THE SCREEN ///////////////////////////////

    /*
        Says to Global Manager if we have to hide the screen
        ENTRY:
            b: boolean, true= we have to hide the screen at the end
        EXIT:
            Nothing
    */
    private void HideTheScreen(bool b){
        GlobalManager.current.HideScreen = b;
    }

////////////////////////// ARROW //////////////////////////////////

    /*
        Set arrow position with the the position given

        Input:
         Vector3 targetPosition, position where arrow has to be
    */
    private void MoveArrow(Vector3 targetPosition){
        arrow.transform.position = targetPosition;
    }

///////////////////////// BUTTONS ///////////////////////////////////

    /*
        Show or Hide end buttons
        Entry: bool b, true = show and false = hide
    */
    private void ShowEndButtons(bool b){
        endButtons.SetActive(b);
    }

//////////////////////////// ENTRY FROM BUTTONS ////////////////////////////////

    /*
        Check the entry from buttons
        Call SetStep if the name of the button is known and an error warning otherwise
    */
    public void CheckEntry(string s){

        if (s == "RevoirButton"  || s == "NextButton"){
            EndButtons(s);
        }

        else{
            Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
        }
            
    }

    /*
        Get the index of the chapter choosen depending on which button has been pressed
    */
    private void EndButtons(string s){

        ShowEndButtons(false);

        if (s == "RevoirButton"){
            step = 0;
        }

        if (s == "NextButton"){
            step = 11;
        }
    }
}
