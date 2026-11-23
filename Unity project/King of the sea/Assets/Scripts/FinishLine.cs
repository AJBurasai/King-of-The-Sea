using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    /* This script handles the finish line trigger logic and updating the appropriate lap timer*/

    public GameObject finishLineTrig;       // finish line and checkpoint triggers
    public GameObject checkpointTrig;
    public GameObject lapTimerManager;       // lap timer manager object

    public TideManager tideManager;      // tide manager object

    public TextMeshProUGUI lap1TimeLabel;       // lap time labels for each lap
    public TextMeshProUGUI lap2TimeLabel;
    public TextMeshProUGUI lap3TimeLabel;

    public string lapTime;      // lap time string to be displayed on the labels

    void OnTriggerEnter()       // when the player enters the finish line trigger, update the lap time and switch triggers
    {
        checkpointTrig.SetActive(true);
        finishLineTrig.SetActive(false);
        
        UpdateLapTime();
    }

    void UpdateLapTime()        // update the lap time based on the current lap and reset the timer
    {
        if (LapTimerManager.currentLap == 1)
        {
            lapTime = LapTimerManager.minuteCount.ToString("00") + ":" + LapTimerManager.secondCount.ToString("00") + "." + ((int)LapTimerManager.milliCount).ToString("0");
            lap1TimeLabel.text = lapTime;
            LapTimerManager.minuteCount -= LapTimerManager.minuteCount;
            LapTimerManager.secondCount -= LapTimerManager.secondCount;
            LapTimerManager.milliCount -= LapTimerManager.milliCount;
            tideManager.HighTide();       // set tide to high tide after lap 1
        }
        else if (LapTimerManager.currentLap == 2)
        {
            lapTime = LapTimerManager.minuteCount.ToString("00") + ":" + LapTimerManager.secondCount.ToString("00") + "." + ((int)LapTimerManager.milliCount).ToString("0");
            lap2TimeLabel.text = lapTime;
            LapTimerManager.minuteCount -= LapTimerManager.minuteCount;
            LapTimerManager.secondCount -= LapTimerManager.secondCount;
            LapTimerManager.milliCount -= LapTimerManager.milliCount;
            tideManager.LowTide();        // set tide to low tide after lap 2
        }
        else if (LapTimerManager.currentLap == 3)
        {
            lapTime = LapTimerManager.minuteCount.ToString("00") + ":" + LapTimerManager.secondCount.ToString("00") + "." + ((int)LapTimerManager.milliCount).ToString("0");
            lap3TimeLabel.text = lapTime;
            LapTimerManager.minuteCount -= LapTimerManager.minuteCount;
            LapTimerManager.secondCount -= LapTimerManager.secondCount;
            LapTimerManager.milliCount -= LapTimerManager.milliCount;
            tideManager.HighTide();       // set tide to high tide after lap 3
        }

        LapTimerManager.currentLap++;        // increment current lap
    }
}
