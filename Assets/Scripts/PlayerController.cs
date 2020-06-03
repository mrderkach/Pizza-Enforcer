using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    public GameObject stick;
   // public Vector2 cannonRotationRange = new Vector2(1, 65);
    public float rotationSpeed = 20;
    public float movementSpeed = 20;
    public float x_left;
    public float x_right;
    public float playerHealth = 100;
    public List<IngMovement> bucket;
    public float totalDamage = 0;
    public bool leftPlayer;
    public GameEventManager eventManager;

    List<GameObject> locs;
    Text damageText;
    Text extraText;
    bool isCombo = false;

    private void Start()
    {
        if (leftPlayer)
        {
            locs = eventManager.BucketLeft;
            damageText = eventManager.DamageLeft;
            extraText = eventManager.ExtraLeft;
        }
        else
        {
            locs = eventManager.BucketRight;
            damageText = eventManager.DamageRight;
            extraText = eventManager.ExtraRight;
        }
        damageText.text = totalDamage.ToString();
        extraText.color = new Color(1f, 0f, 0f, 0.1960f);
        damageText.color = new Color(0f, 0f, 0f, 1f);
        isCombo = false;
    }


    // Use this method to rotate the cannon. Use direction = 1 to rotate up.
    // Use direction = -1 to rotate down.
    public void Rotate(int direction)
    {
        // Calculate the new angle.
        float angle = stick.transform.rotation.eulerAngles.z + direction * rotationSpeed * Time.deltaTime;

        // Ensure the angle is withing the min and max rotation range.
        //angle = Mathf.Clamp(angle, cannonRotationRange.x, cannonRotationRange.y);

        // Apply the new rotation along the Z-axis
        stick.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }

    public void Move(Vector3 new_loc)
    {
        float loc_x = new_loc.x;
        loc_x = Mathf.Clamp(loc_x, x_left, x_right);

        Vector3 temp = transform.position; // copy to an auxiliary variable...
        temp.x = loc_x; // modify the component you want in the variable...
        this.GetComponent<Rigidbody2D>().velocity = (temp - transform.position)*5;
    }

    public void AddToBucket(IngMovement ingredient)
    {
        bucket.Add(ingredient);
        totalDamage += ingredient.damage;
        if (isCombo) totalDamage += ingredient.damage;
        bool spoiled = false;
        if (bucket.Count > locs.Count)
        {
            spoiled = true;
            if (bucket.Count >= locs.Count * 2)
            {
                totalDamage = 14;
            }
            Destroy(ingredient.gameObject);
        }
        else
        {
            ingredient.moving = false;
            Destroy(ingredient.gameObject.GetComponent<Rigidbody2D>());
            Vector3 tmp = Camera.main.ScreenToWorldPoint(locs[bucket.Count - 1].transform.position);
            tmp.z = 0f;
            ingredient.gameObject.transform.position = tmp;
            ingredient.gameObject.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);


            bool combo = true;
            foreach (var i in eventManager.combosIngredients)
            {
                bool found = false;
                foreach (var j in bucket)
                {
                    if (i.id == j.id)
                    {
                        found = true;
                        break;
                    }
                }
                if (!found)
                {
                    combo = false;
                    break;
                }
            }
            if (combo && !isCombo)
            {
                isCombo = true;
                damageText.color = new Color(0f, 1f, 0f, 1f);
                totalDamage = Mathf.Round(totalDamage * 2f);
            }
        }

        if (spoiled)
        {
            if (isCombo)
            {
                totalDamage /= 2;
                isCombo = false;
            }
            damageText.color = new Color(0f, 0f, 0f, 1f);
            extraText.color = new Color(1f, 0f, 0f, 1f);
            totalDamage = Mathf.Round(totalDamage * 0.75f);
        }

        damageText.text = totalDamage.ToString();
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Bucket")
        {
            var other = col.gameObject.transform.parent.parent.parent.GetComponent<PlayerController>();
            if (other.totalDamage > 0)
            {
                playerHealth -= other.totalDamage;
                eventManager.GetComponent<HealthDisplay>().UpdateHealth(playerHealth, this);
            }
            other.IsCombo = false;
            other.totalDamage = 0;
            other.ExtraText.color = new Color(1f, 0f, 0f, 0.1960f);
            other.DamageText.color = new Color(0f, 0f, 0f, 1f);
            other.DamageText.text = other.totalDamage.ToString();
            foreach (var i in other.bucket)
            {
                if (i)
                Destroy(i.gameObject);
            }
            other.bucket.Clear();
        }
    }

    public Text DamageText
    {
        get { return damageText; }
        set { damageText = value; }
    }

    public Text ExtraText
    {
        get { return extraText; }
        set { extraText = value; }
    }

    public bool IsCombo
    {
        get { return isCombo; }
        set { isCombo = value; }
    }
}
