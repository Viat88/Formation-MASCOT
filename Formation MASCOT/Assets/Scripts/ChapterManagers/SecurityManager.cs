using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityManager : MonoBehaviour
{

    public static SecurityManager current;

    /*
        0: Move Jam
        1: PLAY speech saf01;
        2: SHOW picture saf01 with all icons;
        4: Button "VALIDER" has been pressed, PLAY animation glasses and PLAY speech saf02 
        5: PLAY animation gloves and PLAY speech saf03
        6: PLAY animation shoes and PLAY speech saf04
        7: PLAY animation gown and PLAY speech saf05
        8: PLAY animation headsets and PLAY speech saf06
        9: HIDE responses and picture saf01, PLAY video saf01 and PLAY speech saf07
        10: (IF securityRewatch, step =13) MOVE Jam to Mirror Position
        11: SHOW picture saf02 and PLAY speech saf08
        12: MOVE Jam to middle and HIDE picture saf03
        13: SHOW buttons (REVOIR SECURITE, REVOIR CONFORT, SUIVANT)
        15: IF SUIVANT pressed: HIDE buttons and PLAY speech saf09
        16: TELL GlobalManager next Chapter



        Icon picture must show frame when pressed 
        Validate button must set step to 4 and show responses

        REVOIR SECURITE make step=0 and securityRewatch = true;
        REVOIR CONFORT make step=10
    */

    private int step = 0;

    public List<GameObject> iconList;
    private List<Animator> iconAnimatorList;
    private List<GameObject> responses;
    private bool securityRewatch = false;
    public GameObject finalButtons;
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
        GlobalManager.current.currentChapter = 3;
        SetIconAnimatorList();
        SetResponsesList();
    }

    void Update()
    {
        if (!isGamePaused){
            ManageStep();
        }
    }

////////////////////////////////////////////////////////////

    private void IsGamePaused(GameState state){
        isGamePaused = (state == GameState.Paused);
    }

////////////////////////////////////////////////////////////

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
            PlayPhoto(0);
            step += 1;
        }

        if (step == 4){
            ActivateIconAnimation(0, true, true);
            PlaySpeech(1);
            step += 1;
        }

        if (step == 5  && IsPreviousStepFinished()){
            ActivateIconAnimation(1, true, true);
            PlaySpeech(2);
            step += 1;
        }

        if (step == 6  && IsPreviousStepFinished()){
            ActivateIconAnimation(2, true, true);
            PlaySpeech(3);
            step += 1;
        }

        if (step == 7  && IsPreviousStepFinished()){
            ActivateIconAnimation(3, true, true);
            PlaySpeech(4);
            step += 1;
        }

        if (step == 8  && IsPreviousStepFinished()){
            ActivateIconAnimation(4, true, true);
            ActivateIconAnimation(5, false, true);
            PlaySpeech(5);
            step += 1;
        }

        if (step == 9  && IsPreviousStepFinished()){
            DesactivateResponses();
            DesactivateAllIconAnimation();
            PhotoManager.current.ShowPhoto(false);
            PlayVideo(0);
            PlaySpeech(6);
            step += 1;
        }

        if (step == 10  && IsPreviousStepFinished()){
            
            HideTheScreen(true);

            if (securityRewatch){
                step = 13;
                securityRewatch = false;
                MoveJam.current.MoveJamToMiddle();
            }

            else{
                MoveJam.current.MoveJamToMirror();
                step += 1;
            }
        }

        if (step == 11  && IsPreviousStepFinished()){
            PlayPhoto(1);
            PlaySpeech(7);
            step += 1;
        }

        if (step == 12  && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToMiddle();
            PhotoManager.current.ShowPhoto(false);
            step += 1;
        }

        if (step == 13  && IsPreviousStepFinished()){
            ShowFinalButtons(true);
            HideTheScreen(true);
            step += 1;
        }

        if (step == 15  && IsPreviousStepFinished()){
            ShowFinalButtons(false);
            PlaySpeech(8);
            step += 1;
        }

        if (step == 16 && IsPreviousStepFinished()){
            step = 0;
            GlobalManager.current.IsCurrentChapterFinished = true;
        }
    }

////////////////////////////////////////////////////////////

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
        SpeechSoundManager.current.PlaySecurityClip(speechIndex);
    }

////////////////////////////////////////////////////////////

    private List<int> GetList(int n){

        List<int> newIndex = new List<int>();
        newIndex.Add(3);
        newIndex.Add(n);

        return newIndex;
    }

////////////////////////////////////////////////////////////

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

////////////////////////////////////////////////////////////

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

////////////////////////////////////////////////////////////
        
    private void SetResponsesList(){

        responses = new List<GameObject>();

        foreach(GameObject g in iconList){
            responses.Add(g.transform.Find("Response").gameObject);
        }
    }    

    private void SetIconAnimatorList(){

        iconAnimatorList = new List<Animator>();

        foreach(GameObject g in iconList){
            iconAnimatorList.Add(g.GetComponent<Animator>());
        }
    }


    private void ActivateIconAnimation(int n, bool b1, bool b2){

        if (b1){
            DesactivateAllIconAnimation();
        }
        iconAnimatorList[n].enabled = b2;
    }

    private void DesactivateAllIconAnimation(){
        foreach(Animator anim in iconAnimatorList){
            anim.enabled = false;
        }
    }

    private void DesactivateResponses(){

        foreach(GameObject g in responses){
            g.SetActive(false);
        }
    }

////////////////////////////////////////////////////////////

    public void ShowFinalButtons(bool b){
        finalButtons.SetActive(b);
    }

////////////////////////////////////////////////////////////

    public void CheckEntry(string s){

        if (s == "SecurityButton" || s == "ConfortButton" || s == "ValidateButton" || s == "NextButton"){
            SetSteps(s);
        }

        else{
            Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
        }
    }

    private void SetSteps(string s){

        ShowFinalButtons(false);

        if (s == "SecurityButton"){
            securityRewatch = true;
            step = 0;
        }

        if (s == "ConfortButton"){
            step = 10;
        }

        if (s == "ValidateButton"){
            step = 4;
        }

        if (s == "NextButton"){
            step = 15;
        }

        
    }
}
