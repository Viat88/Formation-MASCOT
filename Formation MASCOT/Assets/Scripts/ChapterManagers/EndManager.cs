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
    {}

    // Update is called once per frame
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

    private bool IsAudioSourcePlaying(){
        return SpeechSoundManager.current.audioSource.isPlaying;
    }

    private bool IsJamMoving(){
        return !MoveJam.current.HasFinished();
    }

    private bool IsPreviousStepFinished(){
        return !IsAudioSourcePlaying() && !IsJamMoving();
    }

///////////////////////// BUTTONS ///////////////////////////////////

    private void ShowButtons(bool b){
        canva.SetActive(b);
    }

    private void EnableButtons(bool b){

        foreach(Button button in buttonsList){
            button.interactable = b; 
        }
    }

//////////////////////////// ENTRY FROM BUTTONS ////////////////////////////////

    public void CheckEntry(string s){
        if (s == "Appro" || s == "Sécurité" || s == "Prémontage" || s == "Montage" || s == "FinMontage" || s == "PièceDéfectueuse"){
            SetStep(s);
        }
        else{
            Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
        }
    }

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
