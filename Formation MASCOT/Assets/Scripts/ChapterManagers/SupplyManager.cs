using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SupplyManager : MonoBehaviour
{
    public static SupplyManager current;

    /*
        1: SHOW picture sup01 and PLAY speech sup01 
        2: PLAY speech sup02, SHOW arrow and SHOW button fiche technique
        3: PLAY speech sup03, MOVE arrow and SHOW button Carroussel
        4: PLAY speech sup04, MOVE arrow and SHOW button MOS
        5: HIDE arrow and picture sup01, PLAY speech sup05 and SHOW button circuits
        6: ENABLE all buttons
        7: WAIT for player to click a button

        Fiche technique:
        8: PLAY speech sup06 and PLAY video sup01
        9: PLAY video sup02 and PLAY speech sup07
        10: PLAY speech sup08 and video sup03
        11: WHEN finished: set partSeenList[0] to true and ENABLE buttons

        Carroussel:
        12: PLAY speech sup09 and video sup04
        13: WHEN finished: set partSeenList[1] to true and ENABLE buttons

        MOS:
        14: PLAY speech sup10 and SHOW pictures sup02 and sup03
        15: PLAY speech sup11 ans video sup05
        16: PLAY speech sup12 and video sup06
        17: WHEN finished: set partSeenList[2] to true and ENABLE buttons

        Circuits:
        18: PLAY speech sup13 and video sup07
        19: PLAY speech sup14
        20: PLAY speech sup15 and video sup08
        21: PLAY speech sup16 and video sup09
        22: WHEN finished: set partSeenList[2] to true and ENABLE buttons

        (all has been seen)
        23: Move Jam to Middle
        24: PLAY speech sup17
        25: Show button NEXT

        (Button next clicked)
        26: HIDE buttons and PLAY speech sup18

    */

    private List<bool> partSeenList; // List of four element, one for each part (fiche, Carroussel, MOS, Circuits) to know if they have been seen
    private int step = 0;

    public GameObject arrow;
    public List<Transform> arrowPositionList;
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
        GlobalManager.current.currentChapter = 2;
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

        if (step ==0){
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 1 && IsPreviousStepFinished()){
            PlayPhoto(0);
            PlaySpeech(0);
            step += 1;
        }

        if (step == 2 && IsPreviousStepFinished()){
            PlaySpeech(1);
            ShowArrow(true);
            ShowButton(0, true);
            step += 1;
        }

        if (step == 3 && IsPreviousStepFinished()){
            PlaySpeech(2);
            MoveArrow(arrowPositionList[1].position);
            ShowButton(1, true);
            step += 1;
        }

        if (step == 4 && IsPreviousStepFinished()){
            PlaySpeech(3);
            MoveArrow(arrowPositionList[2].position);
            ShowButton(2, true);
            step += 1;
        }

        if (step == 5 && IsPreviousStepFinished()){
            ShowArrow(false);
            PhotoManager.current.ShowPhoto(false);
            PlaySpeech(4);
            ShowButton(3, true);
            step += 1;
        }

        if (step==6 && IsPreviousStepFinished()){
            EnableButtons(true);
            step += 1;
        }

        if (step == 8 && IsPreviousStepFinished()){
            PlaySpeech(5);
            PlayVideo(0);
            step += 1;
            return;
        }

        if (step == 9 && IsPreviousStepFinished()){
            PlayVideo(1);
            PlaySpeech(6);
            step += 1;
        }

        if (step == 10 && IsPreviousStepFinished()){
            PlaySpeech(7);
            PlayVideo(2);
            step += 1;
        }

        if (step==11 && IsPreviousStepFinished()){
            PlaySpeech(8);
            step += 1;
        }

        if (step == 12 && IsPreviousStepFinished()){
            SetElementPartSeenList(0, true);
            step = 29;
        }



        if (step == 13){
            PlaySpeech(9);
            PlayVideo(3);
            step += 1;
        }

        if (step == 14 && IsPreviousStepFinished()){
            SetElementPartSeenList(1, true);
            step = 29;
        }




        if (step == 15){
            PlaySpeech(10);
            PlayPhoto(1);               // Equal to photo 2 and 3
            step += 1;
        }

        if (step == 16 && IsPreviousStepFinished()){
            PhotoManager.current.ShowPhoto(false);
            PlaySpeech(11);
            PlayVideo(4);
            step += 1;
        }

        if (step == 17 && IsPreviousStepFinished()){
            PlaySpeech(12);
            PlayVideo(5);
            step += 1;
        }

        if (step == 18 && IsPreviousStepFinished()){
            SetElementPartSeenList(2, true);
            step = 29;
        }



        if (step == 19){
            PlaySpeech(13);
            PlayVideo(6);
            step += 1;
        }

        if (step == 20 && IsPreviousStepFinished()){
            PlaySpeech(14);
            step += 1;
        }

        if (step == 21 && IsPreviousStepFinished()){
            PlaySpeech(15);
            PlayVideo(7);
            step += 1;
        }

        if (step == 22 && IsPreviousStepFinished()){
            PlaySpeech(16);
            PlayVideo(8);
            step += 1;
        }

        if (step == 23 && IsPreviousStepFinished()){
            SetElementPartSeenList(3, true);
            step = 29;
        }

        if (step == 24){
            MoveJam.current.MoveJamToMiddle();
            step += 1;
        }

        if (step == 25 && IsPreviousStepFinished()){
            PlaySpeech(17);
            step += 1;
        }

        if (step == 26 && IsPreviousStepFinished()){
            ShowNextButton(true);
            ShowButtons(true);
            step = 6;
        }

        if (step == 27){
            ShowButtons(false);
            ShowNextButton(false);
            PlaySpeech(18);
            step += 1;
        }

        if (step == 28 && IsPreviousStepFinished()){
            step = 0;
            GlobalManager.current.IsCurrentChapterFinished = true;
        }

        if (step == 29){
            GlobalManager.current.HideScreen = true;
              
            if (HaveAllPartBeenSeen()){
                step = 24;
            }   

            else{
                ShowButtons(true);
                step = 6;
            } 
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
        SpeechSoundManager.current.PlaySupplyClip(speechIndex);
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
        newIndex.Add(2);
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
        return !IsAudioSourcePlaying() && !IsVideoPlayerPlaying() && !IsJamMoving();
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
        
        for (int i=0; i<4; i++){
            partSeenList.Add(false);
        }
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
        return !partSeenList.Contains(false);
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
        Show or Hide button at index given
        Entry: 
         int n, index of button 
         bool b, true = show and false = hide
    */
    private void ShowButton(int n, bool b){
        stepButtonList[n].gameObject.SetActive(b);
    }

    /*
        Show or Hide buttons
        Entry: 
         bool b, true = show and false = hide
    */
    private void ShowButtons(bool b){
        foreach(Button button in stepButtonList){
            button.gameObject.SetActive(b);
        }
    }

    /*
        Show or Hide nextButtons
        Entry: 
         bool b, true = show and false = hide
    */
    private void ShowNextButton(bool b){
        nextButton.gameObject.SetActive(b);
    }

    /*
        Make buttons interactabel or not
        Entry: 
         bool b, true = interactable and false = not interactable
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
        if (s == "Fiche" || s == "Carroussel" || s == "MOS" || s == "Circuits" || s == "NextButton"){
            SetStep(s);
        }
        else{
            Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
        }
    }

    private void SetStep( string s){
        
        ShowButtons(false);

        if (s == "Fiche"){
            step = 8;
        }

        if (s == "Carroussel"){
            step = 13;
        }

        if (s == "MOS"){
            step = 15;
        }

        if (s == "Circuits"){
            step = 19;
        }

        if (s == "NextButton"){
            step = 27;
        }
    }
}
