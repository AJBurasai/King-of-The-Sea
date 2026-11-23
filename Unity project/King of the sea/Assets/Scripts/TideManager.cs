using UnityEngine;
using TMPro;

public class TideManager : MonoBehaviour
{
    /* this script handles the tide logic and updates the tide level based on the current lap */


    public GameObject lapTimeManager;
    public GameObject lowTideRocks;
    public GameObject highTideRocks;

    public TextMeshProUGUI tideLabel;

    public void HighTide()
    {
        lowTideRocks.SetActive(false);
        highTideRocks.SetActive(true);
        tideLabel.text = "High Tide";
    }

    public void LowTide()
    {
        lowTideRocks.SetActive(true);
        highTideRocks.SetActive(false);
        tideLabel.text = "Low Tide";
    }

}
