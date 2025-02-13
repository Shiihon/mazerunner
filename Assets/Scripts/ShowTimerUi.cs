using UnityEngine;
using TMPro; // Required for TextMeshPro

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
    }

    void Update()
    {
        if (GameTimer.Instance != null)
        {
            float time = GameTimer.Instance.GetElapsedTime();
            timerText.text = "Time: " + time.ToString("F2") + "s"; 
            
            if (!GameTimer.Instance.IsRunning())
            {
                enabled = false; // Disable this script
            }
        }
    }
}