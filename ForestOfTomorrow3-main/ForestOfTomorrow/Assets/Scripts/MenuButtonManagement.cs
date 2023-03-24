using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuButtonManagement : MonoBehaviour
{
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Animator anim;

    private bool isActivate = false;

    public void MenuButton()
    {
        if (!isActivate)
        {
            OpenMenu();
            isActivate = true;
        }
        else
        {
            CloseMenu();
            isActivate = false;
        }
    }
    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        anim.SetBool("IsSlidingIn", true);
        anim.SetBool("Menu", false);
    }
    public void CloseMenu()
    {
        anim.SetBool("IsSlidingIn", false);
    }
}
