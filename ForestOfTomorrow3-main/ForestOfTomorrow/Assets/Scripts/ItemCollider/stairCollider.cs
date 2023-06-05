using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stairCollider : MonoBehaviour
{
    public PlayerController playerController;
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerController.OnStair();
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            playerController.OffStair();
        }
    }
}
