using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ErrorManager : MonoBehaviour
{
    public static ErrorManager current;

    /*
        0: MOVE Jam to start
        1: PLAY speech err01 + SHOW picture err01
        2: HIDE picture err01 + PLAY speech err02 + PLAY video err01;
        3: MOVE Jam to mirror
        4: PLAY speech err03 + SHOW picture err02
        5: HIDE picture err02 + HIDE screen + MOVE Jam to middle
    */
    private int step = 0;

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
        GlobalManager.current.currentChapter = 7;
    }

    void Update()
    {
        ManageStep();
    }

////////////////////////////////////////////////////////////

    private void ManageStep(){


        if (step == 0 && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 1 && IsPreviousStepFinished()){
            PlaySpeech(0);
            PlayPhoto(0);
            step += 1;
        }

        if (step == 2 && IsPreviousStepFinished()){
            PhotoManager.current.ShowPhoto(false);
            PlaySpeech(1);
            PlayVideo(0);
            step += 1;
        }

        if (step == 3 && IsPreviousStepFinished()){
            MoveJam.current.MoveJamToMirror();
            step += 1;
        }

        if (step == 4 && IsPreviousStepFinished()){
            PlaySpeech(2);
            PlayPhoto(1);
            step += 1;
        }

        if (step == 5 && IsPreviousStepFinished()){
            PhotoManager.current.ShowPhoto(false);
            HideTheScreen(true);
            MoveJam.current.MoveJamToMiddle();
            step += 1;
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
        SpeechSoundManager.current.PlayErrMontageClip(speechIndex);
    }

//////////////////////////// INDEX ////////////////////////////////

    private List<int> GetList(int n){

        List<int> newIndex = new List<int>();
        newIndex.Add(7);
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
}
