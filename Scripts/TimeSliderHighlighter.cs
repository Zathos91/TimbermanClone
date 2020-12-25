using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeSliderHighlighter : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void OnNewScore()
    {
        anim.ResetTrigger("Score");
        anim.SetTrigger("Score");
    }
}
