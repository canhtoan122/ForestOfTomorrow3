using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour
{
    [SerializeField]
    private Button attackButton;
    [SerializeField]
    private Button pickUpItemButton;
    [SerializeField]
    private GameObject step6;
    [SerializeField]
    private GameObject step7;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attackButton.gameObject.SetActive(false);
            pickUpItemButton.gameObject.SetActive(true);
            step6.SetActive(false);
            step7.SetActive(true);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attackButton.gameObject.SetActive(true);
            pickUpItemButton.gameObject.SetActive(false);
        }
    }
}
