using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepButton : MonoBehaviour
{

    public void Presentation(){
        PresManager.current.SetStep(name);
    }

    public void Supply(){
        PresManager.current.SetStep(name);
    }
    
    public void Security(){
        SecurityManager.current.CheckEntry(name);
    }

    public void Premontage(){
        PremontageManager.current.CheckEntry(name);
    }

    public void Montage(){
        MontageManager.current.CheckEntry(name);
    }
}
