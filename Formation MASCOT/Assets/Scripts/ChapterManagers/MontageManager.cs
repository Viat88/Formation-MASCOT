using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MontageManager : MonoBehaviour
{

    public static MontageManager current;

    /*
        0: Move JAM
        1: PLAY speech mon01 + SHOW picture mon01
        2: PLAY speech mon02 + SHOW Arrow
        3: PLAY speech mon03 + MOVE Arrow
        4: PLAY speech mon04 + MOVE Arrow

        5: HIDE Arrow and picture mon01 + PLAY speech mon05 + PLAY video mon01
        6: PLAY speech mon06 + PLAY video mon02
        7: PLAY speech mon07 + PLAY video mon03 

        8: MOVE Jam to mirror + HIDE screen
        9: PLAY speech mon08
        10: PLAY speech mon09 + SHOW picture mon02
        11: HIDE picture mon02 + PLAY speech mon10 + PLAY video mon04

        12: MOVE Jam to middle + HIDE screen
        13: PLAY speech mon11 
        14: PLAY speech mon12 + SHOW button[0]
        15: PLAY speech mon13 + SHOW button[1]
        16: SHOW + ENABLE choiceButtons

        // Non Polaires choosen
        18: MOVE Jam 
        19: PLAY speech mon14
        20: PLAY speech mon15 + SHOW picture mon03
        21: HIDE picture mon03 + step = 30

        // Polaires choosen
        22: MOVE Jam to mirror
        23: PLAY speech mon16
        24: PLAY speech mon17 + PLAY video mon05
        25: PLAY speech mon18 
        26: PLAY speech mon19 + SHOW picture mon04 (3 different pictures)
        27: HIDE picture mon04 + PLAY speech mon20
        28: PLAY speech mon21 + PLAY video mon06
        29: step = 30

        30: HIDE screen + (IF both part seen: step +=1 ) (ELSE step = 16)
        31: MOVE Jam to middle
        32: PLAY speech mon22
        33: SHOW endButtons
        34: (SUIVANT pressed) TELL GlobalManager chapterFinished
    */

    private int step = 0;


    private List<bool> choiceSeenList; // List of four element, one for each part (fiche, Carroussel, MOS, Circuits) to know if they have been seen
    public List<Button> choiceButtonList;
    public GameObject endButtons;


    public GameObject arrow;
    public List<Transform> arrowPositionList;

    private bool computerRewatch = false;
    private bool suivantRewatch = false;

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
        GlobalManager.current.currentChapter = 5;
        InitialisePartSeenList(2);
    }

    void Update()
    {
        ManageStep();
    }

