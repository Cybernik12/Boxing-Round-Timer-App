using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ButtonPressed : MonoBehaviour
{
    private int noPressed;

    public TextMeshProUGUI test;

    // Start is called before the first frame update
    void Start()
    {
        noPressed = 0;
    }

    // Update is called once per frame
    void Update()
    {
        test.text = noPressed.ToString();
    }

    public void Test()
    {
        noPressed += 1;

    }
}
