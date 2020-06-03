using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameEventManager : MonoBehaviour
{

    public bool gameOver;
    private bool paused;
    private int p1Health;
    private int p2Health;
    public Spawner spawner;
    public PlayerController leftPlayer;
    public PlayerController rightPlayer;
    public Text mGameOverText;
    public RectTransform mGameOverPanel;

    public List<GameObject> BucketLeft;
    public Text DamageLeft;
    public Text ExtraLeft;
    public List<GameObject> BucketRight;
    public Text DamageRight;
    public Text ExtraRight;
    public List<IngMovement> combosIngredients;

    public List<GameObject> leftSkins;
    public List<GameObject> rightSkins;

    public DataStorage dataStorage;

    public bool PauseGame = false;
    [SerializeField] private GameObject PauseMenu;
    [SerializeField] private GameObject PizzaLine;



    public void Start()
    {
        //{
        //    if (dataStorage.music_mode == 0)
        //    {
        //        gameObject.GetComponent<BackgroundMusic>().turnOffAudio();
        //    }
        //    else if (dataStorage.music_mode == 1)
        //    {
        //        gameObject.GetComponent<BackgroundMusic>().BGM1();
        //    }
        //    else if (dataStorage.music_mode == 2)
        //    {
        //        gameObject.GetComponent<BackgroundMusic>().BGM2();
        //    }
        //}
        foreach (var i in leftSkins)
        {
            i.SetActive(false);
        }
        leftSkins[dataStorage.left_skin % leftSkins.Count].SetActive(true);
        foreach (var i in rightSkins)
        {
            i.SetActive(false);
        }
        rightSkins[dataStorage.right_skin % rightSkins.Count].SetActive(true);

        gameOver = false;
        paused = false;
        //GameScene.SetActive(true);
        Time.timeScale = 1f;
        //startSpawning();
        //Debug.Log(bgmwrapper);
    }

    public void pause()
    {
        if (paused)
        {
            resume();
        }
        else
        {
            GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "Resume Game";
            Debug.Log("Paused Game");
            paused = true;
            Pauser();
        }
    }
    
    public void resume()
    {
        GameObject.Find("PauseButton").GetComponentInChildren<Text>().text = "Pause Game";
        Debug.Log("Resumed Game");
        paused = false;
        Resume();
    }
    
    public void gameEnd(PlayerController character)
    {
        gameOver = true;
        //stopSpawning();
        mGameOverPanel.gameObject.SetActive(true);
        Time.timeScale = 0f;
        if (leftPlayer == character)
        {
            mGameOverText.text = "Right Player Wins!";
        }
        else
        {
            mGameOverText.text = "Left Player Wins!";
        }
        
    }

    public void ReturnToMenu()
    {
        Debug.Log("Return to main menu");

        SceneManager.LoadScene("Menu");
    }

    public void Pauser()
    {
        if(paused)
        {
            PauseGame = false;
            Time.timeScale = 1f;
            PauseMenu.SetActive(false);
            PizzaLine.SetActive(true);
        }
        else
        {
            PauseGame = true;
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);
            PizzaLine.SetActive(false);
        }
        paused = !paused;
    }

    public void Resume()
    {
        PauseGame = false;
        Time.timeScale = 1f;
        PauseMenu.SetActive(false);
    }
}
