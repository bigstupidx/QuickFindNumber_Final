using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OnePlayer : MonoBehaviour
{
    public static OnePlayer Instance;

    static int numberCount = 100;

    public GameObject prefabBtn;
    public GameObject panelContent;
    public GameObject eventSystem;

    GameObject btn;
    RectTransform rectBtn;
    ButtonPrefab btnScript;

    public GameObject PanelResult;
    public GameObject PanelPause;
    public GameObject PanelChoice;

    public Button BtnBack;
    public Text TxtcurrentNumber;
    public Image ImgClock;
    public Image ImgClockLV3;
    public Text Txttime;
    public Text TxttimeLV3;
    float TimeLevel;
    float time;

    // Level, XP, Score
    int Level;
    string saveLevel = "Level";

    public int Gold = 0;
    string saveGold = "SaveGold";

    public int Score = 0;

    public int BestScoreLV_0 = 0;
    string saveBestScoreLV_0 = "BestScoreLevel_0";

    public int BestScoreLV_1 = 0;
    string saveBestScoreLV_1 = "BestScoreLevel_1";

    public int BestScoreLV_2 = 0;
    string saveBestScoreLV_2 = "BestScoreLevel_2";

    public int BestScoreLV_3 = 0;
    string saveBestScoreLV_3 = "BestScoreLevel_3";

    // Level 4
    int[] ListRandomNumber = new int[100];
    int count;
    public int BestScoreLV_4 = 0;
    string saveBestScoreLV_4 = "BestScoreLevel_4";

    // Sound on game
    AudioSource SoundClick;
    AudioSource SoundResult;
    AudioSource SoundResult100;

    bool isSoundON;
    bool isMusicON;

    float randX, randY;
    string circle = "Circle";

    // Khoảng cách căn với các cạnh
    float marginLeft,
        marginRight,
        marginTop,
        marginBottom;

    // Biến điều kiện cho thời gian chạy
    bool runTime;
    bool resetTime;

    // Array to save positions of numbers
    Vector2[] positions = new Vector2[100];

    DrawNumber draw;

    // Use this for initialization
    void Start()
    {
        Instance = this;

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
        marginTop = 150f;
        marginBottom = 25f;

        // Disable PanelChoice
        PanelChoice.SetActive(false);

        draw = gameObject.AddComponent<DrawNumber>();
        btnScript = gameObject.AddComponent<ButtonPrefab>();

        // Load Best Score of each level
        BestScoreLV_0 = PlayerPrefs.GetInt(saveBestScoreLV_0, 0);
        BestScoreLV_1 = PlayerPrefs.GetInt(saveBestScoreLV_1, 0);
        BestScoreLV_2 = PlayerPrefs.GetInt(saveBestScoreLV_2, 0);
        BestScoreLV_3 = PlayerPrefs.GetInt(saveBestScoreLV_3, 0);
        BestScoreLV_4 = PlayerPrefs.GetInt(saveBestScoreLV_4, 0);
        Gold = PlayerPrefs.GetInt(saveGold, 0);

        // Set time for each level
        if (PanelChoiceScript.Instance.Choice_Level == 0)
        {
            numberCount = 90;
            TimeLevel = 16;
            time = TimeLevel;
        }

        if (PanelChoiceScript.Instance.Choice_Level == 1)
        {
            numberCount = 100;
            TimeLevel = 16;
            time = TimeLevel;
        }

        if (PanelChoiceScript.Instance.Choice_Level == 2)
        {
            numberCount = 100;
            TimeLevel = 11;
            time = TimeLevel;
        }

        // Level 2
        if (PanelChoiceScript.Instance.Choice_Level == 3)
        {
            numberCount = 100;
            TimeLevel = 501;
            time = TimeLevel;

            ImgClock.gameObject.SetActive(false);
            ImgClockLV3.gameObject.SetActive(true);
            TxttimeLV3.gameObject.SetActive(true);
        }

        // Level 4
        Generate_ListRanDomNumber();
        count = 0;
        if (PanelChoiceScript.Instance.Choice_Level == 4)
        {
            numberCount = 100;
            TxtcurrentNumber.text = ListRandomNumber[0].ToString();
            TimeLevel = 16;
            time = TimeLevel;
        }

        runTime = true;
        resetTime = false;

        // Sound
        SoundClick = gameObject.AddComponent<AudioSource>();
        SoundResult = gameObject.AddComponent<AudioSource>();
        SoundResult100 = gameObject.AddComponent<AudioSource>();

        isSoundON = UIManagerScript.Instance.isSoundON;
        isMusicON = UIManagerScript.Instance.isMusicON;

        AudioClip ClickAudioClip;
        ClickAudioClip = (AudioClip)Resources.Load("Audio/Click_Number1");
        SoundClick.clip = ClickAudioClip;
        SoundClick.loop = false;

        AudioClip ResultAudioClip;
        ResultAudioClip = (AudioClip)Resources.Load("Audio/Result");
        SoundResult.clip = ResultAudioClip;
        SoundResult.loop = false;

        AudioClip Result100AudioClip;
        Result100AudioClip = (AudioClip)Resources.Load("Audio/Winner");
        SoundResult100.clip = Result100AudioClip;
        SoundResult100.loop = false;

        // Show numbers
        ShowListNumber();
    }

    // Function to display numbers
    public void ShowListNumber()
    {
        RectTransform rectContent = panelContent.GetComponent<RectTransform>();
        rectContent.anchoredPosition = new Vector3(0, 0, 0);
        rectContent.sizeDelta = new Vector2(rectContent.sizeDelta.x, rectContent.sizeDelta.y);

        float screenWidth = rectContent.sizeDelta.x;
        float screenHeight = rectContent.sizeDelta.y;

        draw.RandomPossition(screenWidth, screenHeight, numberCount, positions, marginLeft, marginRight, marginTop, marginBottom, 1, PanelChoiceScript.Instance.Choice_Level);

        for (int i = 0; i < numberCount; i++)
        {
            btn = Instantiate(prefabBtn) as GameObject;

            rectBtn = btn.GetComponent<RectTransform>();
            rectBtn.SetParent(rectContent);
            rectBtn.localPosition = new Vector3(positions[i].x, positions[i].y, 0);
            rectBtn.localScale = new Vector3(1f, 1f, 1f);

            btnScript = btn.GetComponent<ButtonPrefab>();
            btnScript.SetInfo((i + 1).ToString(), i + 1);

            btn.GetComponent<Button>().onClick.AddListener(() => ShowCircle());

        }
    }

    // Display circle when click to number and correct
    private void ShowCircle()
    {
        EventSystem ev = eventSystem.GetComponent<EventSystem>();
        GameObject btnSelect = ev.currentSelectedGameObject;
        ButtonPrefab btnScript = btnSelect.GetComponent<ButtonPrefab>();

        // If Level 0 or 1 or 2
        if (PanelChoiceScript.Instance.Choice_Level == 0 || PanelChoiceScript.Instance.Choice_Level == 1 || PanelChoiceScript.Instance.Choice_Level == 2)
        {
            if (btnScript.id - Score == 1)
            {
                btnScript.SetCircle(circle);
                TxtcurrentNumber.text = btnScript.id.ToString();

                Score++;

                if (PanelChoiceScript.Instance.Choice_Level == 0)
                    Gold += 1;
                if (PanelChoiceScript.Instance.Choice_Level == 1)
                    Gold += 2;
                if (PanelChoiceScript.Instance.Choice_Level == 2)
                    Gold += 5;

                resetTime = true;

                // Play sound when click to correct number
                if (isSoundON == true)
                    SoundClick.Play();
            }
        }

        // If Level 3
        if (PanelChoiceScript.Instance.Choice_Level == 3)
        {
            if (btnScript.id - Score == 1)
            {
                btnScript.SetCircle(circle);
                TxtcurrentNumber.text = btnScript.id.ToString();

                Score++;

                Gold += 7;

                // Play sound when click to correct number
                if (isSoundON == true)
                    SoundClick.Play();
            }
        }

        // If Level 4
        if (PanelChoiceScript.Instance.Choice_Level == 4)
        {
            if (btnScript.id == ListRandomNumber[count])
            {
                btnScript.SetCircle(circle);
                TxtcurrentNumber.text = ListRandomNumber[count + 1].ToString();

                Score++;
                Gold += 10;

                resetTime = true;

                count++;

                // Play sound when click to correct number
                if (isSoundON == true)
                    SoundClick.Play();
            }
        }
    }

    // Show Pause screen when click to back button
    public void ShowPausePanel()
    {
        runTime = false;
        PanelPause.SetActive(true);
    }

    // Hide Pause screen when click to No button, continue play
    public void HidePausePanel()
    {
        runTime = true;
        PanelPause.SetActive(false);
    }

    // Return ranndom number in level 3
    public void Generate_ListRanDomNumber()
    {
        bool flag;
        int count = 0;

        for (count = 0; count < 100; count++)
        {
            if (count == 0)
            {
                ListRandomNumber[count] = Random.Range(1, 101);

            }

            if (count > 0)
            {
                do
                {
                    flag = true;
                    ListRandomNumber[count] = Random.Range(1, 101);
                    for (int j = 0; j < count; j++)
                    {
                        if (ListRandomNumber[j] == ListRandomNumber[count])
                        {
                            flag = false;
                            break;
                        }
                    }

                } while (flag == false);
            }

        }

    }

    // Update is called once per frame
    void Update()
    {
        int level = PlayerPrefs.GetInt(saveLevel, 0);

        // Bộ đếm thời gian của đồng hồ
        if (time > -1f && runTime == true)
        {
            time -= Time.deltaTime;
            if (time >= 0)
            {
                Txttime.text = ((int)time).ToString();
                TxttimeLV3.text = ((int)time).ToString();
            }

            // Out time and display result screen
            if (time < 0f)
            {

                PanelResult.SetActive(true);

                // Save Gold
                PlayerPrefs.SetInt(saveGold, Gold);
                PlayerPrefs.Save();

                // Next level if get enough Gold
                if (level == 0)
                {
                    if (Gold >= 1000)
                    {
                        Level = 1;
                        PlayerPrefs.SetInt(saveLevel, Level);
                        PlayerPrefs.Save();
                    }
                }
                else if (level == 1)
                {
                    if (Gold >= 3000)
                    {
                        Level = 2;
                        PlayerPrefs.SetInt(saveLevel, Level);
                        PlayerPrefs.Save();
                    }
                }
                else if (level == 2)
                {
                    if (Gold >= 6000)
                    {
                        Level = 3;
                        PlayerPrefs.SetInt(saveLevel, Level);
                        PlayerPrefs.Save();
                    }
                }
                else if (level == 3)
                {
                    if (Gold >= 10000)
                    {
                        Level = 4;
                        PlayerPrefs.SetInt(saveLevel, Level);
                        PlayerPrefs.Save();
                    }
                }
                else if (level == 4)
                {
                    if (Gold >= 15000)
                    {
                        Level = 5;
                        PlayerPrefs.SetInt(saveLevel, Level);
                        PlayerPrefs.Save();
                    }
                }


                // Save Score in each level
                if (PanelChoiceScript.Instance.Choice_Level == 0)
                {
                    // Kiểm tra và lưu nếu điểm là cao nhất
                    if (Score > BestScoreLV_0)
                    {
                        PlayerPrefs.SetInt(saveBestScoreLV_0, Score);
                        PlayerPrefs.Save();
                    }

                    BestScoreLV_0 = PlayerPrefs.GetInt(saveBestScoreLV_0, 0);

                    // Điều kiện này để tránh xảy ra lỗi khởi tạo
                    if (time < -1f)
                    {
                        // Sound
                        if (isMusicON == true)
                            SoundResult.Play();

                        OnePlayerResultScript.Instance.SetInfo(Score, BestScoreLV_0, Gold);
                        
                    }

                }

                if (PanelChoiceScript.Instance.Choice_Level == 1)
                {
                    // Kiểm tra và lưu nếu điểm là cao nhất
                    if (Score > BestScoreLV_1)
                    {
                        PlayerPrefs.SetInt(saveBestScoreLV_1, Score);
                        PlayerPrefs.Save();
                    }

                    BestScoreLV_1 = PlayerPrefs.GetInt(saveBestScoreLV_1, 0);

                    // Điều kiện này để tránh xảy ra lỗi khởi tạo
                    if (time < -1f)
                    {
                        // Sound
                        if (isMusicON == true)
                            SoundResult.Play();

                        OnePlayerResultScript.Instance.SetInfo(Score, BestScoreLV_1, Gold);
                    }

                }

                if (PanelChoiceScript.Instance.Choice_Level == 2)
                {
                    // Kiểm tra và lưu nếu điểm là cao nhất
                    if (Score > BestScoreLV_2)
                    {
                        PlayerPrefs.SetInt(saveBestScoreLV_2, Score);
                        PlayerPrefs.Save();
                    }

                    BestScoreLV_2 = PlayerPrefs.GetInt(saveBestScoreLV_2, 0);

                    // Điều kiện này để tránh xảy ra lỗi khởi tạo
                    if (time < -1f)
                    {
                        // Sound
                        if (isMusicON == true)
                            SoundResult.Play();

                        OnePlayerResultScript.Instance.SetInfo(Score, BestScoreLV_2, Gold);
                    }

                }

                if (PanelChoiceScript.Instance.Choice_Level == 3)
                {
                    // Kiểm tra và lưu nếu điểm là cao nhất
                    if (Score > BestScoreLV_3)
                    {
                        PlayerPrefs.SetInt(saveBestScoreLV_3, Score);
                        PlayerPrefs.Save();
                    }

                    BestScoreLV_3 = PlayerPrefs.GetInt(saveBestScoreLV_3, 0);

                    // Điều kiện này để tránh xảy ra lỗi khởi tạo
                    if (time < -1f)
                    {
                        // Sound
                        if (isMusicON == true)
                            SoundResult.Play();

                        OnePlayerResultScript.Instance.SetInfo(Score, BestScoreLV_3, Gold);
                    }

                }

                if (PanelChoiceScript.Instance.Choice_Level == 4)
                {
                    // Kiểm tra và lưu nếu điểm là cao nhất
                    if (Score > BestScoreLV_4)
                    {
                        PlayerPrefs.SetInt(saveBestScoreLV_4, Score);
                        PlayerPrefs.Save();
                    }

                    BestScoreLV_4 = PlayerPrefs.GetInt(saveBestScoreLV_4, 0);

                    // Điều kiện này để tránh xảy ra lỗi khởi tạo
                    if (time < -1f)
                    {
                        // Sound
                        if (isMusicON == true)
                            SoundResult.Play();

                        OnePlayerResultScript.Instance.SetInfo(Score, BestScoreLV_4, Gold);
                    }

                }

            }


            // When player completed with 100 numbers
            if (Score == numberCount)
            {
                PanelResult.SetActive(true);

                // Sound
                if (isMusicON == true)
                    SoundResult100.Play();

                // Save Gold
                PlayerPrefs.SetInt(saveGold, Gold);
                PlayerPrefs.Save();

                Gold = PlayerPrefs.GetInt(saveGold, 0);
                OnePlayerResultScript.Instance.SetInfo(Score, numberCount, Gold);

                // LevelUp
                if (level == 0)
                {
                    Level = 1;
                    PlayerPrefs.SetInt(saveLevel, Level);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetInt(saveBestScoreLV_0, Score);
                    PlayerPrefs.Save();
                }

                if (level == 1)
                {
                    Level = 2;
                    PlayerPrefs.SetInt(saveLevel, Level);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetInt(saveBestScoreLV_1, Score);
                    PlayerPrefs.Save();
                }

                if (level == 2)
                {
                    Level = 3;
                    PlayerPrefs.SetInt(saveLevel, Level);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetInt(saveBestScoreLV_2, Score);
                    PlayerPrefs.Save();
                }

                if (level == 3)
                {
                    Level = 4;
                    PlayerPrefs.SetInt(saveLevel, Level);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetInt(saveBestScoreLV_3, Score);
                    PlayerPrefs.Save();
                }

                if (level == 4)
                {
                    Level = 5;
                    PlayerPrefs.SetInt(saveLevel, Level);
                    PlayerPrefs.Save();

                    PlayerPrefs.SetInt(saveBestScoreLV_4, Score);
                    PlayerPrefs.Save();
                }

                runTime = false;
            }

        }

        // Reset time when click to number
        if (resetTime == true)
        {
            time = TimeLevel;
            resetTime = false;
        }

    }
}
