using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private ContinueGame data;
    [SerializeField]
    private GameObject continueButton;
    [SerializeField]
    private GameObject newButton;
    [SerializeField]
    private GameObject extraButton;
    [SerializeField]
    private GameObject settingButton;
    [SerializeField]
    private GameObject quitButton;
    [SerializeField]
    private GameObject menuButton;
    [SerializeField]
    private GameObject settingUI;
    [SerializeField]
    private GameObject settingDetail;

    [SerializeField]
    private AudioMixer mixer;
    [SerializeField]
    private Slider masterSlider;
    [SerializeField]
    private Slider BGSlider;
    [SerializeField]
    private Slider SFXSlider;

    private string ContinueData;
    private bool hasPlayedBefore = false;
    private bool closeSettingDetail = false;

    void Start()
    {
        //Check if player have played before
        hasPlayedBefore = data.hasPlayedBefore;
        if(hasPlayedBefore)
        {
            continueButton.SetActive(true);
        }
        else { continueButton.SetActive(false); }

        // Set the initial values of the sliders based on the current mixer values
        masterSlider.onValueChanged.AddListener(delegate { SetMasterVolume(masterSlider.value); });
        BGSlider.onValueChanged.AddListener(delegate { SetBGVolume(BGSlider.value); });
        SFXSlider.onValueChanged.AddListener(delegate { SetSFXVolume(SFXSlider.value); });
    }

    public void SetMasterVolume(float volume)
    {
        mixer.SetFloat("MasterMusic", Mathf.Log10(volume) * 20f);
    }
    public void SetBGVolume(float volume)
    {
        mixer.SetFloat("BGMusic", Mathf.Log10(volume) * 20f);
    }
    public void SetSFXVolume(float volume)
    {
        mixer.SetFloat("SFXMusic", Mathf.Log10(volume) * 20f);
    }
    public void StartNewGame()
    {
        data.hasPlayedBefore = true;
        SceneManager.LoadScene(1);
    }
    public void ContinueGame()
    {
        ContinueData = data.SceneName.ToString();
        SceneManager.LoadScene("" + ContinueData);
    }
    public void Extra()
    {
        continueButton.SetActive(false);
        newButton.SetActive(false);
        extraButton.SetActive(false);
        menuButton.SetActive(true);
        settingButton.SetActive(true);
        quitButton.SetActive(true);
    }
    public void Menu()
    {
        //Check if player have played before
        hasPlayedBefore = data.hasPlayedBefore;
        if (hasPlayedBefore)
        {
            continueButton.SetActive(true);
        }
        else { continueButton.SetActive(false); }
        newButton.SetActive(true);
        extraButton.SetActive(true);
        menuButton.SetActive(false);
        settingButton.SetActive(false);
        quitButton.SetActive(false);
    }
    public void Setting()
    {
        settingUI.SetActive(true);
    }
    public void SettingBack()
    {
        settingUI.SetActive(false);
    }
    public void SettingDetail()
    {
        if (!closeSettingDetail)
        {
            settingDetail.SetActive(true);
            closeSettingDetail = true;
        }
        else
        {
            settingDetail.SetActive(false);
            closeSettingDetail = false;
        }
    }
    public void Quit()
    {
        Application.Quit();
    }
}
