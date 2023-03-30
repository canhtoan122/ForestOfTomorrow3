using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Data", menuName = "Data/Datas")]
public class ContinueGame : ScriptableObject
{
    public bool hasPlayedBefore;
    public string SceneName;
}
