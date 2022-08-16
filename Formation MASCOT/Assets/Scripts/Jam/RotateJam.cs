using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateJam : MonoBehaviour
{
    public static RotateJam current;
    private float time = 1;
    public float speed;
    private Quaternion targetRotation;
    private Quaternion initialRotation;
    private Camera mainCamera;

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

    void Start(){
        mainCamera = Camera.main;
    }

    void FixedUpdate()
    {
        if (!HasFinished() ){
            Rotate(); 
        }       
    }

////////////////////////////////////////////////////////////

    public void StartRotation(Quaternion rotationToReach){

        if (rotationToReach.x==0 && rotationToReach.y==0 && rotationToReach.z==0 && rotationToReach.w==0){
            
            Vector3 targetPos = mainCamera.transform.position;
            targetPos.y = 0;

            Vector3 relativePos = targetPos - transform.position;
            targetRotation = Quaternion.LookRotation(relativePos);
            
        }

        else{
            targetRotation = rotationToReach;
        }
        
        time = 0;
        initialRotation = transform.localRotation;
    }

////////////////////////////////////////////////////////////
    /* Look at targetRotation */
    private void Rotate(){
        time += Time.fixedDeltaTime;
        transform.rotation = Quaternion.Slerp(initialRotation, targetRotation, time * speed);
    }

////////////////////////////////////////////////////////////

    public bool HasFinished(){
        return time*speed>=1;
    }
}
