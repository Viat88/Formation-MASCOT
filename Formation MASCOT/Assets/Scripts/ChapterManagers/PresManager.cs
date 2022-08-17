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
    private List<Vector3> indexesSaveList;          // It saves the index of video, photo and speech at point where player can go back

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
        GlobalManager.current.currentChapter = 1;
        indexesSaveList = new List<Vector3>();
    }

    void Update()
    {
        ManageStep();
    }


////////////////////////////////////////////////////////////

    private void ManageStep(){

        if (step == -1){
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 0 && MoveJam.current.HasFinished()){                                         // 1st available rewatch

            if (isPhotoRewatch){
                step = 3;
            }
            else{
                SpeechSoundManager.current.PlayPres01();
                step += 1;
            }
        }

        if (step == 1 && !IsAudioSourcePlaying()){
            SpeechSoundManager.current.PlayPres02();
            PlayVideo(0);                                    // Play video Pres01
            step += 1;
        }
        
        if (step == 2 && !IsAudioSourcePlaying() && !IsVideoPlayerPlaying()){
            
            // Player only wanted to see again video
            if(isVideoRewatch){
                step = 6;
            }

            // It's the first watch for player
            else{
                SpeechSoundManager.current.PlayPres03();
                step += 1;
            }
        }
        
        if (step == 3 && !IsAudioSourcePlaying()){              // 2nd available rewatch
            PlayPhoto(0);
            SpeechSoundManager.current.PlayPres04();
            step += 1;
        }

        if (step == 4 && !IsAudioSourcePlaying()){
            MoveJam.current.MoveJamToMirror();
            step += 1;
        }

        if (step == 5 && MoveJam.current.HasFinished()){
            SpeechSoundManager.current.PlayPres05();
            step += 1;
        }

        if (step == 6 && !IsAudioSourcePlaying()){
            PhotoManager.current.ShowPhoto(false);
            HideTheScreen(true);
            MoveJam.current.MoveJamToMiddle();
            step += 1;
        }

        if (step == 7 && MoveJam.current.HasFinished()){
            SpeechSoundManager.current.PlayPres06(); 
            step += 1;
        }

        if (step == 8 && !IsAudioSourcePlaying()){
            ShowFinalButtons(true);
            step += 1;
        }
    }

////////////////////////////////////////////////////////////

    private bool IsAudioSourcePlaying(){
        return SpeechSoundManager.current.audioSource.isPlaying;
    }

    private bool IsVideoPlayerPlaying(){
        return VideoManager.current.IsVideoPlayerPlaying();
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


    private List<int> GetList(int n){

        List<int> newIndex = new List<int>();
        newIndex.Add(1);
        newIndex.Add(n);

        return newIndex;
    }

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

    public void ShowFinalButtons(bool b){
        buttons.SetActive(b);
    }

////////////////////////////////////////////////////////////

    public void SetStep(string s){

        if (s == "VideoButton" || s == "PhotoButton"){
            RewatchPhotoOrVideo(s);
        }

        else{
            Debug.Log("Erreur: le string rentré en argument ne correspond pas à une des options");
        }
    }

    private void RewatchPhotoOrVideo(string s){

        ShowFinalButtons(false);
        step = -1;

        if (s == "VideoButton"){
            isVideoRewatch = true;
        }

        else{
            isPhotoRewatch = true;
        }
    }
    
}
