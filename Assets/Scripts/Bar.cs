using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class Bar : MonoBehaviour
{

    public GameObject bar;
    public int time;
    public bool isTimerUp;
    // Start is called before the first frame update
    void Start()
    {
        isTimerUp = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AnimateBar()
    {
        LeanTween.scaleX(bar, -1, time).setOnComplete(SetTimerIsUp);
    }

    public void SetTimerIsUp()
    {
        isTimerUp = true;
    }
}
