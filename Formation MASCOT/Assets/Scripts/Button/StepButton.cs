using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepButton : MonoBehaviour
{
    /*
        Script used by buttons that allow user to see again a step of the chapter
        Each function is directly called in Unity by the adequate button
        Each function corresponds to a unique chapter, the name of the button helps the chapter to know which button has been pressed
    */

    public void Introduction(){
        IntroManager.current.CheckEntry(name);
    }
    public void Presentation(){
        PresManager.current.CheckEntry(name);
    }

    public void Supply(){
        SupplyManager.current.CheckEntry(name);
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

    public void FinMontage(){
        FinMontageManager.current.CheckEntry(name);
    }

    public void End(){
        EndManager.current.CheckEntry(name);
    }
}
