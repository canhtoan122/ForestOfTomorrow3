using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButtonManagement : MonoBehaviour
{
    [SerializeField]
    private Button menuButton;
    [SerializeField]
    private GameObject menuPanel;
    [SerializeField]
    private Animator anim;
    [SerializeField]
    private ContinueGame data;

    private string sceneName;
    private bool isActivate = false;
    private bool isPaused = false;


    private void OnEnable()
    {
        // Subscribe to the application pause event
        Application.focusChanged += OnApplicationFocus;
    }
    private void OnDisable()
    {
        // Unsubscribe from the application pause event
        Application.focusChanged -= OnApplicationFocus;
    }
    public void MenuButton()
    {
        if (!isActivate)
        {
            OpenMenu();
            isActivate = true;
        }
        else
        {
            CloseMenu();
            isActivate = false;
        }
    }
    public void OpenMenu()
    {
        menuPanel.SetActive(true);
        anim.SetBool("IsSlidingIn", true);
        anim.SetBool("Menu", false);
    }
    public void CloseMenu()
    {
        anim.SetBool("IsSlidingIn", false);
    }
    public void ExitGame()
    {
        sceneName = SceneManager.GetActiveScene().name;
        data.SceneName = sceneName;
        SceneManager.LoadScene("Main menu");
    }
    private void OnApplicationFocus(bool hasFocus)
    {
        if (!hasFocus)
        {
            // The game is being paused
            PauseGame();
        }
        else
        {
            // The game is being resumed
            ResumeGame();
        }
    }

    private void PauseGame()
    {
        // Pause game logic
        Time.timeScale = 0f;
        isPaused = true;
        sceneName = SceneManager.GetActiveScene().name;
        data.SceneName = sceneName;
    }

    private void ResumeGame()
    {
        if (isPaused)
        {
            // Resume game logic
            Time.timeScale = 1f;
            isPaused = false;
        }
    }
}
