using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MissionManagement : MonoBehaviour
{
    public static bool mission1Complete = false;
    private Animator anim;
    [SerializeField]
    private Text missionText;

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
    private void Update()
    {
        if (mission1Complete)
        {
            anim.SetBool("IsCompleted", true);
        }
    }
    public void Mission2()
    {
        missionText.text = "Go to the door.";
        mission1Complete = false;
        anim.SetBool("IsCompleted", false);
    }
}
