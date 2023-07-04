using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CraftNPCCollider : MonoBehaviour
{
    public CraftController craftController;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            craftController.ActivatePickUpButton();
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            craftController.DeActivatePickUpButton();
        }
    }
}
