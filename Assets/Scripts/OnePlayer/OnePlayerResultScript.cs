using UnityEngine;
using UnityEngine.SceneManagement;
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
    }

    public void SetInfo(int score, int bestScore, int gold)
    {
        Score.text = score.ToString();
        BestScore.text = bestScore.ToString();
        Gold.text = gold.ToString();
    }

    public void BtnBackClick()
    {
        SceneManager.LoadScene("Menu");
    }

    public void ReplayClick()
    {
        SceneManager.LoadScene("OnePlayer");
    }   
    
}
