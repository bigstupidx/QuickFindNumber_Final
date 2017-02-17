using UnityEngine;

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

    int saveAdvertiseCount;

    public MobileNativeRateUs ratePopUp;
    public int bannerId1;
    
    void Start()
    {
        UM_AdManager.instance.Init();

        isSoundON = true;
        isMusicON = true;

        // Rate for app
        ratePopUp = new MobileNativeRateUs("Like this game?", "Please rate to support future updates!");
        ratePopUp.SetAndroidAppUrl("https://play.google.com/store/apps/details?id=com.OhGame.FindNumber");

        saveAdvertiseCount = PlayerPrefs.GetInt("saveAdCount", 0);
        saveAdvertiseCount++;
        if (saveAdvertiseCount > 4)
        {
            ratePopUp.Start();
            UM_AdManager.instance.StartInterstitialAd(); 
            saveAdvertiseCount = 0;
        }

        PlayerPrefs.SetInt("saveAdCount", saveAdvertiseCount);
        PlayerPrefs.Save();

        DontDestroyOnLoad(this);
        Instance = this;

    }
    
    public void RateClick()
    {
        ratePopUp.Start();
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
        Application.LoadLevel("OnePlayer");
    }

    public void TwoPlayer()
    {
        Application.LoadLevel("TwoPlayer");
    }

    public void Menu()
    {
        Application.LoadLevel("Menu");
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
