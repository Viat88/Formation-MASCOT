using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TranslateJam : MonoBehaviour
{

    public static TranslateJam current;
    private Vector3 initialPoint;
    private Vector3 targetPoint;
    private float time = 1;
    public float speed;

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
        targetPoint = transform.position;
        initialPoint = targetPoint;
    }


    void FixedUpdate()
    {
        if (!HasFinished()){
            Translate();
        }
    }


////////////////////////////////////////////////////////////

    /* Set target and initial point */
    public void SetPoints(Vector3 pointToReach){

        time = 0;

        targetPoint = pointToReach;
        initialPoint = transform.position;
    }


    /* Translate the position of Jam according to time */
    private void Translate(){
        time += Time.fixedDeltaTime;
        transform.position = Vector3.Lerp(initialPoint, targetPoint, time*speed);
    }


    /* Return true if Jam is arrived to the target point */
    public bool HasFinished(){
        return time*speed>=1;
    }


}
