using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveJam : MonoBehaviour
{

    public static MoveJam current;
    private Vector3 targetPosition;
    private Camera mainCamera;
    private int step = 0;

    /*
        0: nothing
        1: START rotation
        2: PLAYING rotation
        3: FINISH rotation + START translation
        4: PLAYING translation 
        5: FINISH translation + START rotation
        6: PLAYING rotation
        7: FINISH rotation, return to 0
    */


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
        mainCamera = Camera.main;
    }


    void Update()
    {
        Vector3 onFloor = new Vector3(transform.position.x, 0, transform.position.z);
        transform.position  = onFloor;
        Move();
    }

////////////////////////////////////////////////////////////

    private void StartMoving(Vector3 positionToReach){

        targetPosition = positionToReach;
        step = 1;
    }

    private void Move(){

        if (step == 1){
            Animate(true);
            RotateTowardTarget();
            step += 1;
        }

        if (step == 2 && RotateJam.current.HasFinished()){
            step += 1;
        }

        if (step == 3){
            Translate();
            step += 1;
        }
        
        if (step == 4 && TranslateJam.current.HasFinished()){
            step += 1;
        }

        if (step == 5){
            RotateTowardCamera();
            step += 1;
        }

        if (step == 6 && RotateJam.current.HasFinished()){
            step = 0;
            Animate(false);
        }
    }


////////////////////////////////////////////////////////////

    private void RotateTowardTarget(){

        // Get the target Quaternion 
        Vector3 relativePos = targetPosition - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos);

        // Call RotateJam
        RotateJam.current.StartRotation(targetRotation); 
    }

    private void Translate(){
        TranslateJam.current.SetPoints(targetPosition);
    }

    private void RotateTowardCamera(){

        // Get MainCamera position
        Vector3 targetPos = mainCamera.transform.position;
        targetPos.y = 0;

        // Get the target Quaternion 
        Vector3 relativePos = targetPos - transform.position;
        Quaternion targetRotation = Quaternion.LookRotation(relativePos);

        // Call RotateJam
        RotateJam.current.StartRotation(targetRotation);        
    }

////////////////////////////////////////////////////////////

    private void Animate(bool b){
        AnimationController.current.IsWalking(b);
    }


////////////////////////////////////////////////////////////

    public bool HasFinished(){
        return step == 0;
    }

////////////////////////////////////////////////////////////

    public void MoveJamToStart(){

        if (!IsAtPosition(GetStartPosition())){
            StartMoving(GlobalManager.current.GetChapterStartPosition());
        }
    }

    public void MoveJamToMiddle(){

        if (!IsAtPosition(new Vector3 (0,0,transform.position.z))){
            StartMoving( new Vector3 (0,0,transform.position.z) );
        }
    }

    public void MoveJamToMirror(){

        if(!IsAtPosition(GetMirrorPosition())){
            StartMoving ( GetMirrorPosition() );
        }
    }

    private Vector3 GetMirrorPosition(){
        Vector3 startPosition = GlobalManager.current.GetChapterStartPosition();
        return new Vector3 (-startPosition.x, 0, startPosition.z );
    }

    private Vector3 GetStartPosition(){
        return GlobalManager.current.GetChapterStartPosition();
    }

    private bool IsAtPosition(Vector3 v){
        bool x = (transform.position.x - v.x >= -0.1 && transform.position.x - v.x <= 0.1);
        bool z = (transform.position.z - v.z >= -0.1 && transform.position.z - v.z <= 0.1);
        return x && z;
    }
}
