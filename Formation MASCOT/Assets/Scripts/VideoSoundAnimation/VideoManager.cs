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
            videoPlayer.Stop();
        }        
    }

    private void PlayVideo(VideoClip video){
        timeSinceStart = 0;
        videoPlayer.clip = video;
        videoPlayer.Play();
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

        else{
            Debug.Log("l'indice du chapitre ne correspond à aucun chapitre");
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

////////////////////////////////////////////////////////////

    public bool IsVideoPlayerPlaying(){
        return videoPlayer.isPlaying;
    }
}
