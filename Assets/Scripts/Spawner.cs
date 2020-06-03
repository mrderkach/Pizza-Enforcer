/*
    The spawner will need an array of ingredients (just drag them to the inspector) to generate them randomly. 
    When an ingredient is spawned, it should firstly stay at the top of the screen for seconds (hold time)
    and then drop at random speed.
    To stop the spawner, set the value of stopSpawner to false.
*/

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] ingredients;
    public Vector2 spawnPositionX;  //random spawn location within a certain range
    public float spawnPositionY;
    public float spawnMostWait;  //need this if spawnWait is a random value from a specific range
    public float spawnLeastWait;
    public int startWait;        //wait time before starting spawn
    public bool stopSpawner;
    public float holdTime; //same as holdTime in IngMovement

    IngMovement move;
    int randIngred;
    float interval;
    float[] leftArea;
    float[] rightArea;
    int enableArea;
    float spawnWait1;
    float spawnWait2;

    void Start()
    {
        leftArea = new float[3];
        rightArea = new float[3];
       
        interval = spawnPositionX.y - spawnPositionX.x;
        leftArea[0] = spawnPositionX.x;
        rightArea[0] = spawnPositionX.x + 3 * (interval / 11);
        leftArea[1] = rightArea[0] + interval / 11;
        rightArea[1] = leftArea[1] + 3 * (interval / 11);
        leftArea[2] = rightArea[1] + interval / 11;
        rightArea[2] = leftArea[2] + 3 * (interval / 11);
        StartCoroutine(WaitSpawner());
    }

    void Update()
    {
        
        spawnWait1 = Random.Range(spawnLeastWait, spawnMostWait);
        spawnWait2 = Random.Range(holdTime, spawnMostWait);
        
    }

    IEnumerator WaitSpawner()
    {
        yield return new WaitForSeconds(startWait);
        while (!stopSpawner)
        {
            var rnd = new System.Random();
            var randomNumbers = Enumerable.Range(0, 5).OrderBy(x => rnd.Next()).Take(3).ToList();
            for (int i=0; i<3; i++)
            {
                enableArea = (int)randomNumbers[i];
                if (enableArea >= 3)
                    continue;
                else
                    spawn(enableArea);
                yield return new WaitForSeconds(spawnWait1);
            }
            yield return new WaitForSeconds(spawnWait2);

        }
    }

    void spawn(int idx)
    {
        randIngred = Random.Range(0, ingredients.Length-1);
        //Debug.Log(randIngred);
        Vector3 spawnPosition = new Vector3(Random.Range(leftArea[idx], rightArea[idx]), spawnPositionY, 0);
        var obj = Instantiate(ingredients[randIngred], spawnPosition, gameObject.transform.rotation);
        obj.SetActive(true);
    }
}
