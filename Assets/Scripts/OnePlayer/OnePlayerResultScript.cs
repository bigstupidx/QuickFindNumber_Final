using UnityEngine;
using UnityEngine.UI;

public class OnePlayerResultScript : MonoBehaviour
{
    public static OnePlayerResultScript Instance;

    public Button BtnBack;
    public Text Score;
    public Text BestScore;
    public Text Gold;

    // Use this for initialization
    void Start()
    {
        Instance = this;

        if (PlayerPrefs.GetInt("saveAdCount", 0) % 3 == 0)
        {
            UM_AdManager.instance.StartInterstitialAd();
        }            
    }

    public void SetInfo(int score, int bestScore, int gold)
    {
        Score.text = score.ToString();
        BestScore.text = bestScore.ToString();
        Gold.text = gold.ToString();
    }

    public void BtnBackClick()
    {
        Application.LoadLevel("Menu");
    }

    public void ReplayClick()
    {
        Application.LoadLevel("OnePlayer");
    }

    public void RateClick()
    {
        UIManagerScript.Instance.RateClick();
    }
    
}
