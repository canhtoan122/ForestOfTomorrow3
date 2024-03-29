using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManagement : MonoBehaviour
{
    [SerializeField]
    private GameObject[] tutorialSteps;
    [SerializeField]
    private Button[] tutorialButtons;
    public bool isMoved = false;
    public bool isJumped = false;
    public bool isDashed = false;
    public bool isAttacked = false;
    public bool isActivate = false;

    private int currentStep = 0;

    private void OnEnable()
    {
        ControllerUI.Instance.OnAttackTriggered += CheckAttack;
        ControllerUI.Instance.OnJumpTriggered += CheckJump;
        ControllerUI.Instance.OnDashTriggered += CheckDash;
        ControllerUI.Instance.OnMoveLeftTriggered += CheckMove;
        ControllerUI.Instance.OnMoveRightTriggered += CheckMove;
    }
    private void OnDisable()
    {
        ControllerUI.Instance.OnAttackTriggered -= CheckAttack;
        ControllerUI.Instance.OnJumpTriggered -= CheckJump;
        ControllerUI.Instance.OnDashTriggered -= CheckDash;
        ControllerUI.Instance.OnMoveLeftTriggered -= CheckMove;
        ControllerUI.Instance.OnMoveRightTriggered -= CheckMove;
    }

    private void Start()
    {
        // Disable all tutorial buttons except the first one
        for (int i = 1; i < tutorialButtons.Length; i++)
        {
            tutorialButtons[i].interactable = false;
        }
    }

    private void CheckAttack()
    {
        isAttacked = true;
        AttackNextStep();
    }    

    private void CheckJump()
    {
        isJumped = true;
        JumpNextStep();
    }    

    private void CheckDash()
    {
        isDashed = true;
        DashNextStep();
    }

    private void CheckMove(bool isMove)
    {
        isMoved = isMove;
        MoveNextStep();
    }

    //Check if player is moved
    public void MoveNextStep()
    {
        // Disable the current tutorial step
        tutorialSteps[currentStep].SetActive(false);

        if (isMoved)
        {
            if (currentStep == 0)
            {
                // Increment the step index
                currentStep++;
            }
            isMoved = false;
        }

        NextStep();
    }
    //Check if player is jumped
    public void JumpNextStep()
    {
        // Disable the current tutorial step
        tutorialSteps[currentStep].SetActive(false);

        if (isJumped)
        {
            if (currentStep == 1)
            {
                // Increment the step index
                currentStep++;
            }
            isJumped = false;
        }

        NextStep();
    }
    //Check if player is dashed
    public void DashNextStep()
    {
        // Disable the current tutorial step
        tutorialSteps[currentStep].SetActive(false);

        if (isDashed)
        {
            if (currentStep == 2)
            {
                // Increment the step index
                currentStep++;
            }
            isDashed = false;
        }

        NextStep();
    }
    //Check if player is attacked
    public void AttackNextStep()
    {
        // Disable the current tutorial step
        tutorialSteps[currentStep].SetActive(false);

        if (isAttacked)
        {
            if (currentStep == 3)
            {
                // Increment the step index
                currentStep++;
            }
            isAttacked = false;
        }

        NextStep();
    }
    public void NextStep()
    {
        if (currentStep < tutorialSteps.Length)
        {
            // Enable the next tutorial step and button
            tutorialSteps[currentStep].SetActive(true);
            tutorialButtons[currentStep].interactable = true;
            if(currentStep == 4 && !isActivate)
            {
                MissionManagement.mission1Complete = true;
                isActivate = true;
            }
        }
        else
        {
            Debug.Log("Tutorial Complete");
        }
    }
}
