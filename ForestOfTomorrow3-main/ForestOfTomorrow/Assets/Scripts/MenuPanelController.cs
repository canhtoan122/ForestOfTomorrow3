using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPanelController : MonoBehaviour
{
    [SerializeField]
    private GameObject missionUI;
    private Animator anim;
    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void OpenMissionUI()
    {
        // Pause game logic
        Time.timeScale = 0f;

        ControllerUI.Instance.ActiveMovementUI(false);
        missionUI.SetActive(true);
        anim.SetBool("IsSlidingIn", false);
    }
    public void CloseMissionUI()
    {
        // Pause game logic
        Time.timeScale = 1f;

        ControllerUI.Instance.ActiveMovementUI(true);
        missionUI.SetActive(false);
    }
    public void Menu()
    {
        anim.SetBool("Menu", true);
        this.gameObject.SetActive(false);
    }
}
