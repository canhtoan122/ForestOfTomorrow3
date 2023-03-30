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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            attackButton.gameObject.SetActive(false);
            pickUpItemButton.gameObject.SetActive(true);
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
