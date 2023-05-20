using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DoorController : MonoBehaviour
{
    public Button attack;
    public Button exit;

    private Animator animator;
    private bool openAnimation = false;
    private void Start()
    {
        animator = GetComponent<Animator>();
    }
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
    // Next scene
    public void NextScene()
    {
        SceneLoader.instance.LoadLevel(SceneManager.GetActiveScene().buildIndex + 1);
        PlayerController.openDoor = false;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attack.gameObject.SetActive(false);
            exit.gameObject.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attack.gameObject.SetActive(true);
            exit.gameObject.SetActive(false);
        }
    }
}
