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
        missionUI.SetActive(true);
        anim.SetBool("IsSlidingIn", false);
    }
    public void CloseMissionUI()
    {
        missionUI.SetActive(false);
    }
    public void Menu()
    {
        anim.SetBool("Menu", true);
        this.gameObject.SetActive(false);
    }
}
