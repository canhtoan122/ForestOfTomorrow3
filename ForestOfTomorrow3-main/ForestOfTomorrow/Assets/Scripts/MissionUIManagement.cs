using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionUIManagement : MonoBehaviour
{
    [SerializeField]
    private MissionManagement missionManagement;
    [SerializeField] 
    private TMP_Text missionType;
    [SerializeField]
    private TMP_Text missionTitle;
    [SerializeField]
    private TMP_Text missionTitle2;
    [SerializeField]
    private TMP_Text missionProgress;
    [SerializeField]
    private TMP_Text missionDescription;
    [SerializeField]
    private GameObject missionDescription2;

    private bool buttonActive = false;

    private string progressSplitText;
    private string descriptionSplitText;

    private void Start()
    {
        // Split the string into an array based on the "-" character
        string[] progressTextArray = missionManagement.mainMission.MissionProgress.Split('-');
        // Join the array back together with a newline character to create a string with line breaks
        progressSplitText = string.Join("\n", progressTextArray);

        // Split the string into an array based on the "-" character
        string[] descriptionTextArray = missionManagement.mainMission.MissionDescription.Split('-');
        // Join the array back together with a newline character to create a string with line breaks
        descriptionSplitText = string.Join("\n", descriptionTextArray);
    }
    private void Update()
    {
        missionType.text = missionManagement.mainMission.MissionType;
        missionTitle.text = missionManagement.mainMission.MissionTitle;
        missionTitle2.text = missionManagement.mainMission.MissionTitle;
        missionProgress.text = progressSplitText;
        missionDescription.text = descriptionSplitText;
    }
    public void ActiveMissionDescription()
    {
        if (!buttonActive)
        {
            missionDescription2.SetActive(true);
            buttonActive = true;
        }
        else
        {
            missionDescription2.SetActive(false);
            buttonActive = false;
        }
    }
}
