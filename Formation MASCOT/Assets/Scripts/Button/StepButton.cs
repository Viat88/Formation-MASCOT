using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StepButton : MonoBehaviour
{

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
