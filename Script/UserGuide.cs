using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UserGuide : MonoBehaviour
{
    public Text instructionText;
    private bool instructionsShown = false;

    void Start()
    {
        // Disable the instruction text at the beginning of the game
        instructionText.gameObject.SetActive(false);
    }

    void Update()
    {
        // Check if the instructions have been shown already
        if (!instructionsShown)
        {
            // Check for player input (e.g., pressing Space)
            if (Input.GetKeyDown(KeyCode.Space))
            {
                // Enable the instruction text when Space is pressed
                instructionText.gameObject.SetActive(true);
                instructionsShown = true; 
            }
        }

        // Check for player input to hide the instruction text (e.g. pressing Enter)
        if (Input.GetKeyDown(KeyCode.Return))
        {
            // Disable the instruction text when Enter is pressed
            instructionText.gameObject.SetActive(false);
        }
    }
}