using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class TransitionManager : MonoBehaviour
{

    public static TransitionManager current;            // Unique TransitionManager
    public GameObject Jam;
    private int step = 0;
    /*
        0: nothing
        1: Start of transition with Rotation right
        2: Doing Rotation right
        3: Rotation right done + start translation
        4: Doing translation
        5: translation done + start rotation
        6: Doing rotation
        7: Rotation finished + end transition
    */

    public GameObject transitionCanva;
    public List<GameObject> stepPictureList;
    public float transitionDelay;
    private float time;


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
        GlobalManager.current.OnIsTransitionChanged += Transition;
    }


    void FixedUpdate()
    {
        PlayTransition();
    }

////////////////////////////////////////////////////////////


    private void Transition(bool b){
        
        if(b){
            
            step = 1;
            time = 0;
        }

        else{
            step = 0;   
        }
        
    }

    private void PlayTransition(){

        time += Time.deltaTime;

        if (step == 1){
            MoveJam.current.MoveJamToStart();
            PlayAnimation();
            step += 1;
        }

        if (step == 2 && MoveJam.current.HasFinished()){

            if (GetChapterDoneIndex() >=0){
                if (time>transitionDelay){
                    ShowTransitionCanva(false);
                    DesactivateAnimations();
                    StopTransition();
                    step = 0;
                }
                
            }

            else{
                StopTransition();
                step = 0;
            }
            
        }        
    }

////////////////////////////////////////////////////////////

    private void StopTransition(){
        GlobalManager.current.IsTransition = false;
    }

////////////////////////////////////////////////////////////

    private void PlayAnimation(){

        if (GetChapterDoneIndex() >= 0){
            ShowTransitionCanva( true );
            GameObject currentPicture = GetStepPicture(GetChapterDoneIndex());
            ShowVoile(true, currentPicture);
            ActivateAnimation(currentPicture);
        }

    }

////////////////////////////////////////////////////////////
    private void ShowTransitionCanva(bool b){
        transitionCanva.SetActive(b);
    }

    private void ShowVoile(bool b, GameObject g){
        g.transform.Find("Voile").gameObject.SetActive(b);
    }

////////////////////////////////////////////////////////////

    private void ActivateAnimation(GameObject g){
        g.GetComponent<Animator>().enabled = true;
    }

    private void DesactivateAnimations(){

        foreach(GameObject g in stepPictureList){
            g.GetComponent<Animator>().enabled = false;
        }
    }

////////////////////////////////////////////////////////////

    private int GetChapterDoneIndex(){
        return GlobalManager.current.currentChapter - 2;
    }

    private GameObject GetStepPicture(int n){
        return stepPictureList[n];
    }

}
