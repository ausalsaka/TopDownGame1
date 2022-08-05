using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffTimerScript : MonoBehaviour
{

    Image timer;    
    private float timeLeft;
    [HideInInspector] public float maxTime;


    void Start()
    {
        //maxTime = gameObject.GetComponentInParent<BuffTimerControllerScript>().tempDur;
        timer = GetComponent<Image>();
        timeLeft = maxTime;
    }

    void Update()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timer.fillAmount = timeLeft / maxTime;
        }else
        {
            gameObject.GetComponentInParent<BuffTimerControllerScript>().timers -= 1;
            Destroy(gameObject);
        }
    }
}
