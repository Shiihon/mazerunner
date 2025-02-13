using UnityEngine;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public static GameTimer Instance; // Singleton to persist across levels
    private float elapsedTime = 0f; // Timer variable
    private bool timerRunning = false; // Track if timer is active

    void Awake()
    {
        // Ensure only one instance of GameTimer exists across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Prevents reset when switching levels
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (timerRunning)
        {
            elapsedTime += Time.deltaTime; // Count time in seconds
        }
    }

    public void StartTimer()
    {
        if (!timerRunning) // Start the timer only once
        {
            timerRunning = true;
            Debug.Log("Timer Started!");
        }
    }

    public void StopTimer()
    {
        timerRunning = false;
        Debug.Log("Timer Stopped! Final Time: " + elapsedTime);
    }

    public float GetElapsedTime()
    {
        return elapsedTime;
    }

    public bool IsRunning()
    {
        return timerRunning;
    }
}