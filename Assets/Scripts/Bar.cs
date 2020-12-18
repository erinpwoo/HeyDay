using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DentedPixel;
using UnityEngine.UI;

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
        if (building.requestedPackageType == "No-rush")
        {
            bar.GetComponent<Image>().color = new Color(138/255f, 43/255f, 226/255f);
        } else if (building.requestedPackageType == "Standard")
        {
            bar.GetComponent<Image>().color = Color.red;
        } else if (building.requestedPackageType == "2-day")
        {
            bar.GetComponent<Image>().color = Color.blue;
        }
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
        building.isTimerRunning = false;
        Destroy(building.timer);
        Destroy(timer);
    }

    public void CancelBarTimer()
    {
        LeanTween.cancel(bar);
    }
}
