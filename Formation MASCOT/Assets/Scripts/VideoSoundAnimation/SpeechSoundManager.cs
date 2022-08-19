using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static SpeechSoundManager current;           // Unique SpeechSoundManager
    private string playerGender;

    // Introduction
    public AudioClip introSpeech;
    public AudioClip lastSentenceIntro;


    // Presentation
    [Header ("Presentation")]
    public AudioClip Pres01;
    public AudioClip Pres02;
    public AudioClip Pres03;
    public AudioClip Pres04;
    public AudioClip Pres05;
    public AudioClip Pres06Man;
    public AudioClip Pres06Woman;

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
        GlobalManager.current.OnGenderChanged += SetGender;
    }

    void Update(){
        if (!audioSource.isPlaying){
            audioSource.Pause();
        }
    }


////////////////////////////////////////////////////////////


    /* Play the sound entered */
    private void PlaySound(AudioClip clip)
    {
        audioSource.PlayOneShot(clip);
    }

    /* Set player's gender*/
    private void SetGender(string gender){
        playerGender = gender;
    }

//////////////////////// Introduction ////////////////////////////////////

    public void PlayIntroSpeech(){
        PlaySound(introSpeech);
    }

    public void PlayLastSentenceIntro(){
        PlaySound(lastSentenceIntro);
    }

//////////////////////// Presentation ////////////////////////////////////

    public void PlayPres01(){
        PlaySound(Pres01);
    }

    public void PlayPres02(){
        PlaySound(Pres02);
    }

    public void PlayPres03(){
        PlaySound(Pres03);
    }

    public void PlayPres04(){
        PlaySound(Pres04);
    }

    public void PlayPres05(){
        PlaySound(Pres05);
    }

    public void PlayPres06(){
        if (playerGender == "man"){
            PlaySound(Pres06Man);
        }
        else{
            PlaySound(Pres06Woman);
        }
        
    }

//////////////////////// Supply ////////////////////////////////////

    public void PlaySupplyClip(int n){

        if (n < supAudioClipList.Count){
            PlaySound(supAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
        
    }

//////////////////////// Security ////////////////////////////////////

    public void PlaySecurityClip(int n){

        if (n < safAudioClipList.Count){
            PlaySound(safAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
        
    }

//////////////////////// Premontage ////////////////////////////////////

    public void PlayPremontageClip(int n){

        if (n < premAudioClipList.Count){
            PlaySound(premAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
        
    }

//////////////////////// Montage ////////////////////////////////////

    public void PlayMontageClip(int n){

        if (n < monAudioClipList.Count){
            PlaySound(monAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
        
    }

//////////////////////// Fin Montage ////////////////////////////////////

    public void PlayFinMontageClip(int n){

        if (n < finAudioClipList.Count){
            PlaySound(finAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
        
    }

//////////////////////// Erreur ////////////////////////////////////

    public void PlayErrMontageClip(int n){

        if (n < errAudioClipList.Count){
            PlaySound(errAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
        
    }

}
