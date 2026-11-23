using UnityEngine;
using TMPro;

public class FinishLine : MonoBehaviour
{
    /* This script handles the finish line trigger logic */

    public GameObject finishLineTrig;
    public GameObject checkpointTrig;
    public GameObject lapTimeManager;

    public TextMeshProUGUI lapTimeLabel;
    public string lapTime;

    void OnTriggerEnter()
    {
        checkpointTrig.SetActive(true);
        finishLineTrig.SetActive(false);
        UpdateLapTime();
    }

    void UpdateLapTime()
    {
        lapTime = LapTimerManager.minuteCount.ToString("00") + ":" + LapTimerManager.secondCount.ToString("00") + "." + ((int)LapTimerManager.milliCount).ToString("0");
        lapTimeLabel.text = lapTime;
        LapTimerManager.minuteCount -= LapTimerManager.minuteCount;
        LapTimerManager.secondCount -= LapTimerManager.secondCount;
        LapTimerManager.milliCount -= LapTimerManager.milliCount;
    }
}
