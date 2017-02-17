using UnityEngine;

public class PanelReady : MonoBehaviour
{
    public GameObject PanelTwoPlayer;

    bool isMusicON;

    AudioSource SoundReady;
    AudioSource SoundReady_Sword;

    // Use this for initialization
    void Start()
    {
        SoundReady = (AudioSource)gameObject.AddComponent<AudioSource>();
        SoundReady_Sword = (AudioSource)gameObject.AddComponent<AudioSource>();

        AudioClip ReadyAudioClip;
        ReadyAudioClip = (AudioClip)Resources.Load("Audio/Ready");
        SoundReady.clip = ReadyAudioClip;
        SoundReady.loop = false;

        AudioClip ReadySwordAudioClip;
        ReadySwordAudioClip = (AudioClip)Resources.Load("Audio/ReadySword");
        SoundReady_Sword.clip = ReadySwordAudioClip;
        SoundReady_Sword.loop = false;

        isMusicON = UIManagerScript.Instance.isMusicON;

        if (isMusicON == true)
        {
            SoundReady.Play();
            SoundReady_Sword.Play();
        }
    }

    public void Play()
    {
        PanelTwoPlayer.SetActive(true);
    }
}
