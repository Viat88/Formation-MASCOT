using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationController : MonoBehaviour
{
    public static AnimationController current;                                 // Unique AnimationController
    private Animator animator;


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
        animator = GetComponent<Animator>();
        animator.Play("Hello");
    }


    void Update()
    {
        IsTalking();
    }


    // Play talking animation if Jam is talking
    private void IsTalking(){
        animator.SetBool("isTalking", SpeechSoundManager.current.audioSource.isPlaying);
    }

    
    // Play walking animation 
    public void IsWalking(bool b){
        animator.SetBool("Walk", b);
    }

    // Play left turning animation 
    public void IsTurningLeft(bool b){
        animator.SetBool("TurnLeft", b);
    }

    // Play right turning animation 
    public void IsTurningRight(bool b){
        animator.SetBool("TurnRight", b);
    }

}
