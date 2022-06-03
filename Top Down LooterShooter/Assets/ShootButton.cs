using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootButton : MonoBehaviour
{

    [HideInInspector]public static bool pushingShoot = false;
    
    void Start()
    {
        gameObject.GetComponent<Image>().alphaHitTestMinimumThreshold = .9f;
    }

    public void Pushed()
    {
        pushingShoot = true;
    }
    public void Released()
    {
        pushingShoot = false;
    }
    
    // Update is called once per frame
    void Update()
    {
        
    }
}
