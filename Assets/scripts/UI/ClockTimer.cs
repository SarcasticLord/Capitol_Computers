using UnityEngine;
using TMPro;
using System;

public class ClockTimer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        DateTime now = DateTime.Now;

        timerText.text = now.ToString("hh:mm tt");
    }
}
