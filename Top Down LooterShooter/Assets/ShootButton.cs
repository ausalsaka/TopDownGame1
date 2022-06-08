using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{

    [HideInInspector]public static bool pushingShoot = false;
    public GameObject shootArea;
    public GameObject player;
    public GameObject firePoint;
    [HideInInspector]public GameObject gun;
    public GameObject joystick;
    
    void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = .9f;
    }

    public void OnPointerDown(PointerEventData touch)
    {
        pushingShoot = true;
    }
    public void OnPointerUp(PointerEventData touch)
    {
        pushingShoot = false;
    }
    
}
