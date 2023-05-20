using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    private Animator animator;
    private bool openAnimation = false;

    private void OnEnable()
    {
        ControllerUI.Instance.OnOpenDoor += OpenDoor;
    }
    private void OnDisable()
    {
        ControllerUI.Instance.OnOpenDoor -= OpenDoor;
    }
    private void Start()
    {
        animator = GetComponent<Animator>();
        animator.SetBool("isOpened", false);
    }

    private void OpenDoor()
    {
        animator.SetBool("isOpened", true);
        openAnimation = false;
    }    
    // Next scene
    public void NextScene()
    {
        ControllerUI.Instance.ActiveMovementUI(false);
        SceneLoader.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ControllerUI.Instance.ActiveAttackButton(false);
            ControllerUI.Instance.ActiveOpenDoorButton(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            ControllerUI.Instance.ActiveAttackButton(true);
            ControllerUI.Instance.ActiveOpenDoorButton(false);
        }
    }
}
