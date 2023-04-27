using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameInstruction : MonoBehaviour
{
    [SerializeField]
    private GameObject arrow;
    private void Update()
    {
        Vector2 cameraPosition = Camera.main.transform.position;
        if (cameraPosition.x >= 25)
        {
            this.gameObject.SetActive(false);
            arrow.SetActive(true);
        }
    }
}
