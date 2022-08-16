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
        }

        else{
            step = 0;   
        }
        
    }

    private void PlayTransition(){

        if (step == 1){
            MoveJam.current.MoveJamToStart();
            step += 1;
        }

        if (step == 2 && MoveJam.current.HasFinished()){
            StopTransition();
            step = 0;
        }        
    }

////////////////////////////////////////////////////////////

    private void StopTransition(){
        GlobalManager.current.IsTransition = false;
    }

}
