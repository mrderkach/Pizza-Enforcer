using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ChooseCharacter : MonoBehaviour
{
    public List<Sprite> skins;
    public int cur_left = 0;
    public int cur_right = 0;
    public Image leftImage;
    public Image rightImage;

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

    public void NextSkin(int id)
    {
        if (id == 0)
        {
            cur_left += 1;
            if (cur_left >= skins.Count) cur_left = 0;
            leftImage.sprite = skins[cur_left];
        } else
        {
            cur_right += 1;
            if (cur_right >= skins.Count) cur_right = 0;
            rightImage.sprite = skins[cur_right];
        }
    }

    public void PrevSkin(int id)
    {
        if (id == 0)
        {
            cur_left -= 1;
            if (cur_left < 0) cur_left = skins.Count - 1;
            leftImage.sprite = skins[cur_left];
        }
        else
        {
            cur_right -= 1;
            if (cur_right < 0) cur_right = skins.Count - 1;
            rightImage.sprite = skins[cur_right];
        }
    }

    public void GoBack()
    {
        SceneManager.LoadScene("Menu");
    }

    public void Play()
    {
        dataStorage.left_skin = cur_left;
        dataStorage.right_skin = cur_right;
        SceneManager.LoadScene("MainScene");
    }
}
