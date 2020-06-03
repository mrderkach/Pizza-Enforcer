using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundMusic : MonoBehaviour
{
    private static BackgroundMusic _instance;
    public AudioClip _AudioClip1;
    public AudioClip _AudioClip2;
    public AudioSource _AudioSource;
    public GameObject BGM;
    public bool isMute = false;
    public enum MusicSetting { FirstSetup, NoMusic, Audio1, Audio2};
    public PlayerSettings playerSettings;

    //public static BackgroundMusic instance
    //{
    //    get
    //    {
    //        if (_instance == null)
    //        {
    //            _instance = GameObject.FindObjectOfType<BackgroundMusic>();
    //             //DontDestroyOnLoad(_instance.gameObject);
    //        }

    //        return _instance;
    //    }


    //}
    //public void Awake()
    //{
    //    if (_instance == null)
    //    {
    //        _instance = this;
    //        //DontDestroyOnLoad(this.gameObject);
    //    }
    //    else if (this != _instance)
    //    {
    //        Destroy(this.gameObject);
    //    }
        
    //}

    void setupBGM()
    {

        BGM = new GameObject("BGM_Player");
        _AudioSource = BGM.AddComponent<AudioSource>();
        

        switch (playerSettings.MusicSetting)
        {
            case MusicSetting.FirstSetup:
            case MusicSetting.Audio2:
                {
                    _AudioSource.clip = _AudioClip2;
                    _AudioSource.volume = 1;
                    _AudioSource.Play();
                    break;
                }
            case MusicSetting.Audio1:
            {
                    _AudioSource.clip = _AudioClip1;
                    _AudioSource.volume = 1;
                    _AudioSource.Play();
                    break;
            }
            case MusicSetting.NoMusic:
            default:
            {
                    _AudioSource.clip = _AudioClip2;
                    _AudioSource.volume = 1;
                    _AudioSource.Stop();
                    break;
            }
        }
    }

    void Start() 
     {
        GameObject obj = GameObject.FindGameObjectWithTag("Prefs");
       
        if (obj == null)
        {
            obj = new GameObject();
            playerSettings = obj.AddComponent<PlayerSettings>();
            playerSettings.MusicSetting = MusicSetting.Audio1;
        }
        else
        {
            playerSettings = obj.GetComponent<PlayerSettings>();
        }
        setupBGM();

     }


    private void Update()
    {
        if (BGM == null) setupBGM();
        if (!BGM.GetComponent<AudioSource>().isPlaying && playerSettings.MusicSetting == MusicSetting.Audio2)
            BGM2();
        if (!BGM.GetComponent<AudioSource>().isPlaying && playerSettings.MusicSetting == MusicSetting.Audio1)
            BGM1();
        if (playerSettings.MusicSetting == MusicSetting.NoMusic && BGM.GetComponent<AudioSource>().isPlaying)
            turnOffAudio();
    }
    
    public void BGM1()
    {
        BGM.GetComponent<AudioSource>().Stop();
        _AudioSource.clip = _AudioClip1;
        _AudioSource.volume = 1;
        playerSettings.MusicSetting = MusicSetting.Audio1;
        turnOnAudio();
    }

    public void BGM2()
    {

        BGM.GetComponent<AudioSource>().Stop();
        BGM.GetComponent<AudioSource>().clip = _AudioClip2;
        BGM.GetComponent<AudioSource>().volume = 1;
        playerSettings.MusicSetting = MusicSetting.Audio2;
        turnOnAudio();
    }

    public void turnOffAudio()
    {

        playerSettings.MusicSetting = MusicSetting.NoMusic;
        BGM.GetComponent<AudioSource>().Stop();
    }

    public void turnOnAudio()
    {
        BGM.GetComponent<AudioSource>().Play();
    }
    
}
