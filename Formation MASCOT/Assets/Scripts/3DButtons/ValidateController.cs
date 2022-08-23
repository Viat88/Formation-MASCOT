using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateController : MonoBehaviour
{
    /*
        Controller of 3D button "Validate" during Security chapter
    */

    public List<GameObject> iconList;                                           // List of icons available on screen


    /*
        Called when user click on mouse over the 3D button
        Remove all frame of icons (=deselection), show responses and tell SecurityManager it has been clicked
    */
    public void OnMouseDown(){                                  

        foreach (GameObject g in iconList){

            g.transform.Find("Frame").gameObject.SetActive(false);
            g.transform.Find("Response").gameObject.SetActive(true);
            SecurityManager.current.CheckEntry("ValidateButton");
        }
    }
}
