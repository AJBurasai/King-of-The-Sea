using UnityEngine;

public class TrackCheckpoint : MonoBehaviour
{
    /* This script handles the checkpoint trigger logic */

    public GameObject finishLineTrig;
    public GameObject checkpointTrig;

    void OnTriggerEnter()
    {
        finishLineTrig.SetActive(true);
        checkpointTrig.SetActive(false);
    }
}
