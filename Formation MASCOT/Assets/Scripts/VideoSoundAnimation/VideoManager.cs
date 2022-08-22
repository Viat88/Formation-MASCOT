using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoManager : MonoBehaviour
{
    public static VideoManager current;
    public GameObject videoScreen;
    private VideoPlayer videoPlayer;

    private float timeSinceStart = 0;
    public float timeToWait; 

    public List<VideoClip> presVideoClipList;
    public List<VideoClip> supVideoClipList;
    public List<VideoClip> safVideoClipList;
    public List<VideoClip> premVideoClipList;
    public List<VideoClip> monVideoClipList;
    public List<VideoClip> finVideoClipList;
    public List<VideoClip> errVideoClipList;


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
        videoPlayer = videoScreen.GetComponent<VideoPlayer>();
        GlobalManager.current.OnVideoIndexChanged += PlayIndexedVideo;
    }

    void Update()
    {
        RemoveVideo();
    }

////////////////////////////////////////////////////////////

    private void RemoveVideo(){

        timeSinceStart += Time.deltaTime;

        if (timeSinceStart > timeToWait && !IsVideoPlayerPlaying()){
            StopVideo();
        }        
    }

    private void PlayVideo(VideoClip video){
        timeSinceStart = 0;
        videoPlayer.clip = video;
        videoPlayer.Play();
    }

    public void StopVideo(){
        videoPlayer.Stop();
    }

    private void PlayIndexedVideo(List<int> indexList){

        int chapter = indexList[0];
        int videoIndex = indexList[1];
        
        if (chapter == 1){
            PlayPresVideo(videoIndex);
        }

        if (chapter == 2){
            PlaySupVideo(videoIndex);
        }

        if (chapter == 3){
            PlaySafVideo(videoIndex);
        }

        if (chapter == 4){
            PlayPremVideo(videoIndex);
        }

        if (chapter == 5){
            PlayMonVideo(videoIndex);
        }

        if (chapter == 6){
            PlayFinVideo(videoIndex);
        }

        if (chapter == 7){
            PlayErrVideo(videoIndex);
        }

    }
////////////////////////////////////////////////////////////

    private void PlayPresVideo(int index){
        if (index < presVideoClipList.Count && index >= 0){
            PlayVideo(presVideoClipList[index]);
        } 

        else{
            Debug.Log("l'indice vidéo dépasse le nombre de clip vidéo disponible");
        }
    }

    private void PlaySupVideo(int index){
        if (index < supVideoClipList.Count && index >= 0){
            PlayVideo(supVideoClipList[index]);
        } 

        else{
            Debug.Log("l'indice vidéo dépasse le nombre de clip vidéo disponible");
        }
    }

    private void PlaySafVideo(int index){
        if (index < safVideoClipList.Count && index >= 0){
            PlayVideo(safVideoClipList[index]);
        } 

        else{
            Debug.Log("l'indice vidéo dépasse le nombre de clip vidéo disponible");
        }
    }

    private void PlayPremVideo(int index){
        if (index < premVideoClipList.Count && index >= 0){
            PlayVideo(premVideoClipList[index]);
        } 

        else{
            Debug.Log("l'indice vidéo dépasse le nombre de clip vidéo disponible");
        }
    }

    private void PlayMonVideo(int index){
        if (index < monVideoClipList.Count && index >= 0){
            PlayVideo(monVideoClipList[index]);
        } 

        else{
            Debug.Log("l'indice vidéo dépasse le nombre de clip vidéo disponible");
        }
    }

    private void PlayFinVideo(int index){
        if (index < finVideoClipList.Count && index >= 0){
            PlayVideo(finVideoClipList[index]);
        } 

        else{
            Debug.Log("l'indice vidéo dépasse le nombre de clip vidéo disponible");
        }
    }

    private void PlayErrVideo(int index){
        if (index < errVideoClipList.Count && index >= 0){
            PlayVideo(errVideoClipList[index]);
        } 

        else{
            Debug.Log("l'indice vidéo dépasse le nombre de clip vidéo disponible");
        }
    }

////////////////////////////////////////////////////////////

    public bool IsVideoPlayerPlaying(){
        return videoPlayer.isPlaying;
    }
}
