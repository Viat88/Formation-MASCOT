using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GlobalManager : MonoBehaviour
{

    public static GlobalManager current;           // Unique GlobalManager
    [HideInInspector]
    public int currentChapter = 1;
    public List<GameObject> chapterList;
    public Transform chapterStartPosition;

///////////////////////// Listeners ///////////////////////////////////  
    
    /*
        Listener for a new transition
    */
    public event Action<Boolean> OnIsTransitionChanged;
    public void IsTransitionChanged(Boolean b){
        OnIsTransitionChanged?.Invoke(b);
    }

    [HideInInspector]
    public Boolean isTransition = false;
    public Boolean IsTransition{
        get => isTransition;
        set
        {
            isTransition = value;
            IsTransitionChanged(isTransition); //Fire the event
        }
    }


    /*
        Listener about if current chapter finished
    */
    public event Action<Boolean> OnCurrentChapterFinished;
    public void CurrentChapterFinished(Boolean b){
        OnCurrentChapterFinished?.Invoke(b);
    }

    [HideInInspector]
    public Boolean currentChapterFinished = false;
    public Boolean IsCurrentChapterFinished{
        get => currentChapterFinished;
        set
        {
            currentChapterFinished = value;
            CurrentChapterFinished(currentChapterFinished); //Fire the event
        }
    }


    /*
        Listener about the player's gender
    */
    public event Action<String> OnGenderChanged;
    public void GenderChanged(string s){
        OnGenderChanged?.Invoke(s);
    }

    [HideInInspector]
    public String playerGender = "man";
    public String PlayerGender{
        get => playerGender;
        set
        {
            playerGender = value;
            GenderChanged(playerGender); //Fire the event
        }
    }


    /*
        Listener about the video index
    */
    public event Action<List<int>> OnVideoIndexChanged;
    public void VideoIndexChanged(List<int> l){
        OnVideoIndexChanged?.Invoke(l);
    }

    [HideInInspector]
    public List<int> videoIndex = new List<int>();
    public List<int> VideoIndex{
        get => videoIndex;
        set
        {
            videoIndex = value;
            VideoIndexChanged(videoIndex); //Fire the event
        }
    }


    /*
        Listener about the photo index
    */
    public event Action<List<int>> OnPhotoIndexChanged;
    public void PhotoIndexChanged(List<int> l){
        OnPhotoIndexChanged?.Invoke(l);
    }

    [HideInInspector]
    public List<int> photoIndex = new List<int>();
    public List<int> PhotoIndex{
        get => photoIndex;
        set
        {
            photoIndex = value;
            PhotoIndexChanged(photoIndex); //Fire the event
        }
    }

    /*
        Listener about if the screen has to be hide at the end
    */
    public event Action<Boolean> OnHideScreenChanged;
    public void HideScreenChanged(Boolean b){
        OnHideScreenChanged?.Invoke(b);
    }

    [HideInInspector]
    public Boolean hideScreen = false;
    public Boolean HideScreen{
        get => hideScreen;
        set
        {
            hideScreen = value;
            HideScreenChanged(hideScreen); //Fire the event
        }
    }


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
        this.OnCurrentChapterFinished += ChapterFinished;
        this.OnIsTransitionChanged += TransitionFinished;
    }


    void Update()
    {}

////////////////////////////////////////////////////////////

    private void ChapterFinished(bool b){
        if (b){
            chapterList[currentChapter].SetActive(false);                    
            IsTransition = true;  
        }
    }

    private void TransitionFinished(bool b){
        if (!b){
            ChangeCurrentChapter();
        }
    }

    private void ChangeCurrentChapter(){

        if (currentChapter + 1 <chapterList.Count ){
            currentChapter += 1;
            chapterList[currentChapter].SetActive(true);
        }
        else{
            Debug.Log("Pas de chapitre supplémentaire");
        }
        
    }

////////////////////////////////////////////////////////////

    public Vector3 GetChapterStartPosition(){
        return chapterStartPosition.position;
    }
}
