using UnityEngine;
using TMPro; // Required for TextMeshPro

public class TimerUI : MonoBehaviour
{
    private TextMeshProUGUI timerText;

    void Start()
    {
        timerText = GetComponent<TextMeshProUGUI>();
        UpdateTimerText();
    }

    void Update()
    {
        // Ensure the timer updates every frame
        if (GameTimer.Instance != null && GameTimer.Instance.IsRunning())
        {
            UpdateTimerText();
        }
    }

    void UpdateTimerText()
    {
        float time = GameTimer.Instance.GetElapsedTime();
        timerText.text = "Time: " + time.ToString("F2") + "s"; // Format to 2 decimal places
    }
}