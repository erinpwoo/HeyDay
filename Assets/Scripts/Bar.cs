using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;

public class Bar : MonoBehaviour
{

    public GameObject bar;
    public int time;
    public bool isTimerUp;
    public GameObject timer;
    public Building building;
    // Start is called before the first frame update
    void Start()
    {
        isTimerUp = false;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AnimateBar(GameObject timer, Building building)
    {
        this.timer = timer;
        this.building = building;
        LeanTween.scaleX(bar, 0, time).setOnComplete(SetTimerIsUp);
    }

    public void SetTimerIsUp()
    {
        isTimerUp = true;
        LeanTween.cancel(bar);
        building.DecrementPoints();
        Destroy(timer);
    }

    public void CancelBarTimer()
    {
        LeanTween.cancel(bar);
    }
}
