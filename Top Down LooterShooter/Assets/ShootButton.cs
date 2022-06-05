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
        Debug.Log(touch.pointerId);

        //if pointerID == 1, check where pointer 0 is. if it is out of shootarea, dont look at it.
        //if pointer ID == 0, 2
       // if(gun != null && touch.lastPress == shootArea)
       // {
       //     Debug.Log("touch");
       //     Vector2 touchDir = new Vector2(touch.position.x - Camera.main.WorldToScreenPoint(transform.position).x, touch.position.y - Camera.main.WorldToScreenPoint(transform.position).y);
       //     firePoint.transform.up = touchDir;
       //     if (touchDir.x > 0)
       //     {
       //         gun.GetComponent<Weapon>().bulletExit = gun.GetComponent<Weapon>().bulletExitR;
       //         gun.GetComponent<SpriteRenderer>().flipX = false;
       //         player.GetComponent<SpriteRenderer>().flipX = false;
       //     }
       //     else
       //     {
       //         gun.GetComponent<Weapon>().bulletExit = gun.GetComponent<Weapon>().bulletExitL;
       //         gun.GetComponent<SpriteRenderer>().flipX = true;
       //         player.GetComponent<SpriteRenderer>().flipX = true;
       //     }
       // }else if (gun != null && touch.lastPress == joystick)
       // {
       //     player.GetComponent<Moobment>().JoystickControlsDirection();
       //}


    }
    public void OnPointerUp(PointerEventData touch)
    {
        

        pushingShoot = false;

        
    }
    
}
