/*
    Please attach this script to each ingredient object.
    IngMovement defines some movement proprieties of ingredients including speed, gravity, and hold time.
    Bounding feature is also defined. Add Rigidbody 2D and Circle Collider 2D to the ingredient object. 
    Then drag 'Bouncy' to 'Material' of Collider. The Colliders of walls will require 'BouncyEdge'.
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IngMovement : MonoBehaviour
{
    public float holdTime; //ingredients hold at the top of the screen for a certain amount of time // spawnLeastWait < holdTime <spawnMostWait
    public int rotationSpeed;
    public float horizontalLeastVelocity;
    public float horizontalMostVelocity;
    public float verticalLeastVelocity;
    public float verticalMostVelocity;
    public float gravity;
    public float damage = 1;
    public float id = 0;

    Rigidbody2D rb2d;
    bool hold;
    public bool moving = true;

    float initialHorizontalVelocity;
    float initialVerticalVelocity;
    bool busy = false;

    void Start()
    {
        hold = true;
        initialHorizontalVelocity = Random.Range(horizontalLeastVelocity, horizontalMostVelocity);
        initialVerticalVelocity = Random.Range(verticalLeastVelocity, verticalMostVelocity);
        rb2d = GetComponent<Rigidbody2D> ();
    }

    void Update()
    {
        if (hold)
        {
            StartCoroutine(HoldIng());
        }
        else
        {
            if (Time.timeScale != 0f && moving)
            {
                transform.position = transform.position + new Vector3(initialHorizontalVelocity / 100, initialVerticalVelocity / 100, 0);
                transform.Rotate(Vector3.forward, rotationSpeed * ((initialHorizontalVelocity * initialHorizontalVelocity) + (initialVerticalVelocity * initialVerticalVelocity)));
                rb2d.AddForce(new Vector2(0, gravity));
            }
        }
        
    }

    IEnumerator HoldIng()
    {
        float timer = holdTime;
        while(timer >= 0)
        {
            timer -= Time.deltaTime;
            yield return null;
        }
        hold = false;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (!moving) return;

        if (col.gameObject.tag == "Bucket")
        {
            busy = true;
            var player = col.gameObject.transform.parent.parent.parent.GetComponent<PlayerController>();
            player.AddToBucket(this.gameObject.GetComponent<IngMovement>());
        }
        else if (col.gameObject.tag == "Player" && !busy)
        {
            var player = col.gameObject.GetComponent<PlayerController>();
            player.playerHealth -= damage;
            player.eventManager.GetComponent<HealthDisplay>().UpdateHealth(player.playerHealth, player);
            Destroy(this.gameObject);
        }
        else if (col.gameObject.tag != "Ingredient" && !busy)
        {
            Destroy(this.gameObject);
        }
    }
}
