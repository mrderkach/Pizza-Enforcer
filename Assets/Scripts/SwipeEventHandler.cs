using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class SwipeEventHandler : MonoBehaviour, IDragHandler
{
    public PlayerController Player;


    // Should this script be enabled on Awake?
    public bool enabledOnAwake = false;

    // Allows setting of the initial enabled state of this script. enabled == false will
    // prevent method events from firing.
    private void Awake()
    {
        enabled = enabledOnAwake;
    }

    // 3
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Drag");
        // If the script is not enabled then don't do anything!
        if (!enabled)
            return;

        // If the change in Y position of the pointer is positive
        // then the user is dragging upward. If it is negative
        // then the user is dragging downward.
        if (eventData.delta.y > 0)
            Player.Rotate(1);
        else if (eventData.delta.y < 0)
            Player.Rotate(-1);

        if (eventData.delta.x != 0)
        {
            Player.Move(Camera.main.ScreenToWorldPoint((Vector3)eventData.position+Vector3.forward));
        }
        else
        {
            Vector2 tmp = new Vector2 (0f, 0f);

            Player.GetComponent<Rigidbody2D>().velocity = tmp;
        }
    }
}
