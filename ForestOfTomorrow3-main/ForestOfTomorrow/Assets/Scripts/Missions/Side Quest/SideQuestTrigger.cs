using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideQuestTrigger : MonoBehaviour
{
    public Level1Controller level1Controller;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            level1Controller.ActivateSideQuest1();
            MissionManagement.instance.ActivateSideQuest();
        }
    }
}
