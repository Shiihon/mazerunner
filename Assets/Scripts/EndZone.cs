using System;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI; // Import TextMeshPro for UI

public class EndZone : MonoBehaviour
{
    private bool isLevelComplete;

    public GameObject uiPanel; // Assign the UI Panel in Inspector
    public TextMeshProUGUI messageText; // Use TextMeshPro for text
    public Button nextLevelButton; // Reference to the Button

    public CoinCollection coinCollector; // Reference to your CoinCollection script
    public int totalCoins = 10; // Set the total number of coins in the Inspector

    private void Start()
    {
        uiPanel.SetActive(false); // Hide UI at the start
        nextLevelButton.gameObject.SetActive(false); // Hide button at the start
        nextLevelButton.onClick.AddListener(LoadNextLevel); // Assign button click event
        messageText.gameObject.SetActive(false);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure Player has the "Player" tag
        {
            Debug.Log("✅ Player reached EndZone!"); // Debugging

            int coinsCollected = coinCollector.GetCoinCount(); // Get collected coins
            Debug.Log("✅ Coins Collected: " + coinsCollected);

            // Make sure the UI Panel and MessageText are active
            uiPanel.SetActive(true); // Show UI panel
            messageText.gameObject.SetActive(true); // ✅ Ensure the text is active
            nextLevelButton.gameObject.SetActive(true); // Show button
            isLevelComplete = true;

            // Determine the message based on coins collected
            if (coinsCollected == totalCoins)
            {
                messageText.text = "You ain't special...";
            }
            else if (coinsCollected >= totalCoins / 2)
            {
                messageText.text = "Well... you did alright.";
            }
            else
            {
                messageText.text = "Wow... you sucked.";
            }

            Debug.Log("✅ MessageText Updated: " + messageText.text);
        }
    }

    private void LoadNextLevel()
    {
        // Get current scene name
        string currentScene = SceneManager.GetActiveScene().name;

        // Manually define level transitions
        if (currentScene == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
            isLevelComplete = false;
        }
        else if (currentScene == "Level 2")
        {
            SceneManager.LoadScene("Level 3");
            isLevelComplete = false;
        }
        else if (currentScene == "Level 3")
        {
            // If it's the last level, change the text instead of loading a new scene
            messageText.text = "Congrats... you officially suck.";
            nextLevelButton.gameObject.SetActive(false); // Hide button after final level
        }
    }

    public bool IsLevelComplete()
    {
        return isLevelComplete;
    }
}