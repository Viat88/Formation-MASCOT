using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextButton : MonoBehaviour
{

    void Start()
    {}

    void Update()
    {}

    public void FinishChapter(){
        PresManager.current.ShowFinalButtons(false);
        GlobalManager.current.IsCurrentChapterFinished = true;
    }
}
