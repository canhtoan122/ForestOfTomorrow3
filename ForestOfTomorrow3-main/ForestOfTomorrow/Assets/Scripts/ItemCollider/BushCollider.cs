using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BushCollider : MonoBehaviour
{
    public Level1Controller level1Controller;
    public static bool isBush = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isBush = true;
            level1Controller.PickUpItem(this.transform, this.gameObject);
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            level1Controller.NotPickUpItem();
        }
    }
}
