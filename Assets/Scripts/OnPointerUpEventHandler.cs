using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

public class OnPointerUpEventHandler :
    MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    public PlayerController Player;

    private void Awake()
    {
    }

    // 4	
    public void OnPointerUp(PointerEventData eventData)
    {
        Vector2 tmp = new Vector2(0f, 0f);

        Player.GetComponent<Rigidbody2D>().velocity = tmp;
    }

    public void OnPointerDown(PointerEventData eventData) { }
}