////////////////////////////////////////////////////////////

    private void ManageStep(){

        if (step == 0){
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 1 && IsPreviousStepFinished()){
            PlaySpeech(0);
            PlayPhoto(0);
            step += 1;
        }

        if (step == 2 && IsPreviousStepFinished()){
            PlaySpeech(1);
            ShowArrow(true);
            MoveArrow(arrowPositionList[0].position);
            step += 1;
        }

        if (step == 3 && IsPreviousStepFinished()){
            PlaySpeech(2);
            MoveArrow(arrowPositionList[1].position);
            step += 1;
        }

        if (step == 4 && IsPreviousStepFinished()){
            PlaySpeech(3);
            MoveArrow(arrowPositionList[2].position);
            step += 1;
        }

        if (step == 5 && IsPreviousStepFinished()){
            ShowArrow(false);
            PhotoManager.current.ShowPhoto(false);
            PlaySpeech(4);
            PlayVideo(0);
            step += 1;
        }

        if (step == 6 && IsPreviousStepFinished()){
            PlaySpeech(5);
            PlayVideo(1);
            step += 1;
        }

        if (step == 7 && IsPreviousStepFinished()){
            PlaySpeech(6);
            PlayVideo(2);
            step += 1;
        }

        if (step == 8 && IsPreviousStepFinished()){

            HideTheScreen(true);
            
            if (computerRewatch){
                step = 31;
                computerRewatch = false;
            }

            else{
                MoveJam.current.MoveJamToMirror();
                step += 1;
            }
            
        }

        if (step == 9 && IsPreviousStepFinished()){
            PlaySpeech(7);
            step += 1;
        }

        if (step == 10 && IsPreviousStepFinished()){
            PlaySpeech(8);
            PlayPhoto(1);
            step += 1;
        }

        if (step == 11 && IsPreviousStepFinished()){
            PhotoManager.current.ShowPhoto(false);
            PlaySpeech(9);
            PlayVideo(3);
            step += 1;
        }

        if (step == 12 && IsPreviousStepFinished()){
            HideTheScreen(true);

            if (suivantRewatch){
                step = 31;
                suivantRewatch = false;
            }

            else{
                MoveJam.current.MoveJamToMiddle();
                step += 1;
            }
            
        }

        if (step == 13 && IsPreviousStepFinished()){
            PlaySpeech(10);
            step += 1;
        }

        if (step == 14 && IsPreviousStepFinished()){
            PlaySpeech(11);
            ShowChoiceButton(0, true);
            step += 1;
        }

        if (step == 15 && IsPreviousStepFinished()){
            PlaySpeech(12);
            ShowChoiceButton(1, true);
            step += 1;
        }

        if (step == 16 && IsPreviousStepFinished()){

            ShowChoiceButton(0, true);
            ShowChoiceButton(1, true);

            EnableButtons(true);
            step += 1;
        }

/////////////////// Non Polaires choosen ///////////////////

        if (step == 18 && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 19 && IsPreviousStepFinished()){
            PlaySpeech(13);
            step += 1;
        }

        if (step == 20 && IsPreviousStepFinished()){
            PlaySpeech(14);
            PlayPhoto(02);
            step += 1;
        }

        if (step == 21 && IsPreviousStepFinished()){
            PhotoManager.current.ShowPhoto(false);
            SetElementPartSeenList(0, true);
            step = 30;
        }


/////////////////// Polaires choosen ///////////////////

        if (step == 22 && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToMirror();
            step += 1;
        }

        if (step == 23 && IsPreviousStepFinished()){
            PlaySpeech(15);
            step += 1;
        }

        if (step == 24 && IsPreviousStepFinished()){
            PlaySpeech(16);
            PlayVideo(4);
            step += 1;
        }

        if (step == 25 && IsPreviousStepFinished()){
            PlaySpeech(17);
            step += 1;
        }

        if (step == 26 && IsPreviousStepFinished()){
            PlaySpeech(18);
            PlayPhoto(3);
            step += 1;
        }

        if (step == 27 && IsPreviousStepFinished()){
            PhotoManager.current.ShowPhoto(false);
            PlaySpeech(19);
            step += 1;
        }

        if (step == 28 && IsPreviousStepFinished()){
            PlaySpeech(20);
            PlayVideo(5);
            step += 1;
        }

        if (step == 29 && IsPreviousStepFinished()){
            SetElementPartSeenList(1, true);
            step = 30;
        }



        if (step == 30 && IsPreviousStepFinished()){
            HideTheScreen(true);

            if (HaveAllPartBeenSeen()){
                step += 1;
            }

            else{
                step = 16;
            }
        }

        if (step == 31 && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToMiddle();
            step += 1;
        }

        if (step == 32 && IsPreviousStepFinished()){
            PlaySpeech(21);
            step += 1;
        }

        if (step == 33 && IsPreviousStepFinished()){
            ShowEndButtons(true);
            step += 1;
            Debug.Log("ok");
        }

        if (step == 35 && IsPreviousStepFinished()){
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
        SpeechSoundManager.current.PlayMontageClip(speechIndex);
    }

//////////////////////////// INDEX ////////////////////////////////

    private List<int> GetList(int n){

        List<int> newIndex = new List<int>();
        newIndex.Add(5);
        newIndex.Add(n);

        return newIndex;
    }

//////////////////////////// STEP FINISH ////////////////////////////////

    private bool IsAudioSourcePlaying(){
        return SpeechSoundManager.current.audioSource.isPlaying;
    }

    private bool IsVideoPlayerPlaying(){
        return VideoManager.current.IsVideoPlayerPlaying();
    }

    private bool IsJamMoving(){
        return !MoveJam.current.HasFinished();
    }

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

    private void InitialisePartSeenList(int listLength){

        choiceSeenList = new List<bool>();

        for (int i=0; i<listLength; i++){
            choiceSeenList.Add(false);;
        }         
    }

    private void SetElementPartSeenList(int n, bool b){
        choiceSeenList[n] = b;
    }

    private bool HaveAllPartBeenSeen(){
        return !choiceSeenList.Contains(false);
    }

////////////////////////// ARROW //////////////////////////////////

    private void MoveArrow(Vector3 targetPosition){
        arrow.transform.position = targetPosition;
    }

    private void ShowArrow(bool b){
        arrow.SetActive(b);
    }

///////////////////////// BUTTONS ///////////////////////////////////

    private void ShowChoiceButton(int n, bool b){
        
        choiceButtonList[n].gameObject.SetActive(b);
    }

    private void EnableButtons(bool b){

        foreach(Button button in choiceButtonList){
            button.interactable = b; 
        }
    }

    private void ShowEndButtons(bool b){
        endButtons.SetActive(b);
    }

//////////////////////////// ENTRY FROM BUTTONS ////////////////////////////////

    public void CheckEntry(string s){

        if (s == "OrdiButton" || s == "SuivantButton" || s == "ComposButton" || s == "NextButton"){
            EndButtons(s);
        }

        else{

            if(s == "Polaires" || s == "NonPolaires"){
                ChoiceButton(s);
            }

            else{
                Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
            }
            
        }
    }

    private void EndButtons(string s){

        ShowEndButtons(false);

        if (s == "OrdiButton"){
            computerRewatch = true;
            step = 0;
        }

        if (s == "SuivantButton"){
            suivantRewatch = true;
            step = 8;
        }

        if (s == "ComposButton"){
            step = 12;
            InitialisePartSeenList(2);
        }

        if (s == "NextButton"){
            step = 35;
        }
    }

    private void ChoiceButton(string s){

        ShowChoiceButton(0, false);
        ShowChoiceButton(1, false);

        if (s == "NonPolaires"){
            step = 18;
        }

        else{
            step = 22;
        }
    }
}
