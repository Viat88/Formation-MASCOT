using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenderController : MonoBehaviour
{

    public string associatedGender;

    public void SetGender(){
        GlobalManager.current.PlayerGender = associatedGender;
        IntroManager.current.StepPlusOne();
    } 
}
