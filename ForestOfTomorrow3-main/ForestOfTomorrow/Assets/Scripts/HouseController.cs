using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HouseController : MonoBehaviour
{
    // Next scene
    public void NextScene()
    {
        SceneLoader.instance.LoadLevel(3);
        PlayerController.openDoor = false;
        MissionManagement.mission3Complete = true;
    }
}
