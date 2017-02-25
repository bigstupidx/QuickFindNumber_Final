using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PauseScript : MonoBehaviour
{
    public static PauseScript Instance;

    public Button BtnYes;
    public Button BtnNo;

    // Use this for initialization
    void Start()
    {
        Instance = this;
    }

    public void BtnYesClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void BtnNoClick_OnePlayer()
    {
        OnePlayer.Instance.HidePausePanel();
    }

    public void BtnNoClick_TwoPlayer()
    {
        TwoPlayer.Instance.HidePausePanel();
    }    
}
