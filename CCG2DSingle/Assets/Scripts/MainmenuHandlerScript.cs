using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainmenuHandlerScript : MonoBehaviour
{
    public GameObject panelMain;
    public GameObject panelOptions;

    [Header("Start")]
    [SerializeField]
    private string sceneToLoad;

    [Header("Options")]
    public GameObject panelOptionsAudio;
    public GameObject panelOptionsCredit;
    public GameObject panelOptionsHelp;


    [Header("Options - Audio")]
    public Slider BGMSlider;


    // Start is called before the first frame update
    void Start()
    {
        panelOptions.SetActive(false);
        panelMain.SetActive(true);
        
        if (!PlayerPrefs.HasKey("BGMVolume"))
        {
            PlayerPrefs.SetFloat("BGMVolume", 1);
            LoadVolume();
        }
        else
        {
            LoadVolume();
        }
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void ClickStartButton()
    {
        Debug.Log("StartButton Clicked");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        //SceneManager.LoadScene("LoadingScreen");
        FindObjectOfType<SceneLoading>().LoadScene(sceneToLoad);
    }



    public void ClickOptionsButton()
    {
        Debug.Log("OptionsButton Clicked");
        panelMain.SetActive(false);
        panelOptions.SetActive(true);
        panelOptionsAudio.SetActive(true);
        panelOptionsCredit.SetActive(false);

    }

    public void ClickOptionsBackButton()
    {
        Debug.Log("OptionsBackButton Clicked");
        panelOptions.SetActive(false);
        panelMain.SetActive(true);
        
    }
    public void ClickOptionsAudioButton()
    {
        Debug.Log("OptionsAudioButton Clicked");
        panelOptionsAudio.SetActive(true);
        panelOptionsCredit.SetActive(false);
        panelOptionsHelp.SetActive(false);

    }
    public void ClickOptionsCreditButton()
    {
        Debug.Log("OptionsCreditButton Clicked");
        panelOptionsAudio.SetActive(false);
        panelOptionsCredit.SetActive(true);
        panelOptionsHelp.SetActive(false);

    }
    public void ClickOptionsHelpButton()
    {
        Debug.Log("OptionsCreditButton Clicked");
        panelOptionsAudio.SetActive(false);
        panelOptionsCredit.SetActive(false);
        panelOptionsHelp.SetActive(true);
    }



    public void ClickQuitButton()
    {
        Debug.Log("QuitButton Clicked");
        Application.Quit();
    }


    public void ChangeBGMVolume()
    {
        AudioListener.volume = BGMSlider.value;
        SaveVolume();
    }
    public void SaveVolume()
    {
        PlayerPrefs.SetFloat("BGMVolume", BGMSlider.value);
    }
    public void LoadVolume()
    {
        BGMSlider.value = PlayerPrefs.GetFloat("BGMVolume");
    }

}
