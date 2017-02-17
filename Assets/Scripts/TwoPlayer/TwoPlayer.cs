using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class TwoPlayer : MonoBehaviour
{
    public static TwoPlayer Instance;

    const int numberCount = 75;

    public GameObject prefabBtn;
    public GameObject panelContent1;
    public GameObject panelContent2;
    public GameObject eventSystem;

    GameObject[] listBtn1 = new GameObject[100];
    GameObject[] listBtn2 = new GameObject[100];
    RectTransform rectBtn1, rectBtn2;
    ButtonPrefab[] listBtnScript1 = new ButtonPrefab[100];
    ButtonPrefab[] listBtnScript2 = new ButtonPrefab[100];

    public GameObject PanelResult1;
    public GameObject PanelResult2;
    public Text TxtScoreLine_1Win;
    public Text TxtScoreLine_2Lose;
    public Text TxtScoreLine_1Lose;
    public Text TxtScoreLine_2Win;
    public GameObject PanelPause1;
    public GameObject PanelPause2;

    public Button BtnBack;
    public Text TxtcurrentNumber1;
    public Text TxtcurrentNumber2;

    float randX, randY;
    string circle1 = "red_circle";
    string circle2 = "blue_circle";

    // Khoảng cách căn với các cạnh
    float marginLeft,
        marginRight,
        marginTop = 20f,
        marginBottom = 10f;

    // Sound
    AudioSource SoundClick1;
    AudioSource SoundClick2;
    AudioSource SoundResult;

    bool isSoundON;
    bool isMusicON;

    // Biến kiểm tra thứ tự
    int tmp;
    int ScorePlayer_1;
    int ScorePlayer_2;
    public Text TxtScore1;
    public Text TxtScore2;

    // Array to save positions of numbers
    Vector2[] positions = new Vector2[numberCount];

    DrawNumber draw;
    
    void Start()
    {
        Instance = this;
        draw = new DrawNumber();
        tmp = 0;
        ScorePlayer_1 = 0;
        ScorePlayer_2 = 0;

        // Set margin by screen size
        if (((float)Screen.height / (float)Screen.width) < 1.7f)
        {
            marginLeft = 25f;
            marginRight = 25f;
        }
        else
        {
            marginLeft = 40f;
            marginRight = 40f;
        }

        // Sound
        SoundClick1 = (AudioSource)gameObject.AddComponent<AudioSource>();
        SoundClick2 = (AudioSource)gameObject.AddComponent<AudioSource>();
        SoundResult = (AudioSource)gameObject.AddComponent<AudioSource>();

        AudioClip ClickAudioClip_1;
        ClickAudioClip_1 = (AudioClip)Resources.Load("Audio/Click_Number1");
        SoundClick1.clip = ClickAudioClip_1;
        SoundClick1.loop = false;

        AudioClip ClickAudioClip_2;
        ClickAudioClip_2 = (AudioClip)Resources.Load("Audio/Click_Number2");
        SoundClick2.clip = ClickAudioClip_2;
        SoundClick2.loop = false;

        AudioClip ResultAudioClip;
        ResultAudioClip = (AudioClip)Resources.Load("Audio/Winner");
        SoundResult.clip = ResultAudioClip;
        SoundResult.loop = false;

        isSoundON = UIManagerScript.Instance.isSoundON;
        isMusicON = UIManagerScript.Instance.isMusicON;

        ShowListNumber();
    }

    // Ham hien thi cac con so
    private void ShowListNumber()
    {

        RectTransform rectContent1 = panelContent1.GetComponent<RectTransform>();
        RectTransform rectContent2 = panelContent2.GetComponent<RectTransform>();
        rectContent1.sizeDelta = new Vector2(rectContent1.sizeDelta.x, rectContent1.sizeDelta.y);
        rectContent2.sizeDelta = new Vector2(rectContent2.sizeDelta.x, rectContent2.sizeDelta.y);

        float screenWidth = rectContent1.sizeDelta.x;
        float screenHeight = rectContent1.sizeDelta.y;

        // Caculate position of numbers
        draw.RandomPossition(screenWidth, screenHeight, numberCount, positions, marginLeft, marginRight, marginTop, marginBottom, 2, 0);

        for (int i = 0; i < numberCount; i++)
        {
            listBtn1[i] = Instantiate(prefabBtn) as GameObject;
            listBtn2[i] = Instantiate(prefabBtn) as GameObject;

            rectBtn1 = listBtn1[i].GetComponent<RectTransform>();
            rectBtn1.SetParent(rectContent1);
            rectBtn1.localPosition = new Vector3(positions[i].x, positions[i].y, 0);
            rectBtn1.localScale = new Vector3(0.8f, 0.8f, 1f);
            rectBtn1.localRotation = Quaternion.Euler(0, 0, 180);

            rectBtn2 = listBtn2[i].GetComponent<RectTransform>();
            rectBtn2.SetParent(rectContent2);
            rectBtn2.localPosition = new Vector3(-positions[i].x, -positions[i].y, 0);
            rectBtn2.localScale = new Vector3(0.8f, 0.8f, 1f);

            listBtnScript1[i] = listBtn1[i].GetComponent<ButtonPrefab>();
            listBtnScript1[i].SetInfo((i + 1).ToString(), i + 1);

            listBtnScript2[i] = listBtn2[i].GetComponent<ButtonPrefab>();
            listBtnScript2[i].SetInfo((i + 1).ToString(), i + 1);

            listBtn1[i].GetComponent<Button>().onClick.AddListener(() => ShowCircle(1));
            listBtn2[i].GetComponent<Button>().onClick.AddListener(() => ShowCircle(2));
        }

    }

    // Hiển thị khoanh tròn
    private void ShowCircle(int player)
    {
        EventSystem ev = eventSystem.GetComponent<EventSystem>();
        GameObject btnSelect = ev.currentSelectedGameObject;
        ButtonPrefab btnScript = btnSelect.GetComponent<ButtonPrefab>();

        if (player == 1 && (btnScript.id - tmp) == 1)
        {
            listBtnScript1[tmp].SetCircle(circle1);
            listBtnScript2[tmp].SetCircle(circle1);
            TxtcurrentNumber1.text = listBtnScript1[tmp].id.ToString();
            TxtcurrentNumber2.text = listBtnScript1[tmp].id.ToString();

            tmp++;
            ScorePlayer_1++;

            if (isSoundON == true)
                SoundClick1.Play();
        }

        if (player == 2 && (btnScript.id - tmp) == 1)
        {
            listBtnScript1[tmp].SetCircle(circle2);
            listBtnScript2[tmp].SetCircle(circle2);
            TxtcurrentNumber1.text = listBtnScript1[tmp].id.ToString();
            TxtcurrentNumber2.text = listBtnScript1[tmp].id.ToString();

            tmp++;
            ScorePlayer_2++;

            if (isSoundON == true)
                SoundClick2.Play();
        }

        if (tmp == 75)
        {
            if (isMusicON == true)
                SoundResult.Play();

            if (ScorePlayer_1 > ScorePlayer_2)
            {
                PanelResult1.SetActive(true);
                PanelResult2.SetActive(false);
                TxtScoreLine_1Win.text = ScorePlayer_1.ToString() + " - " + ScorePlayer_2.ToString();
                TxtScoreLine_2Lose.text = ScorePlayer_2.ToString() + " - " + ScorePlayer_1.ToString();
            }

            else if (ScorePlayer_1 < ScorePlayer_2)
            {
                PanelResult1.SetActive(false);
                PanelResult2.SetActive(true);
                TxtScoreLine_1Lose.text = ScorePlayer_1.ToString() + " - " + ScorePlayer_2.ToString();
                TxtScoreLine_2Win.text = ScorePlayer_2.ToString() + " - " + ScorePlayer_1.ToString();
            }

        }

    }

    // Display Pause screen 
    public void ShowPause1()
    {
        PanelPause1.SetActive(true);
        //PanelPause2.SetActive(false);
    }

    public void ShowPause2()
    {
        //PanelPause1.SetActive(false);
        PanelPause2.SetActive(true);
    }

    // Hide Pause screen
    public void HidePausePanel()
    {
        PanelPause1.SetActive(false);
        PanelPause2.SetActive(false);
    }    
}

