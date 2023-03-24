using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagement : MonoBehaviour
{
    [SerializeField] private GameObject[] tutorialSteps;
    [SerializeField] private Button[] tutorialButtons;

    private int currentStep = 0;

    private void Start()
    {
        // Disable all tutorial buttons except the first one
        for (int i = 1; i < tutorialButtons.Length; i++)
        {
            tutorialButtons[i].interactable = false;
        }
    }

    public void NextStep()
    {
        // Disable the current tutorial step
        tutorialSteps[currentStep].SetActive(false);

        // Increment the step index
        currentStep++;

        if (currentStep < tutorialSteps.Length)
        {
            // Enable the next tutorial step and button
            tutorialSteps[currentStep].SetActive(true);
            tutorialButtons[currentStep].interactable = true;
        }
        else
        {
            MissionManagement.mission1Complete = true;
        }
    }

}
