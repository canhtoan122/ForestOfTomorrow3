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

    [SerializeField]
    private Mission mission1Object;
    [SerializeField]
    private Mission mission2Object;

    private void Start()
    {
        anim = GetComponent<Animator>();
        missionText.text = mission1Object.MissionTitle;
        mission1Object.isActive = true;
    }
    private void Update()
    {
        if (mission1Complete)
        {
            mission1Object.isActive = false;
            anim.SetBool("IsCompleted", true);
        }
    }
    public void Mission2()
    {
        mission2Object.isActive = true;
        missionText.text = mission2Object.MissionTitle;
        mission1Complete = false;
        anim.SetBool("IsCompleted", false);
    }
}
