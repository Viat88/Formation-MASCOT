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

    private float time = 0;
    

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
        
        GlobalManager.current.OnVideoIndexChanged += Enter;
        GlobalManager.current.OnPhotoIndexChanged += Enter;
        GlobalManager.current.OnHideScreenChanged += Exit;
        
    }

    void Update()
    {
      Move();  
    }

////////////////////////////////////////////////////////////

    public void Exit(bool b){
        
        if(b){
            InitialisePosition(hidenPosition.position);
        }
    }

    public void Enter(List<int> l){
        InitialisePosition(shownPosition.position);
    }

    private void InitialisePosition(Vector3 targetPos){
        initialPosition = screen.transform.position;
        targetPosition = targetPos;
        time = 0;
    }

    private void Move(){

        if (initialPosition != targetPosition){
            time += Time.deltaTime;
            screen.transform.position = Vector3.Lerp(initialPosition, targetPosition, time*speed);
        }
    }

////////////////////////////////////////////////////////////


}
