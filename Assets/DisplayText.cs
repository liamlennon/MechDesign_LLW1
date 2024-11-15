using UnityEngine;
using UnityEngine.UI;  // Include UI namespace for Text

public class DisplayTextOnOverlap : MonoBehaviour
{
    // Reference to the UI Text component
    public Text displayText;

    // Text you want to display when overlap happens
    public string message = "Overlap detected!";

    // Start is called before the first frame update
    void Start()
    {
        // Initially hide the text
        if (displayText != null)
        {
            displayText.gameObject.SetActive(false);
        }
    }

    // This method is called when a collider enters the trigger area
    private void OnTriggerEnter(Collider other)
    {
        // Check if the other object has a specific tag (optional)
        if (other.CompareTag("Player"))
        {
            // Display the message
            if (displayText != null)
            {
                displayText.text = message;
                displayText.gameObject.SetActive(true);
            }
        }
    }

    // Optionally, hide the text when the object exits the trigger
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (displayText != null)
            {
                displayText.gameObject.SetActive(false);
            }
        }
    }
}