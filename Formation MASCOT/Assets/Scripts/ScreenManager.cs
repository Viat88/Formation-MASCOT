using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenManager : MonoBehaviour
{

    public static ScreenManager current;

    
    public GameObject screen;

    public float speed;
    public Transform shownPosition;
    public Transform hidenPosition;
    private Vector3 targetPosition;
    private Vector3 initialPosition;
    

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
        /*
        GlobalManager.current.OnVideoIndexChanged += ShowScreen;
        GlobalManager.current.OnPhotoIndexChanged += ShowScreen;
        GlobalManager.current.OnHideScreenChanged += SetExitParameter;
        */
    }

    void Update()
    {
      Move();  
    }

////////////////////////////////////////////////////////////

    public void Exit(bool b){
        
        if(b){
            initialPosition = transform.position;
            targetPosition = hidenPosition.position;
        }
    }

    public void Enter(List<int> l){
        initialPosition = transform.position;
        targetPosition = shownPosition.position;
    }

    private void Move(){

        if (initialPosition != targetPosition){
            transform.position = Vector3.Lerp(initialPosition, targetPosition, Time.deltaTime*speed);
        }
    }

////////////////////////////////////////////////////////////


}
