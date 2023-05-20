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
    public GameObject level1Button;
    public Sprite currentLevelButton;

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
        if(sceneName == "Scene 4")
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
        mapUI.SetActive(true);
    }
    public void Level1()
    {
        level1Button.GetComponent<Image>().sprite = currentLevelButton;
    }
    public void LoadLevel1()
    {
        SceneManager.LoadScene("End of the Demo");
    }
}
