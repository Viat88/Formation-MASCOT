using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ValidateController : MonoBehaviour
{
    public List<GameObject> iconList;

    public void OnMouseDown(){

        foreach (GameObject g in iconList){

            g.transform.Find("Frame").gameObject.SetActive(false);
            g.transform.Find("Response").gameObject.SetActive(true);
            SecurityManager.current.CheckEntry("ValidateButton");
        }
    }
}
