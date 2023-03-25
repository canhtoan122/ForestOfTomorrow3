using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class MissionManagement : MonoBehaviour
{
    public static MissionManagement instance;
    public static bool mission1Complete = false;

    public List<Mission> missionList;
    public Mission mainMission;

    private Animator anim;
    [SerializeField]
    private Text missionText;

    [SerializeField]
    private Mission mission1Object;
    [SerializeField]
    private Mission mission2Object;

    // Create a instance for other Script can use
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    // Main Mission active based on isActive bool is true or not
    private void Start()
    {
        anim = GetComponent<Animator>();
        mission1Object.isActive = true;
    }
    // Apply Main mission into the text and check if Mission 1 is Complete or not
    private void Update()
    {
        UpdateMainMission();
        missionText.text = mainMission.MissionTitle;
        if (mission1Complete)
        {
            mission1Object.isActive = false;
            anim.SetBool("IsCompleted", true);
        }
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
    public void Mission2()
    {
        UpdateMainMission();
        mission2Object.isActive = true;
        missionText.text = mainMission.MissionTitle;
        mission1Complete = false;
        anim.SetBool("IsCompleted", false);
    }
}
