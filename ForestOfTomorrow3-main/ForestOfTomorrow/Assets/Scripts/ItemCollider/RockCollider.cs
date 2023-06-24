using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockCollider : MonoBehaviour
{
    public Level1Controller level1Controller;
    public static bool isRock = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            isRock = true;
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
