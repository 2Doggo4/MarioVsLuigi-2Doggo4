using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StartButtonRainbowText : MonoBehaviour
{
    public TMP_Text label;
    public Button Button;
    public float colorSpeed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Button.interactable == true)
        {
            float hue = Mathf.PingPong(Time.time * colorSpeed, 1.0f);
            Color rainbowColor = Color.HSVToRGB(hue, 1.0f, 1.0f);
            label.color = rainbowColor;
        }
        else
        {
            label.color = Color.grey;
        }
    }
}
