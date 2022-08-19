using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhotoManager : MonoBehaviour
{

    public static PhotoManager current;
    private GameObject currentPhoto;

    public List<GameObject> presPhotoList;
    public List<GameObject> supPhotoList;
    public List<GameObject> safPhotoList;
    public List<GameObject> premPhotoList;
    public List<GameObject> monPhotoList;

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
        GlobalManager.current.OnPhotoIndexChanged += PlayIndexedPhoto;
    }

////////////////////////////////////////////////////////////

    public void ShowPhoto(bool b){

        if(currentPhoto){
            currentPhoto.SetActive(b);
        }
        
        else{
            Debug.Log("Pas de photo actuellement affichée");
        }
    }


    private void PlayIndexedPhoto(List<int> indexList){

        int chapter = indexList[0];
        int photoIndex = indexList[1];
        
        if (chapter == 1){
            PlayPresPhoto(photoIndex);
        }
        
        if (chapter == 2){
            PlaySupPhoto(photoIndex);
        }

        if (chapter == 3){
            PlaySafPhoto(photoIndex);
        }

        if (chapter == 4){
            PlayPremPhoto(photoIndex);
        }

        if (chapter == 5){
            PlayMonPhoto(photoIndex);
        }

        else{
            Debug.Log("l'indice du chapitre ne correspond à aucun chapitre");
        }
    }

////////////////////////////////////////////////////////////

    private void PlayPresPhoto(int index){

        if (index < presPhotoList.Count && index >= 0){
            currentPhoto = presPhotoList[index];
            ShowPhoto(true);
        } 

        else{
            Debug.Log("l'indice photo dépasse le nombre de photo disponible");
        }
    }

    private void PlaySupPhoto(int index){

        if (index < supPhotoList.Count && index >= 0){
            currentPhoto = supPhotoList[index];
            ShowPhoto(true);
        } 

        else{
            Debug.Log("l'indice photo dépasse le nombre de photo disponible");
        }
    }

    private void PlaySafPhoto(int index){

        if (index < safPhotoList.Count && index >= 0){
            currentPhoto = safPhotoList[index];
            ShowPhoto(true);
        } 

        else{
            Debug.Log("l'indice photo dépasse le nombre de photo disponible");
        }
          
    }

    private void PlayPremPhoto(int index){

        if (index < premPhotoList.Count && index >= 0){
            currentPhoto = premPhotoList[index];
            ShowPhoto(true);
        } 

        else{
            Debug.Log("l'indice photo dépasse le nombre de photo disponible");
        }
          
    }

    private void PlayMonPhoto(int index){

        if (index < monPhotoList.Count && index >= 0){
            currentPhoto = monPhotoList[index];
            ShowPhoto(true);
        } 

        else{
            Debug.Log("l'indice photo dépasse le nombre de photo disponible");
        }
          
    }
}
