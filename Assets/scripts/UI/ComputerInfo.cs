using UnityEngine;
using TMPro;
using System;

public class ComputerInfo : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] TextMeshProUGUI dateText; 
    [SerializeField] TextMeshProUGUI machineText;
    [SerializeField] TextMeshProUGUI osText;

    [SerializeField] TextMeshProUGUI[] textFields;


    void Start()
    {
        string machine = SystemInfo.deviceName;
        machineText.text = machine;
        osText.text = Environment.OSVersion.ToString();

        foreach (var field in textFields){
            field.text = field.text.Replace("{machine}", machine);
        }

    }

    // Update is called once per frame
    void Update()
    {
        DateTime now = DateTime.Now;

        timerText.text = now.ToString("hh:mm tt");
        dateText.text = now.ToString("MM/dd/yyyy");
    }
}
