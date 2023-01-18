using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mono.Cecil;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI roundsText;

    [SerializeField]
    private TMP_InputField rounds;

    [SerializeField]
    private TextMeshProUGUI minutesText;

    [SerializeField]
    private TMP_InputField minutes;

    [SerializeField]
    private TextMeshProUGUI restText;

    [SerializeField]
    private TMP_InputField rest;

    [SerializeField]
    private TMP_Dropdown dropdownTest;

    private int roundsInt;
    private int minutesInt;
    private int restInt;

    [SerializeField]
    private float timer;

    [SerializeField]
    private int timerInt;

    private float timerRest;

    private int timerRestInt;

    private int temp;

    [SerializeField]
    private int seconds;

    private bool start;

    private TouchScreenKeyboardType KeyboardType { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        rounds.keyboardType = TouchScreenKeyboardType.NumberPad;
        minutes.keyboardType = TouchScreenKeyboardType.NumberPad;
        rest.keyboardType = TouchScreenKeyboardType.NumberPad;

        start = false;
    }

    // Update is called once per frame
    void Update()
    {

        roundsInt = int.Parse(rounds.text);
        minutesInt = int.Parse(minutes.text);
        restInt = int.Parse(rest.text);
         /*
        roundsText.text = roundsInt.ToString();
        minutesText.text = minutesInt.ToString();
        restText.text = restInt.ToString();
         */

        minutesText.text = timerInt.ToString();
        restText.text = timerRestInt.ToString();

        if (start == true && temp > 0)
        {


            roundsText.text = temp.ToString();

            timer -= Time.deltaTime;

            timerInt = (int)timer;

            if (timerInt <= 0)
            {
                timer = 0f;
                timerInt = 0;

                timerRest -= Time.deltaTime;

                timerRestInt = (int)timerRest;

                if (timerRest <= 0)
                {
                    timerRest = 0f;
                    timerRestInt = 0;

                    temp--;

                    timer = minutesInt;

                    timerRest = restInt;

                    if (temp <= 0)
                    {
                        start = false;
                    }
                }
            }
        }        
    }

    public void StartTimer()
    {
        // timer = minutesInt * 60;

        timer = minutesInt;

        timerRest = restInt;

        temp = roundsInt;

        start = true;
    }
}
