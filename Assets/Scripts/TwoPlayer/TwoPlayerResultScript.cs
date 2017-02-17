using UnityEngine;
using UnityEngine.UI;

public class TwoPlayerResultScript : MonoBehaviour
{
    public static TwoPlayerResultScript Instance;

    public Text TxtScoreLine_1;
    public Text TxtScoreLine_2;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        UM_AdManager.instance.StartInterstitialAd();
    }

    public void SetScoreLine(int scorePlayer1, int scorePlayer2)
    {
        TxtScoreLine_1.text = scorePlayer2.ToString() + " - " + scorePlayer1.ToString();
        TxtScoreLine_2.text = scorePlayer1.ToString() + " - " + scorePlayer2.ToString();
    }

    public void BtnBackClick()
    {
        Application.LoadLevel("Menu");
    }

    public void BtnReplayClick()
    {
        Application.LoadLevel("TwoPlayer");
    }    
}
