using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameMenu : MonoBehaviour
{
    public DataStorage dataStorage;

    private void Start()
    {
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
