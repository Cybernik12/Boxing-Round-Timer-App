using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

public class Timer : MonoBehaviour
{
    // Input and Output variables

    [SerializeField] private TextMeshProUGUI roundsText;
    [SerializeField] private TMP_InputField roundsInput;

    [SerializeField] private TextMeshProUGUI minutesText;
    [SerializeField] private TMP_InputField minutesInput;

    [SerializeField] private TextMeshProUGUI secondsText;
    [SerializeField] private TMP_InputField secondsInput;

    [SerializeField] private TextMeshProUGUI restText;
    [SerializeField] private TMP_InputField restInput;

    [SerializeField] private Button startButton;
    [SerializeField] private Button resetButton;

    // Variables for the Interger values of the Inputed values

    private int roundsInt;
    private int minutesInt;
    private int secondsInt;
    private int restInt;

    // Variables for the Timers

    private float timer;
    private int timerInt;

    private float timerRest;
    private int timerRestInt;

    private float timerMinutes;
    private float timerSeconds;

    private int rounds;
    private bool start;
    private bool restStart;

    private float countDown = 3.0f;
    private bool countDownEnd;

    // Variables for the Audio

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip roundStart;
    [SerializeField] private AudioClip roundEnd;
    [SerializeField] private AudioClip tenSecondsLeft;

    private float volume = 0.5f;

    private TouchScreenKeyboardType KeyboardType { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        NumberPad();

        start = false;
        restStart = false;
        countDownEnd = false;
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(countDownEnd);
        Debug.Log(start);

        InputToInt();
        DisplayText(); 
        AudioPlayer();

        if (start == true && rounds > 0) // if there are rounds left, start timer
        {
            IntervalTimer();
        }        
    }

    private void NumberPad() // Sets the Input Keyboard to only be NumberPad when on a mobile device
    {
        roundsInput.keyboardType = TouchScreenKeyboardType.NumberPad;
        minutesInput.keyboardType = TouchScreenKeyboardType.NumberPad;
        secondsInput.keyboardType = TouchScreenKeyboardType.NumberPad;
        restInput.keyboardType = TouchScreenKeyboardType.NumberPad;
    }

    private void InputToInt() // Sets the Inputed Value into Int Variables
    {
        roundsInt = int.Parse(roundsInput.text);
        minutesInt = int.Parse(minutesInput.text);
        secondsInt = int.Parse(secondsInput.text);
        restInt = int.Parse(restInput.text);
    }

    private void TimerSet() // Sets the Timer Values as well as the Round and Rest Variables
    {
        rounds = roundsInt;
        timerMinutes = minutesInt;
        timerSeconds = secondsInt;
        timerRest = restInt;

        timer = ((timerMinutes * 60) + timerSeconds);

        timerInt = (int)timer;
        timerRestInt = (int)timerRest;
    }

    private void DisplayText() // Sets the Text variables and displays them
    {
        minutesText.text = (timerInt / 60).ToString("00") + ":" + (timerInt % 60).ToString("00");
        restText.text = "Rest: " + timerRestInt.ToString();
    }

    public void StartButton() // Functionality for the Start Button
    {
        TimerSet();

        start = true;

        resetButton.gameObject.SetActive(true);
        startButton.gameObject.SetActive(false);

        Time.timeScale = 1;
    }

    public void ResetButton() // Functionality for the Reset Button
    {
        TimerSet();

        start = false;
        countDownEnd = false;

        countDown = 3.0f;

        resetButton.gameObject.SetActive(false);
        startButton.gameObject.SetActive(true);

        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }
    }

    public void Pause() // Pauses the scene when the pause button is pressed
    {
        if (Time.timeScale > 0)
        {
            Time.timeScale = 0;
        }

        else
        {
            Time.timeScale = 1;
        }
    }

    private void IntervalTimer() // The Logic on how the Interval Timer works
    {
        roundsText.text = "Rounds: " + rounds.ToString();
        
        countDown -= Time.deltaTime;

        if (countDown <= 0)
        {
            countDown = 0f;
            countDownEnd = true;

            timer -= Time.deltaTime;
            timerInt = (int)timer;

            if (timerInt <= 0) // if timer is 0, start rest timer
            {
                restStart = true;

                timer = 0f;
                timerInt = 0;

                timerRest -= Time.deltaTime;
                timerRestInt = (int)timerRest;

                if (timerRest <= 0) // if rest timer is 0, minus a round
                {
                    restStart = false;

                    timerRest = 0f;
                    timerRestInt = 0;

                    rounds--;

                    timer = ((timerMinutes * 60) + timerSeconds);
                    timerRest = restInt;

                    if (rounds <= 0) // if rounds is 0, stop
                    {
                        start = false;
                    }
                }
            }
        }
    }

    private void AudioPlayer() // Plays correct Audio when Conditions are met
    {
        if (timerInt == 10 && start == true)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(tenSecondsLeft, volume); // For this to not play muliple times, you need an audio that is more than 1 second long
            }
        }

        if (timerInt == ((timerMinutes * 60) + timerSeconds) - 1 && start == true && countDownEnd == true)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(roundStart, volume);
            }
        }

        if (timerInt == 1 && restStart == false && start == true)
        {
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(roundEnd, volume);
            }
        }
    }
}
