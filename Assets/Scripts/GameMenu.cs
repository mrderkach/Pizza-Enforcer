using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public DataStorage dataStorage;

    private void Start()
    {
        //if (dataStorage.music_mode == 0)
        //{
        //    gameObject.GetComponent<BackgroundMusic>().turnOffAudio();
        //}
        //else if (dataStorage.music_mode == 1)
        //{
        //    gameObject.GetComponent<BackgroundMusic>().BGM1();
        //}
        //else if (dataStorage.music_mode == 2)
        //{
        //    gameObject.GetComponent<BackgroundMusic>().BGM2();
        //}
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("ChooseCharacter");
    }

    public void SetMusicMode(int i)
    {
        dataStorage.music_mode = i;
    }



    public void QuitGame()
    {
        Application.Quit();
    }


}
