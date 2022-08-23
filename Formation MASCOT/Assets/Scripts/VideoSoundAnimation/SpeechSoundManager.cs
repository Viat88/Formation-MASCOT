using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static SpeechSoundManager current;           // Unique SpeechSoundManager


    [Header ("Introduction")]
    public List<AudioClip> introAudioClipList;

    [Header ("Presentation")]
    public List<AudioClip> presAudioClipList;

    [Header ("Supply")]
    public List<AudioClip> supAudioClipList;

    [Header ("Security")]
    public List<AudioClip> safAudioClipList;

    [Header ("Premontage")]
    public List<AudioClip> premAudioClipList;

    [Header ("Montage")]
    public List<AudioClip> monAudioClipList;

    [Header ("Fin Montage")]
    public List<AudioClip> finAudioClipList;

    [Header ("Erreur")]
    public List<AudioClip> errAudioClipList;
    [Header ("End")]
    public List<AudioClip> endAudioClipList;
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


    void Update(){
        
        if (!audioSource.isPlaying && !isGamePaused){
            RemoveClip();
        }
        
    }

////////////////////////////////////////////////////////////

    private void IsGamePaused(GameState state){

        if (state == GameState.Paused){
            PauseSound();
        }

        if (state == GameState.Running){
            PlaySound();
        }

        isGamePaused = (state == GameState.Paused);
    }

////////////////////////////////////////////////////////////


    /* Change the sound */
    private void ChangeClip(AudioClip newClip)
    {
        audioSource.clip = newClip;
        PlaySound();
    }

    private void PlaySound(){
        audioSource.Play();
    }


    /* Stop the sound */
    public void StopSound(){
        audioSource.Stop();
    }

    /* Pause the sound */
    public void PauseSound(){
        audioSource.Pause();
    }

    public void RemoveClip(){
        audioSource.clip = null;
    }

//////////////////////// Introduction ////////////////////////////////////

    public void PlayIntroClip(int n){

        if (n < introAudioClipList.Count){
            ChangeClip(introAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
    }

//////////////////////// Presentation ////////////////////////////////////

    public void PlayPresClip(int n){

        if (n < presAudioClipList.Count){
            ChangeClip(presAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
    }

//////////////////////// Supply ////////////////////////////////////

    public void PlaySupplyClip(int n){

        if (n < supAudioClipList.Count){
            ChangeClip(supAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
    }

//////////////////////// Security ////////////////////////////////////

    public void PlaySecurityClip(int n){

        if (n < safAudioClipList.Count){
            ChangeClip(safAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
    }

//////////////////////// Premontage ////////////////////////////////////

    public void PlayPremontageClip(int n){

        if (n < premAudioClipList.Count){
            ChangeClip(premAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
    }

//////////////////////// Montage ////////////////////////////////////

    public void PlayMontageClip(int n){

        if (n < monAudioClipList.Count){
            ChangeClip(monAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
    }

//////////////////////// Fin Montage ////////////////////////////////////

    public void PlayFinMontageClip(int n){

        if (n < finAudioClipList.Count){
            ChangeClip(finAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
    }

//////////////////////// Erreur ////////////////////////////////////

    public void PlayErrMontageClip(int n){

        if (n < errAudioClipList.Count){
            ChangeClip(errAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        } 
    }

//////////////////////// End ////////////////////////////////////

    public void PlayEndClip(int n){

        if (n < endAudioClipList.Count){
            ChangeClip(endAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        } 
    }

}
