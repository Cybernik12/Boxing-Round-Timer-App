using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using Mono.Cecil;

public class Timer : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI roundsText;
    [SerializeField] private TMP_InputField roundsInput;

    [SerializeField] private TextMeshProUGUI minutesText;
    [SerializeField] private TMP_InputField minutesInput;

    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] private TMP_InputField secondsInput;

    [SerializeField] private TextMeshProUGUI restText;
    [SerializeField] private TMP_InputField restInput;

    private int roundsInt;
    private int minutesInt;
    [SerializeField]
    private int secondsInt;
    private int restInt;

    private float timer;
    private int timerInt;

    private float timerMinutes;

    private float timerSeconds;

    private float timerRest;
    private int timerRestInt;

    private int rounds;
    private bool start;

    private TouchScreenKeyboardType KeyboardType { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        roundsInput.keyboardType = TouchScreenKeyboardType.NumberPad;
        minutesInput.keyboardType = TouchScreenKeyboardType.NumberPad;
        secondsInput.keyboardType = TouchScreenKeyboardType.NumberPad;
        restInput.keyboardType = TouchScreenKeyboardType.NumberPad;

        start = false;
    }

    // Update is called once per frame
    void Update()
    {

        roundsInt = int.Parse(roundsInput.text);
        minutesInt = int.Parse(minutesInput.text);
        secondsInt = int.Parse(secondsInput.text);
        restInt = int.Parse(restInput.text);

        minutesText.text = timerInt.ToString();
        restText.text = timerRestInt.ToString();

        if (start == true && rounds > 0) // if there are rounds left, start timer
        {
            roundsText.text = rounds.ToString();
            timer -= Time.deltaTime;
            timerInt = (int)timer;

            if (timerInt <= 0) // if timer is 0, start rest timer
            {
                timer = 0f;
                timerInt = 0;

                timerRest -= Time.deltaTime;
                timerRestInt = (int)timerRest;

                if (timerRest <= 0) // if rest timer is 0, minus a round
                {
                    timerRest = 0f;
                    timerRestInt = 0;

                    rounds--;

                    // Change
                    timer = minutesInt;
                    timerRest = restInt;

                    if (rounds <= 0) // if rounds is 0, stop
                    {
                        start = false;
                    }
                }
            }
        }        
    }

    public void StartTimer()
    {
        rounds = roundsInt;
        timerMinutes = minutesInt;
        timerSeconds = secondsInt;
        timerRest = restInt;

        timer = (timerMinutes * 60) + timerSeconds;

        start = true;
    }
}
