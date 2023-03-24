using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Mission", menuName = "Missions/Mission")]
public class Mission : ScriptableObject
{
    // Check if the mission is active
    public bool isActive;

    // Create a mission setting
    public string MissionTitle;
    public string MissionDescription;
    public string MissionType;
    public string MissionProgress;
    // Check if player have reach the require description
    public string[] objectiveDescriptions;
}
