using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MissionManagement : MonoBehaviour
{
    private static MissionManagement instance;
    public static bool mission1Complete = false;
    public static bool mission2Complete = false;
    public static bool mission3Complete = false;
    public static bool mission4Complete = false;
    public List<Mission> missionList;
    public Mission mainMission;

    private string currentSceneName;
    private Animator anim;
    [SerializeField]
    private Text missionText;

    [SerializeField]
    private Mission mission1Object;
    [SerializeField]
    private Mission mission2Object;
    [SerializeField]
    private Mission mission3Object;
    [SerializeField]
    private Mission mission4Object;
    [SerializeField]
    private Mission mission5Object;


    // Main Mission active based on isActive bool is true or not
    private void Start()
    {
        anim = GetComponent<Animator>();
        currentSceneName = SceneManager.GetActiveScene().name;
        if (currentSceneName == "Opening")
        {
            mission1Object.isActive = true;
        }
        else if(currentSceneName == "Scene 2")
        {
            mission2Complete = true;
            if(mission2Complete)
            {
                mission1Object.isActive = false;
                mission3Object.isActive = true;

            }
        }
        else if(currentSceneName == "Scene 3")
        {
            mission3Complete = true;
            if (mission3Complete)
            {
                mission1Object.isActive = false;
                mission2Object.isActive = false;
                mission4Object.isActive = true;
            }
        }
        
    }
    // Apply Main mission into the text and check if Mission 1 is Complete or not
    private void Update()
    {
        missionText.text = mainMission.MissionTitle;
        if (mission1Complete)
        {
            mission1Object.isActive = false;
            anim.SetBool("IsCompleted", true);
        }
        else if(mission2Complete)
        {
            mission2Object.isActive = false;
            anim.SetBool("IsCompleted", true);
        }
        else if (mission3Complete)
        {
            mission3Object.isActive = false;
            anim.SetBool("IsCompleted", true);
        }
        else if(mission4Complete)
        {
            mission4Object.isActive = false;
            anim.SetBool("IsCompleted", true);
        }
        UpdateMainMission();
    }
    // Create a Main Mission as a Main Scriptable Object for all the mission
    public void UpdateMainMission()
    {
        foreach (Mission mission in missionList)
        {
            if (mission.isActive)
            {
                mainMission.MissionTitle = mission.MissionTitle;
                mainMission.MissionDescription = mission.MissionDescription;
                mainMission.MissionType = mission.MissionType;
                mainMission.MissionProgress = mission.MissionProgress;
                // add any other information you want to transfer here
                break;
            }
        }
    }
    // After mission 1 is complete, mission 2 will be activate
    public void NextMission()
    {
        
        if (mission1Complete)
        {
            mission2Object.isActive = true;
            missionText.text = mainMission.MissionTitle;
            mission1Complete = false;
            anim.SetBool("IsCompleted", false);
        }
        else if (mission2Complete)
        {
            mission3Object.isActive = true;
            missionText.text = mainMission.MissionTitle;
            mission2Complete = false;
            anim.SetBool("IsCompleted", false);
        }
        else if (mission3Complete)
        {
            mission4Object.isActive = true;
            missionText.text = mainMission.MissionTitle;
            mission3Complete = false;
            anim.SetBool("IsCompleted", false);
        }
        else if(mission4Complete)
        {
            mission5Object.isActive = true;
            missionText.text = mainMission.MissionTitle;
            mission4Complete = false;
            anim.SetBool("IsCompleted", false);
        }
    }

}
