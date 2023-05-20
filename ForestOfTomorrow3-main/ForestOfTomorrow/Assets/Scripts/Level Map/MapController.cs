using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MapController : MonoBehaviour
{
    public GameObject pickUpButton;
    public GameObject attackButton;
    public GameObject gameInstruction;
    public GameObject playerPosition;
    public GameObject mapUI;
    public GameObject level1Button;
    public Sprite currentLevelButton;

    private bool gameInstructionFinish = false;
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
    public void ActivatePickUpButton()
    {
        pickUpButton.SetActive(true);
        attackButton.SetActive(false);
    }
    public void DeactivatePickUpButton()
    {
        pickUpButton.SetActive(false);
        attackButton.SetActive(true);
    }
    public void ActivateMap()
    {
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
