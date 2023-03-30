using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorScene2Controller : MonoBehaviour
{
    public Button attack;
    public Button open;

    public Animator animator;
    private bool openAnimation = false;
    private void Update()
    {
        openAnimation = PlayerController.openDoor;
        if (openAnimation)
        {
            animator.SetBool("isOpened", true);
            openAnimation = false;
        }
        else
        {
            animator.SetBool("isOpened", false);
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attack.gameObject.SetActive(false);
            open.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attack.gameObject.SetActive(true);
            open.gameObject.SetActive(false);
        }
    }
}
