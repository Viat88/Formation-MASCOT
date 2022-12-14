using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PresManager : MonoBehaviour
{

    public static PresManager current;
    public GameObject buttons;

    /*
        a: Has to play Pres01 speech 
        b: Has to play Pres01 video with Pres02 speech
        c: Has to play Pres03 speech
        d: Has to show Pres02 picture
        e: Has to play Pres04 speech
        f: Has to move Jam to the right
        g: Has to play Pres05 speech
        h: Has to move Jam to the middle
        i: Has to play Pres06 speech depending on Player's gendary (SpechSoundManager will manage gendary alone)
        j: Show buttons: "REVOIR VIDEO", "REVOIR PHOTO" and "SUIVANT" 
        k: Has to tell GlobalManager if player press "SUIVANT"
    */

    private int step = 0;
    /*
        Steps:
        0: START a 
        1: FINISH a + START b
        2: FINISH b + START c
        3: FINISH c + START d and e
        4: PLAYING d + FINISH e + START f
        5: PLAYING d + FINISH f + START g
        6: FINISH d and g + START h
        7: FINISH h + START i
        8: START j

    */

    private bool isVideoRewatch = false;
    private bool isPhotoRewatch = false;
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
        GlobalManager.current.currentChapter = 1;
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

        if (step == -1){
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 0 && IsPreviousStepFinished()){                                         // 1st available rewatch

            if (isPhotoRewatch){
                step = 3;
            }
            else{
                PlaySpeech(0);
                step += 1;
            }
        }

        if (step == 1 && IsPreviousStepFinished()){
            PlaySpeech(1);
            PlayVideo(0);                                    // Play video Pres01
            step += 1;
        }
        
        if (step == 2 && IsPreviousStepFinished()){
            
            // Player only wanted to see again video
            if(isVideoRewatch){
                step = 6;
            }

            // It's the first watch for player
            else{
                PlaySpeech(2);
                step += 1;
            }
        }
        
        if (step == 3 && IsPreviousStepFinished()){              // 2nd available rewatch
            PlayPhoto(0);
            ShowArrow(true);
            MoveArrow(arrowPositionList[0].position);
            PlaySpeech(3);
            step += 1;
        }

        if (step == 4 && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToMirror();
            step += 1;
        }

        if (step == 5 && IsPreviousStepFinished()){
            MoveArrow(arrowPositionList[1].position);
            PlaySpeech(4);
            step += 1;
        }

        if (step == 6 && IsPreviousStepFinished()){
            PhotoManager.current.ShowPhoto(false);
            ShowArrow(false);
            HideTheScreen(true);
            MoveJam.current.MoveJamToMiddle();
            step += 1;
        }

        if (step == 7 && IsPreviousStepFinished()){
            PlaySpeech(5); 
            step += 1;
        }

        if (step == 8 && IsPreviousStepFinished()){
            ShowFinalButtons(true);
            step += 1;
        }

        if (step == 10){
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
        SpeechSoundManager.current.PlayPresClip(speechIndex);
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
        return !IsAudioSourcePlaying() && !IsVideoPlayerPlaying() && !IsJamMoving();
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
        newIndex.Add(1);
        newIndex.Add(n);

        return newIndex;
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

    /*
        Show or Hide arrow
        Entry: bool b, true = show and false = hide
    */
    private void ShowArrow(bool b){
        arrow.SetActive(b);
    }

///////////////////////// BUTTONS ///////////////////////////////////

    /*
        Show or Hide final buttons
        Entry: bool b, true = show and false = hide
    */
    public void ShowFinalButtons(bool b){
        buttons.SetActive(b);
    }

//////////////////////////// ENTRY FROM BUTTONS ////////////////////////////////

    /*
        Check the entry from buttons
        Call EndButtons if the name of the button corresponds to the name of an end buttons
        Call ChoiceButtons if the name of the button corresponds to the name of a choice buttons
        An error warning otherwise
    */
    public void CheckEntry(string s){

        if (s == "VideoButton" || s == "PhotoButton" || s == "NextButton"){
            SetStep(s);
        }

        else{
            Debug.Log("Erreur: le string rentr?? en argument ne correspond pas ?? une des options");
        }
    }

    private void SetStep(string s){

        ShowFinalButtons(false);
        
        if (s == "VideoButton"){
            step = -1;
            isVideoRewatch = true;
        }

        if (s == "PhotoButton"){
            step = -1;
            isPhotoRewatch = true;
        }
            
        if (s == "NextButton"){
            step = 10;
        }
    }
    
}
