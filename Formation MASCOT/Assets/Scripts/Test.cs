using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Update()
    {
        if (Input.GetKeyDown("s")){
            ScreenManager.current.Exit(true);
        }

        if (Input.GetKeyDown("e")){
            ScreenManager.current.Enter(new List<int>());
        }
       
    }
}
