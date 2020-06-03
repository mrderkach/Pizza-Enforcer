using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour
{

    public PlayerController leftPlayer;
    public PlayerController rightPlayer;
    public Slider leftSlider;
    public Slider rightSlider;
    public Image leftDamageImage;
    public Image rightDamageImage;
    public float flashSpeed = 3f;                               // The speed the damageImage will fade at.
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);     // The colour the damageImage is set to, to flash.

    public bool leftDamaged = false;
    public bool rightDamaged = false;
    public GameEventManager eventManager;

    private void Update()
    {
        if (leftDamaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            leftDamageImage.color = flashColour;
            leftDamaged = false;
        }
        else
        {
            // ... transition the colour back to clear.
            leftDamageImage.color = Color.Lerp(leftDamageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }

        if (rightDamaged)
        {
            // ... set the colour of the damageImage to the flash colour.
            rightDamageImage.color = flashColour;
            rightDamaged = false;
        }
        else
        {
            // ... transition the colour back to clear.
            rightDamageImage.color = Color.Lerp(rightDamageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
    }

    public void UpdateHealth(float value, PlayerController character)
    {

        if (leftPlayer == character)
        {
            leftDamaged = true;
            leftSlider.value = Mathf.Max(value, 0);
            Vector3 tmp = character.eventManager.leftSkins[character.eventManager.dataStorage.left_skin].GetComponent<Body>().body.transform.localScale;
            tmp.x = 1f + 0.5f * (100 - value) / 100;
            character.eventManager.leftSkins[character.eventManager.dataStorage.left_skin].GetComponent<Body>().body.transform.localScale = tmp;

        }
        else
        {
            rightDamaged = true;
            rightSlider.value = Mathf.Max(value, 0);
            Vector3 tmp = character.eventManager.rightSkins[character.eventManager.dataStorage.right_skin].GetComponent<Body>().body.transform.localScale;
            tmp.x = 1f + 0.5f * (100 - value) / 100;
            character.eventManager.rightSkins[character.eventManager.dataStorage.right_skin].GetComponent<Body>().body.transform.localScale = tmp;
        }

        if (value <= 0)
        {
            rightDamaged = false;
            leftDamaged = false;
            eventManager.GetComponent<GameEventManager>().gameEnd(character);
        }
    }
}
