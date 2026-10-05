// this is the timer
// i got this from a tutorial

using UnityEngine;
using TMPro;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    float passedTime;
    bool isRunning = true;

    // Update is called once per frame
    void Update()
    {
        if (!isRunning) return;
        passedTime += Time.deltaTime;
        int minutes = Mathf.FloorToInt(passedTime / 60);
        int seconds = Mathf.FloorToInt(passedTime % 60);

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public void StopTimer()
    {
        isRunning = false;
    }
    
    public float GetTime()
    {
        return passedTime;
    }
}
