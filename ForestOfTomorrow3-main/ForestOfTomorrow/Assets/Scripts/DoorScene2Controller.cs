using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorScene2Controller : MonoBehaviour
{
    public Animator animator;

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
        animator.SetBool("isOpened", false);
    }
    private void OpenDoor()
    {
        animator.SetBool("isOpened", true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.LogError("Trigger");
            ControllerUI.Instance.ActiveAttackButton( false);
            ControllerUI.Instance.ActiveOpenDoorButton( true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            Debug.LogError("EE");
            ControllerUI.Instance.ActiveAttackButton(true);
            ControllerUI.Instance.ActiveOpenDoorButton(false);
        }
    }
}
