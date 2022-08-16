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
            if (videoIndex < presVideoClipList.Count && videoIndex >= 0){
                PlayVideo(presVideoClipList[videoIndex]);
            } 

            else{
                Debug.Log("l'indice vidéo dépasse le nombre de clip vidéo disponible");
        }
        }

        else{
            Debug.Log("l'indice du chapitre ne correspond à aucun chapitre");
        }
    }

//////////////////////// Presentation ////////////////////////////////////

    public bool IsVideoPlayerPlaying(){
        return videoPlayer.isPlaying;
    }
}
