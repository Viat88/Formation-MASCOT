using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SupStepButton : MonoBehaviour
{
    public void setStep(){
        SupplyManager.current.ListenButtons(name);
    }

}
