using UnityEngine;
using UnityEngine.SceneManagement;

public class UIManagerScript : MonoBehaviour
{
    public static UIManagerScript Instance;

    public GameObject PanelMain;
    public GameObject PanelSetting;
    public GameObject PanelExit;
    public bool isSoundON;
    public bool isMusicON;
    public GameObject BtnSoundON;
    public GameObject BtnSoundOFF;
    public GameObject BtnMusicON;
    public GameObject BtnMusicOFF;
        
    void Start()
    {
        isSoundON = true;
        isMusicON = true;
        
        DontDestroyOnLoad(this);
        Instance = this;

    }
    
    public void SettingClick()
    {
        PanelSetting.SetActive(true);
    }

    public void CloseSetting()
    {
        PanelSetting.SetActive(false);
    }

    // Switch sound ON or OFF
    public void SoundToggle()
    {
        if (isSoundON == true)
        {
            isSoundON = false;
            BtnSoundON.SetActive(false);
            BtnSoundOFF.SetActive(true);
        }
        else
        {
            isSoundON = true;
            BtnSoundON.SetActive(true);
            BtnSoundOFF.SetActive(false);
        }
    }

    // Switch Music ON or OFF
    public void MusicToggle()
    {
        if (isMusicON == true) 
        {
            isMusicON = false;
            BtnMusicON.SetActive(false);
            BtnMusicOFF.SetActive(true);
        }
        else
        {
            isMusicON = true;
            BtnMusicON.SetActive(true);
            BtnMusicOFF.SetActive(false);
        }
    }

    public void OnePlayer()
    {
        SceneManager.LoadScene("OnePlayer");
    }

    public void TwoPlayer()
    {
        SceneManager.LoadScene("TwoPlayer");
    }

    public void Menu()
    {
        SceneManager.LoadScene("Menu");
    }

    // Exit game
    public void ExitClick()
    {
        PanelExit.SetActive(true);
    }

    public void Exit_YesClick()
    {
        Application.Quit();
    }

    public void Exit_NoClick()
    {
        PanelExit.SetActive(false);
    }
}
