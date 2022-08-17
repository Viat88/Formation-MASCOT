using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IconController : MonoBehaviour
{
    public GameObject frame;

    public void OnMouseDown(){
        frame.SetActive(!frame.activeSelf);
    }
}
