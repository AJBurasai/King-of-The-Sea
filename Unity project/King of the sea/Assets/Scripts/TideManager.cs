using UnityEngine;
using TMPro;

public class TideManager : MonoBehaviour
{
    /* this script handles the tide logic and updates the tide level based on the current lap */


    public GameObject lapTimeManager;
    public GameObject lowTideRocks;
    public GameObject highTideRocks;
    public AIController aiController;

    public bool isHighTide = false; // flag to track the current tide state

    public TextMeshProUGUI tideLabel;

    public void HighTide()     
    {
        isHighTide = true; // set the flag to indicate high tide
        lowTideRocks.SetActive(false);
        highTideRocks.SetActive(true);
        tideLabel.text = "High Tide";
        aiController.TideChanged();
        Debug.Log("tide changed");
    }

    public void LowTide()
    {
        isHighTide = false; // set the flag to indicate low tide   
        lowTideRocks.SetActive(true);
        highTideRocks.SetActive(false);
        tideLabel.text = "Low Tide";
        aiController.TideChanged();
        Debug.Log("tide changed");
    }

}
