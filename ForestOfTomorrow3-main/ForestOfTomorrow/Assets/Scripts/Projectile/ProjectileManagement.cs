using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileManagement : MonoBehaviour
{
    
    public static bool isSlash = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            isSlash = true;
            Enemy.instance.ApplyDamage();
        }
    }
}
