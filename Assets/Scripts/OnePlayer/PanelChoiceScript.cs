using UnityEngine;
using UnityEngine.UI;

public class PanelChoiceScript : MonoBehaviour
{
    public static PanelChoiceScript Instance;

    public GameObject PanelOnePlayer;
    public GameObject PanelPopUp;

    // Gold and Level value on the PopUp
    public Text TxtGold;
    public Text TxtLevel;

    public GameObject ImgCup_LV0;

    public GameObject ImgLock_LV1;
    public GameObject ImgCup_LV1;

    public GameObject ImgLock_LV2;
    public GameObject ImgCup_LV2;

    public GameObject ImgLock_LV3;
    public GameObject ImgCup_LV3;

    public GameObject ImgLock_LV4;
    public GameObject ImgCup_LV4;

    string saveBestScoreLV_0 = "BestScoreLevel_0";
    string saveBestScoreLV_1 = "BestScoreLevel_1";
    string saveBestScoreLV_2 = "BestScoreLevel_2";
    string saveBestScoreLV_3 = "BestScoreLevel_3";
    string saveBestScoreLV_4 = "BestScoreLevel_4";
    string saveLevel = "Level";

    public int Choice_Level;
    
    void Start()
    {
        Instance = this;

        // Advertisement
        int saveAdvertiseCount = PlayerPrefs.GetInt("saveAdCount", 0);
        saveAdvertiseCount++;
        PlayerPrefs.SetInt("saveAdCount", saveAdvertiseCount);
        PlayerPrefs.Save();

        if (PlayerPrefs.GetInt(saveLevel, 0) >= 4)
        {
            ImgLock_LV1.SetActive(false);
            ImgLock_LV2.SetActive(false);
            ImgLock_LV3.SetActive(false);
            ImgLock_LV4.SetActive(false);
        }

        else if (PlayerPrefs.GetInt(saveLevel, 0) >= 3)
        {
            ImgLock_LV1.SetActive(false);
            ImgLock_LV2.SetActive(false);
            ImgLock_LV3.SetActive(false);
        }

        else if (PlayerPrefs.GetInt(saveLevel, 0) >= 2)
        {
            ImgLock_LV1.SetActive(false);
            ImgLock_LV2.SetActive(false);
        }

        else if (PlayerPrefs.GetInt(saveLevel, 0) >= 1)
        {
            ImgLock_LV1.SetActive(false);
        }

        // Display CUP icon in each level
        if (PlayerPrefs.GetInt(saveBestScoreLV_0, 0) == 90)
        {
            ImgCup_LV0.SetActive(true);
        }
        if (PlayerPrefs.GetInt(saveBestScoreLV_1, 0) == 100)
        {
            ImgCup_LV1.SetActive(true);
        }
        if (PlayerPrefs.GetInt(saveBestScoreLV_2, 0) == 100)
        {
            ImgCup_LV2.SetActive(true);
        }
        if (PlayerPrefs.GetInt(saveBestScoreLV_3, 0) == 100)
        {
            ImgCup_LV3.SetActive(true);
        }
        if (PlayerPrefs.GetInt(saveBestScoreLV_4, 0) == 100)
        {
            ImgCup_LV3.SetActive(true);
        }
    }

    // Level 0
    public void BtnLV0_Click()
    {
        Choice_Level = 0;
        PanelOnePlayer.SetActive(true);
    }

    // Level 1
    public void BtnLV1_Click()
    {
        // Player unqualify
        if (PlayerPrefs.GetInt(saveLevel, 0) < 1)
        {
            PanelPopUp.SetActive(true);
            TxtGold.text = "1000";
            TxtLevel.text = "0";
        }
        // Player qualify
        else if (PlayerPrefs.GetInt(saveLevel, 0) >= 1)
        {
            Choice_Level = 1;
            PanelOnePlayer.SetActive(true);
        }

    }

    // Level 2
    public void BtnLV2_Click()
    {
        if (PlayerPrefs.GetInt(saveLevel, 0) < 2)
        {
            PanelPopUp.SetActive(true);
            TxtGold.text = "3000";
            TxtLevel.text = "1";
        }
        else if (PlayerPrefs.GetInt(saveLevel, 0) >= 2)
        {
            Choice_Level = 2;
            PanelOnePlayer.SetActive(true);
        }

    }

    // Level 3
    public void BtnLV3_Click()
    {
        // Check condition with level 3
        if (PlayerPrefs.GetInt(saveLevel, 0) < 3)
        {
            PanelPopUp.SetActive(true);
            TxtGold.text = "6000";
            TxtLevel.text = "2";
        }
        else if (PlayerPrefs.GetInt(saveLevel, 0) >= 3)
        {
            Choice_Level = 3;
            PanelOnePlayer.SetActive(true);
        }

    }

    // Level 4
    public void BtnLV4_Click()
    {
        // Check condition with level 3
        if (PlayerPrefs.GetInt(saveLevel, 0) < 4)
        {
            PanelPopUp.SetActive(true);
            TxtGold.text = "10.000";
            TxtLevel.text = "3";
        }
        else if (PlayerPrefs.GetInt(saveLevel, 0) >= 4)
        {
            Choice_Level = 4;
            PanelOnePlayer.SetActive(true);
        }

    }

    // Close PopUp
    public void BtnCloseClick()
    {
        PanelPopUp.SetActive(false);
    }

    // Back
    public void BtnBackClick()
    {
        Application.LoadLevel("Menu");
    }
    
}
