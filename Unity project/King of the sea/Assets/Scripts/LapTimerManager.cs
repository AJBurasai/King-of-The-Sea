using UnityEngine;
using TMPro;
using System.Collections;
using System.Collections.Generic;


public class LapTimerManager : MonoBehaviour
{
    /* this script handles the lap timer ui in the top right of the screen */

    public static int minuteCount;      //variables for milli, seconds, minutes
    public static int secondCount;
    public static float milliCount;
    public static string milliDisplay;      // for displaying milliseconds as string

    public static int minuteCountFinal;     // variables for final time at end of race
    public static int secondCountFinal;
    public static float milliCountFinal;

    public static int currentLap;       // current lap number

    public TextMeshProUGUI minuteBox;       // tmpro ui elements
    public TextMeshProUGUI secondBox;
    public TextMeshProUGUI milliBox;

    void Start()
    {
        currentLap = 1;         // set current lap to 1
    }

    void Update()
    {
        milliCount += Time.deltaTime * 10;      // count milliseconds based on deltatime *10
        milliCountFinal += Time.deltaTime * 10;

        if (milliCount >= 10)       // reset milli when reaching 10
        {
            milliCount -= 10;       // reset counter
            secondCount++;

            milliCountFinal -= 10;
            secondCountFinal++;
        }

        milliDisplay = ((int)milliCount).ToString();        // cast milli as int, then string, then display on label
        milliBox.text = milliDisplay;

        if (secondCount <= 9)       
        {
            secondBox.text = "0" + secondCount + ".";       // keeps the "00" format 
        }
        else
        {
            secondBox.text = "" + secondCount + ".";
        }

        if (secondCount == 60)
        {
            secondCount -= 60;
            minuteCount++;

            secondCountFinal -= 60;
            minuteCountFinal++;
        }

        if (minuteCount <= 9)
        {
            minuteBox.text = "0" + minuteCount + ":";
        }
        else
        {
            minuteBox.text = "" + minuteCount + ":";
        }
    }
}
