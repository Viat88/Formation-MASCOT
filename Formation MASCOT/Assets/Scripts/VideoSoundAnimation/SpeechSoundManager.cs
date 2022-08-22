using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeechSoundManager : MonoBehaviour
{
    public AudioSource audioSource;
    public static SpeechSoundManager current;           // Unique SpeechSoundManager
    private string playerGender;


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

    /* Stop the sound */
    public void StopSound(){
        audioSource.Stop();
    }

//////////////////////// Introduction ////////////////////////////////////

    public void PlayIntroClip(int n){

        if (n < introAudioClipList.Count){
            PlaySound(introAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
        }
        
    }

//////////////////////// Presentation ////////////////////////////////////

    public void PlayPresClip(int n){

        if (n < presAudioClipList.Count){
            PlaySound(presAudioClipList[n]);
        }

        else{
            Debug.Log("Plus de vocaux supplémentaires");
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
