using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public GameObject gameInstruction;
    public GameObject playerPosition;
    public GameObject mapUI;
    public GameObject homeButton;
    public GameObject level1Button;
    public Sprite currentLevelButton;
    public Sprite homeLevelButton;

    private bool gameInstructionFinish = false;

    private void OnEnable()
    {
        ControllerUI.Instance.OnInteractTriggered += ActivateMap;
    }

    private void OnDisable()
    {
        ControllerUI.Instance.OnInteractTriggered -= ActivateMap;
    }

    private void Start()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        if(sceneName == "AP_Scene 4")
        {
            gameInstruction.SetActive(true);
        }
        Level1();
    }
    private void Update()
    {
        if (!gameInstructionFinish)
        {
            Vector2 playerPos = playerPosition.transform.position;
            if (playerPos.x <= -26)
            {
                gameInstruction.SetActive(false);
                gameInstructionFinish = true;
            }
        }
    }
    public void ActivateMap()
    {
        ControllerUI.Instance.ActiveAttackButton(false);
        ControllerUI.Instance.SetInteractState(EInteractState.ACTIVEMAP);
        ControllerUI.Instance.ActiveInteractButton(true);
    }
    public void DeactivateMap()
    {
        ControllerUI.Instance.ActiveMovementUI(true);
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.SetInteractState(EInteractState.NONE);
        ControllerUI.Instance.ActiveInteractButton(false);
    }
    public void ActivateMap(EInteractState interactState)
    {
        if(interactState != EInteractState.ACTIVEMAP)
        {
            return;
        }
        ControllerUI.Instance.ActiveMovementUI(false);
        mapUI.SetActive(true);
    }
    public void DeActivateMap()
    {
        mapUI.SetActive(false);
        ControllerUI.Instance.ActiveMovementUI(true);
    }
    public void LoadHome()
    {
        TapToContinue.playerDie = false;
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.ActiveInteractButton(false);
        SceneManager.LoadScene("AP_Scene 4");
    }
    public void Level1()
    {
        homeButton.GetComponent<Image>().sprite = homeLevelButton;
        homeButton.GetComponent<Button>().interactable = true;
        level1Button.GetComponent<Image>().sprite = currentLevelButton;
        level1Button.GetComponent<Button>().interactable = true;
    }
    public void LoadLevel1()
    {
        ControllerUI.Instance.ActiveAttackButton(true);
        ControllerUI.Instance.ActiveInteractButton(false);
        SceneManager.LoadScene("AP_Level 1");
    }
}
