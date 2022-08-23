using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PremontageManager : MonoBehaviour
{

    public static PremontageManager current;

    /*
        0: Replace JAM
        1: PLAY speech prem01
        2: PLAY video prem01 and speech prem02
        3: PLAY video prem02 and speech prem03
        4: PLAY video prem03 and speech prem04
        5: PLAY speech prem05 
        6: PLAY speech prem06
        7: SHOW 2 icons
        8: Enable buttons

        10: PLAY speech prem07 and video prem04

        11: PLAY speech prem08

        12: HIDE screen + (IF both done: step=13) (OR step=7)

        13: PLAY speech prem09 
        14: SHOW NextButton and step = 7

        15: (SUIVANT pressed) MOVE Jam to middle
        16: PLAY speech prem10
        17: TELL GLOBALMANAGER chapter finished
    */

    private int step = 0;
    private List<bool> partSeenList; // List of four element, one for each part (fiche, Carroussel, MOS, Circuits) to know if they have been seen
    public List<Button> stepButtonList;
    public Button nextButton;
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
        GlobalManager.current.currentChapter = 4;
        InitialisePartSeenList();
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
            PlaySpeech(1);
            PlayVideo(0);
            step += 1;
        }

        if (step == 3 && IsPreviousStepFinished()){
            PlaySpeech(2);
            PlayVideo(1);
            step += 1;
        }

        if (step == 4 && IsPreviousStepFinished()){
            PlaySpeech(3);
            PlayVideo(2);
            step += 1;
        }

        if (step == 5 && IsPreviousStepFinished()){
            HideTheScreen(true);
            PlaySpeech(4);
            step += 1;
        }

        if (step == 6 && IsPreviousStepFinished()){
            PlaySpeech(5);
            step += 1;
        }

        if (step == 7){
            ShowButtons(true);
            step += 1;
        }

        if (step == 8 && IsPreviousStepFinished()){
            EnableButtons(true);
            step += 1;
        }       

        if (step == 10){
            PlaySpeech(6);
            PlayVideo(3);
            SetElementPartSeenList(0, true);
            step = 12;
        } 

        if (step == 11 && IsPreviousStepFinished()){
            PlaySpeech(7);
            SetElementPartSeenList(1, true);
            step = 12;
        } 

        if (step == 12 && IsPreviousStepFinished() ){
            HideTheScreen(true);
            if (HaveAllPartBeenSeen()){
                step = 13;
            }

            else{
                step = 7;
            }
        } 

        if (step == 13){
            PlaySpeech(8);
            step += 1;
        }

        if (step==14 && IsPreviousStepFinished()){
            ShowNextButton(true);
            step = 7;
        }

        if (step == 15){
            MoveJam.current.MoveJamToMiddle();
            step += 1;
        }

        if (step == 16 && IsPreviousStepFinished()){
            PlaySpeech(9);
            step += 1;
        }

        if (step == 17 && IsPreviousStepFinished()){
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
        SpeechSoundManager.current.PlayPremontageClip(speechIndex);
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
        newIndex.Add(4);
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

/////////////////////////// PART SEEN LIST /////////////////////////////////

    /*
        Initialise choiceSeenList, the list of boolean telling if each choice has been selected

        Input:
         int listLength, the lenght of the list, ie, the number of choices
    */
    private void InitialisePartSeenList(){

        partSeenList = new List<bool>();
        partSeenList.Add(false);
        partSeenList.Add(false);        
    }

    /*
        Set the element of index n with the boolean value b
        
        Input:
         int n, the index where to set the value
         bool b, the boolean value to put
    */
    private void SetElementPartSeenList(int n, bool b){
        partSeenList[n] = b;
    }

    /*
        Tells if all choices have been selected
        
        Output:
         bool, false if one hasn't been selected (ie if there is a false in choiceSeenList), true otherwise
    */
    private bool HaveAllPartBeenSeen(){
        return partSeenList[0] && partSeenList[1];
    }

///////////////////////// BUTTONS ///////////////////////////////////

    /*
        Show or Hide all buttons
        Entry: bool b, true = show and false = hide
    */
    private void ShowButtons(bool b){
        foreach(Button button in stepButtonList){
            button.gameObject.SetActive(b);
        }
    }

    /*
        Show or Hide nextButton
        Entry: bool b, true = show and false = hide
    */
    private void ShowNextButton(bool b){
        nextButton.gameObject.SetActive(b);
    }

    /*
        Show or Hide buttons (stepButtonList and nextButton)
        Entry: bool b, true = show and false = hide
    */
    private void EnableButtons(bool b){

        foreach(Button button in stepButtonList){
            button.interactable = b; 
        }

        nextButton.interactable = b;
    }

//////////////////////////// ENTRY FROM BUTTONS ////////////////////////////////

    /*
        Check the entry from buttons
        Call EndButtons if the name of the button corresponds to the name of an end buttons
        Call ChoiceButtons if the name of the button corresponds to the name of a choice buttons
        An error warning otherwise
    */
    public void CheckEntry(string s){

        if (s == "EpargneButton" || s == "ToolsButton" || s == "NextButton"){
            SetSteps(s);
        }

        else{
            Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
        }
    }

    private void SetSteps(string s){

        ShowButtons(false);
        ShowNextButton(false);

        if (s == "EpargneButton"){
            step = 10;
        }

        if (s == "ToolsButton"){
            step = 11;
        }

        if (s == "NextButton"){
            step = 15;
        }

        
    }

}
