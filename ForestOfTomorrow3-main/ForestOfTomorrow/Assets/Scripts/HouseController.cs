using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseController : MonoBehaviour
{
    // Next scene
    public void NextScene()
    {
        ControllerUI.Instance.ActiveMovementUI(false);
        SceneLoader.instance.LoadLevel(4);
        MissionManagement.mission3Complete = true;
    }
}
