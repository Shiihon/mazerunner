using UnityEngine;
using TMPro; // Import TextMeshPro for UI

public class EndZone : MonoBehaviour
{
    public GameObject uiPanel; // Assign the UI Panel in Inspector
    public TextMeshProUGUI messageText; // Use TextMeshPro for text
    public CoinCollection coinCollector; // Reference to your CoinCollection script
    public int totalCoins = 10; // Set the total number of coins in the Inspector

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) // Make sure Player has the "Player" tag
        {
            int coinsCollected = coinCollector.GetCoinCount(); // Get the collected coins

            uiPanel.SetActive(true); // Show UI Panel

            // Check how many coins were collected and set the message
            if (coinsCollected == totalCoins)
                messageText.text = "You ain't special...";
            else if (coinsCollected >= totalCoins / 2)
                messageText.text = "Well... you did alright.";
            else
                messageText.text = "Wow... you sucked.";
        }
    }
}